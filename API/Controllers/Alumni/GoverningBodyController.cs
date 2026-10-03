using API.Controllers.Services;
using BAL.Services.About.About_Us;
using BAL.Services.Alumni.Governing_Body;
using DTO.Models.About;
using DTO.Models.Alumni;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Alumni
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoverningBodyController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IGoverningBodyService _governingBodyService;

        public GoverningBodyController(IFileUploadService fileUpload, IGoverningBodyService governingBodyService)
        {
            _fileUpload = fileUpload;
            _governingBodyService = governingBodyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _governingBodyService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var result = await _governingBodyService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] GoverningBodyDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Title,
                        model.Photo,
                        "Alumni",
                        "Governing Body"
                    );
            }
            try
            {


                // Create a model for database
                var page = new GoverningBodyDTO
                {
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _governingBodyService.CreateAsync(page);
                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(imagePath);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _fileUpload.Delete(imagePath);
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] GoverningBodyDTO model)
        {
            var existingPage = await _governingBodyService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Governing Body not found."
                });
            }

            var row = existingPage.Rows[0];


            string? oldImage =
                row["Image"] == DBNull.Value
                    ? null
                    : row["Image"].ToString();

            string? imagePath = oldImage;

            if (model.Photo != null)
            {
                imagePath = await _fileUpload.UploadAsync(
                                model.Title,
                                model.Photo,
                                "Alumni",
                                "Governing Body"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page = new GoverningBodyDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    UpdatedBy = model.UpdatedBy,
                };

                var result = await _governingBodyService.UpdateAsync(page);

                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(imagePath);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                _fileUpload.Delete(imagePath);
                return BadRequest(new
                {
                    message =
                        ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] GoverningBodyDTO model)
        {
            try
            {
                var result = await _governingBodyService.deleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.Image != null)
                    {
                        _fileUpload.Delete(model.Image);
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
