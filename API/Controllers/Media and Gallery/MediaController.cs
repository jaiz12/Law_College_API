using API.Controllers.Services;
using BAL.Services.Media_and_Gallery.Album;
using BAL.Services.Media_and_Gallery.Media;
using DTO.Models;
using DTO.Models.DataResponse;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Media_and_Gallery
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IFileUploadService _fileUpload;

        public MediaController(
            IMediaService mediaService,
            IFileUploadService fileUpload)
        {
            _mediaService = mediaService;
            _fileUpload = fileUpload;
        }


        // =====================================================
        // GET ALL MEDIA
        // GET: api/Media
        // =====================================================

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result =
                    await _mediaService.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        // =====================================================
        // GET MEDIA BY ID
        // GET: api/Media/1
        // =====================================================

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(
            int Id)
        {
            try
            {
                var result =
                    await _mediaService.GetByIdAsync(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        // =====================================================
        // GET MEDIA BY ALBUM
        // GET: api/Media/Album/1
        // =====================================================

        [HttpGet("Album/{AlbumId}")]
        public async Task<IActionResult> GetByAlbumId(
            int AlbumId)
        {
            try
            {
                var result =
                    await _mediaService.GetByAlbumIdAsync(
                        AlbumId
                    );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        // =====================================================
        // CREATE MEDIA
        // POST: api/Media
        // =====================================================

        // =====================================================
        // CREATE MEDIA (MULTIPLE FILES SUPPORT)
        // POST: api/Media
        // =====================================================

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MediaDTO model)
        {
            // Track uploaded physical files for cleanup on error
            var uploadedFiles = new List<string>();

            try
            {
                // ---------------------------------------------
                // Validate Album
                // ---------------------------------------------
                if (model.AlbumId <= 0)
                {
                    return BadRequest(new { message = "Album is required." });
                }

                // ---------------------------------------------
                // Validate Photos
                // ---------------------------------------------
                if (model.Photos == null || model.Photos.Count == 0)
                {
                    return BadRequest(new { message = "At least one image or video file is required." });
                }

                // ---------------------------------------------
                // Process Each Uploaded File
                // ---------------------------------------------
                foreach (var file in model.Photos)
                {
                    if (file == null || file.Length == 0)
                        continue;

                    var contentType = file.ContentType?.ToLower();
                    string? uploadedPath = null;
                    string? imagePath = null;
                    string? videoPath = null;

                    // Determine file type and upload
                    if (contentType != null && contentType.StartsWith("image/"))
                    {
                        imagePath = await _fileUpload.UploadAsync(file, "Media And Gallery", "Media");
                        uploadedPath = imagePath;
                    }
                    else if (contentType != null && contentType.StartsWith("video/"))
                    {
                        videoPath = await _fileUpload.UploadAsync(file, "Media And Gallery", "Media");
                        uploadedPath = videoPath;
                    }
                    else
                    {
                        // Roll back any previously saved files in this batch
                        RollbackPhysicalFiles(uploadedFiles);
                        return BadRequest(new { message = $"File '{file.FileName}' is invalid. Only image and video files are allowed." });
                    }

                    // Track physical file path for rollback
                    if (!string.IsNullOrEmpty(uploadedPath))
                    {
                        uploadedFiles.Add(uploadedPath);
                    }

                    // Prepare DTO for database insertion
                    var singleMediaDto = new MediaDTO
                    {
                        AlbumId = model.AlbumId,
                        Image = imagePath,
                        Video = videoPath,
                        CreatedBy = model.CreatedBy
                    };

                    // Save to database
                    var result = await _mediaService.CreateAsync(singleMediaDto);

                    // If database execution fails, trigger rollback
                    if (!result.IsSucceeded)
                    {
                        RollbackPhysicalFiles(uploadedFiles);
                        return BadRequest(new { message = $"Failed to save media record for file '{file.FileName}'." });
                    }
                }

                return Ok(new DataResponse("All media files uploaded successfully.", true));
            }
            catch (Exception ex)
            {
                // On exception, clean up all uploaded physical files
                RollbackPhysicalFiles(uploadedFiles);

                return BadRequest(new { message = ex.Message });
            }
        }

        // Helper method to delete physical files during rollback
        private void RollbackPhysicalFiles(List<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    try
                    {
                        _fileUpload.Delete(filePath);
                    }
                    catch
                    {
                        // Suppress rollback errors to ensure all files attempt deletion
                    }
                }
            }
        }


        // =====================================================
        // UPDATE MEDIA
        // PUT: api/Media
        // =====================================================

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] MediaDTO model)
        {
            string? newlyUploadedFile = null;

            try
            {
                // ---------------------------------------------
                // Validate Media ID
                // ---------------------------------------------
                if (model.Id <= 0)
                {
                    return BadRequest(new { message = "Valid Media ID is required." });
                }

                // ---------------------------------------------
                // Get existing media
                // ---------------------------------------------
                var existingMedia = await _mediaService.GetByIdAsync(model.Id);

                if (existingMedia == null || existingMedia.Rows.Count == 0)
                {
                    return NotFound(new { message = "Media record not found." });
                }

                // Extract existing file paths from database
                var row = existingMedia.Rows[0];
                string? oldImage = row["Image"] == DBNull.Value ? null : row["Image"]?.ToString();
                string? oldVideo = row["Video"] == DBNull.Value ? null : row["Video"]?.ToString();

                // Retain current paths by default
                model.Image = oldImage;
                model.Video = oldVideo;

                // ---------------------------------------------
                // Check for single file replacement in Photos list
                // ---------------------------------------------
                var fileToUpload = model.Photos?.FirstOrDefault(f => f != null && f.Length > 0);

                if (fileToUpload != null)
                {
                    var contentType = fileToUpload.ContentType?.ToLower();

                    // Handle New Image Replacement
                    if (contentType != null && contentType.StartsWith("image/"))
                    {
                        newlyUploadedFile = await _fileUpload.UploadAsync(fileToUpload, "Media And Gallery", "Media");
                        model.Image = newlyUploadedFile;
                        model.Video = null; // Clear video reference when replaced by an image
                    }
                    // Handle New Video Replacement
                    else if (contentType != null && contentType.StartsWith("video/"))
                    {
                        newlyUploadedFile = await _fileUpload.UploadAsync(fileToUpload, "Media And Gallery", "Media");
                        model.Video = newlyUploadedFile;
                        model.Image = null; // Clear image reference when replaced by a video
                    }
                    else
                    {
                        return BadRequest(new { message = "Only image and video files are allowed." });
                    }
                }

                // ---------------------------------------------
                // Update Database Record
                // ---------------------------------------------
                var result = await _mediaService.UpdateAsync(model);

                if (!result.IsSucceeded)
                {
                    // Roll back the newly uploaded physical file if DB fails
                    if (!string.IsNullOrWhiteSpace(newlyUploadedFile))
                    {
                        SafeDeleteFile(newlyUploadedFile);
                    }

                    return BadRequest(new { message = "Failed to update media record in database." });
                }

                // ---------------------------------------------
                // Cleanup Old Physical Files (Safe Delete)
                // ---------------------------------------------
                // Only run cleanup if a new file was uploaded and DB operation succeeded
                if (!string.IsNullOrWhiteSpace(newlyUploadedFile))
                {
                    if (!string.IsNullOrWhiteSpace(oldImage) && oldImage != model.Image)
                    {
                        SafeDeleteFile(oldImage);
                    }

                    if (!string.IsNullOrWhiteSpace(oldVideo) && oldVideo != model.Video)
                    {
                        SafeDeleteFile(oldVideo);
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Cleanup new file on error to prevent orphaned files
                if (!string.IsNullOrWhiteSpace(newlyUploadedFile))
                {
                    SafeDeleteFile(newlyUploadedFile);
                }

                return BadRequest(new { message = ex.Message });
            }
        }

        // Helper method for safely deleting files without crashing the response execution
        private void SafeDeleteFile(string filePath)
        {
            try
            {
                _fileUpload.Delete(filePath);
            }
            catch
            {
                // Suppress deletion errors so main response pipeline completes safely
            }
        }


        // =====================================================
        // DELETE MEDIA
        // DELETE: api/Media
        // =====================================================

        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromForm] MediaDTO model)
        {
            try
            {
                // ---------------------------------------------
                // Get existing media
                // ---------------------------------------------

                var existingMedia =
                    await _mediaService.GetByIdAsync(
                        model.Id
                    );


                if (
                    existingMedia == null ||
                    existingMedia.Rows.Count == 0
                )
                {
                    return NotFound(new
                    {
                        message =
                            "Media not found."
                    });
                }


                // ---------------------------------------------
                // Get existing file paths
                // ---------------------------------------------

                var row =
                    existingMedia.Rows[0];


                string? image =
                    row["Image"] == DBNull.Value
                        ? null
                        : row["Image"]?.ToString();


                string? video =
                    row["Video"] == DBNull.Value
                        ? null
                        : row["Video"]?.ToString();


                // ---------------------------------------------
                // Delete database record
                // ---------------------------------------------

                var result =
                    await _mediaService.DeleteAsync(
                        model
                    );


                // ---------------------------------------------
                // Delete physical files
                // ---------------------------------------------

                if (result.IsSucceeded)
                {
                    if (
                        !string.IsNullOrWhiteSpace(image)
                    )
                    {
                        _fileUpload.Delete(image);
                    }


                    if (
                        !string.IsNullOrWhiteSpace(video)
                    )
                    {
                        _fileUpload.Delete(video);
                    }
                }


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
