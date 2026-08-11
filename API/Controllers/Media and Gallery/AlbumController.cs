using API.Controllers.Services;
using BAL.Services.Media_and_Gallery.Album;
using DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Media_and_Gallery
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IAlbumService _albumService;

        public AlbumController(
            IFileUploadService fileUpload,
            IAlbumService albumService
        )
        {
            _fileUpload = fileUpload;
            _albumService = albumService;
        }


        // ---------------------------------------
        // Get All
        // GET: api/Album
        // ---------------------------------------

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result =
                    await _albumService.GetAllAsync();

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


        // ---------------------------------------
        // Get By ID
        // GET: api/Album/1
        // ---------------------------------------

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(
            int Id
        )
        {
            try
            {
                var result =
                    await _albumService.GetByIdAsync(Id);

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


        // ---------------------------------------
        // Create
        // POST: api/Album
        // ---------------------------------------

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AlbumDTO model
        )
        {

            // Upload image
            if (model.Photo != null)
            {
                model.CoverImage =
                    await _fileUpload.UploadAsync(
                        model.Photo,
                        "Media And Gallery",
                        "Album"
                    );
            }
            try
            {
                var result =
                    await _albumService.CreateAsync(
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


        // ---------------------------------------
        // Update
        // PUT: api/Album
        // ---------------------------------------

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromForm] AlbumDTO model
        )
        {
            var existingPage = await _albumService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Administrative Staff not found."
                });
            }

            var row = existingPage.Rows[0];


            string? oldImage =
                row["CoverImage"] == DBNull.Value
                    ? null
                    : row["CoverImage"].ToString();

            string? imagePath = oldImage;

            if (model.Photo != null)
            {
                model.CoverImage =
                    await _fileUpload.UploadAsync(
                        model.Photo,
                        "Media And Gallery",
                        "Album"
                    );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {
                var existingAlbum =
                    await _albumService.GetByIdAsync(
                        model.Id
                    );

                if (
                    existingAlbum == null ||
                    existingAlbum.Rows.Count == 0
                )
                {
                    return NotFound(new
                    {
                        message =
                            "Album not found."
                    });
                }


                var result =
                    await _albumService.UpdateAsync(
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


        // ---------------------------------------
        // Delete
        // DELETE: api/Album
        // ---------------------------------------

        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromForm] AlbumDTO model
        )
        {
            try
            {

                var result = await _albumService.DeleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.CoverImage != null)
                    {
                        _fileUpload.Delete(model.CoverImage);
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
