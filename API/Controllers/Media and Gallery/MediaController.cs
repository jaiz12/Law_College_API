using API.Controllers.Services;
using BAL.Services.Media_and_Gallery.Album;
using BAL.Services.Media_and_Gallery.Media;
using DTO.Models;
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

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] MediaDTO model)
        {
            try
            {
                // ---------------------------------------------
                // Validate Album
                // ---------------------------------------------

                if (model.AlbumId <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Album is required."
                    });
                }


                // ---------------------------------------------
                // Validate Photo
                // ---------------------------------------------

                if (model.Photo == null)
                {
                    return BadRequest(new
                    {
                        message =
                            "Image or video file is required."
                    });
                }


                // ---------------------------------------------
                // Determine file type
                // ---------------------------------------------

                var contentType =
                    model.Photo.ContentType
                        ?.ToLower();


                // ---------------------------------------------
                // Image
                // ---------------------------------------------

                if (
                    contentType != null &&
                    contentType.StartsWith("image/")
                )
                {
                    model.Image =
                        await _fileUpload.UploadAsync(
                            model.Photo,
                            "Media And Gallery",
                            "Media"
                        );
                }


                // ---------------------------------------------
                // Video
                // ---------------------------------------------

                else if (
                    contentType != null &&
                    contentType.StartsWith("video/")
                )
                {
                    model.Video =
                        await _fileUpload.UploadAsync(
                            model.Photo,
                            "Media And Gallery",
                            "Media"
                        );
                }


                // ---------------------------------------------
                // Invalid file type
                // ---------------------------------------------

                else
                {
                    return BadRequest(new
                    {
                        message =
                            "Only image and video files are allowed."
                    });
                }


                // ---------------------------------------------
                // Save
                // ---------------------------------------------

                var result =
                    await _mediaService.CreateAsync(
                        model
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
        // UPDATE MEDIA
        // PUT: api/Media
        // =====================================================

        [HttpPut]
        public async Task<IActionResult> Update(
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
                // Existing file paths
                // ---------------------------------------------

                var row =
                    existingMedia.Rows[0];


                string? oldImage =
                    row["Image"] == DBNull.Value
                        ? null
                        : row["Image"]?.ToString();


                string? oldVideo =
                    row["Video"] == DBNull.Value
                        ? null
                        : row["Video"]?.ToString();


                // ---------------------------------------------
                // Keep old file by default
                // ---------------------------------------------

                model.Image =
                    oldImage;

                model.Video =
                    oldVideo;


                // ---------------------------------------------
                // New file uploaded
                // ---------------------------------------------

                if (model.Photo != null)
                {
                    var contentType =
                        model.Photo.ContentType
                            ?.ToLower();


                    // =========================================
                    // New Image
                    // =========================================

                    if (
                        contentType != null &&
                        contentType.StartsWith("image/")
                    )
                    {
                        model.Image =
                            await _fileUpload.UploadAsync(
                                model.Photo,
                                "Media And Gallery",
                                "Media"
                            );


                        model.Video = null;


                        // Delete old image

                        if (
                            !string.IsNullOrWhiteSpace(
                                oldImage
                            )
                        )
                        {
                            _fileUpload.Delete(
                                oldImage
                            );
                        }


                        // Delete old video

                        if (
                            !string.IsNullOrWhiteSpace(
                                oldVideo
                            )
                        )
                        {
                            _fileUpload.Delete(
                                oldVideo
                            );
                        }
                    }


                    // =========================================
                    // New Video
                    // =========================================

                    else if (
                        contentType != null &&
                        contentType.StartsWith("video/")
                    )
                    {
                        model.Video =
                            await _fileUpload.UploadAsync(
                                model.Photo,
                                "Media And Gallery",
                                "Media"
                            );


                        model.Image = null;


                        // Delete old image

                        if (
                            !string.IsNullOrWhiteSpace(
                                oldImage
                            )
                        )
                        {
                            _fileUpload.Delete(
                                oldImage
                            );
                        }


                        // Delete old video

                        if (
                            !string.IsNullOrWhiteSpace(
                                oldVideo
                            )
                        )
                        {
                            _fileUpload.Delete(
                                oldVideo
                            );
                        }
                    }


                    // =========================================
                    // Invalid file
                    // =========================================

                    else
                    {
                        return BadRequest(new
                        {
                            message =
                                "Only image and video files are allowed."
                        });
                    }
                }


                // ---------------------------------------------
                // Update database
                // ---------------------------------------------

                var result =
                    await _mediaService.UpdateAsync(
                        model
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
