using API.Controllers.Services;
using BAL.Services.About.About_Us;
using BAL.Services.Alumni.Notable_Alumni;
using DTO.Models.About;
using DTO.Models.Alumni;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Alumni
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotableAlumniController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly INotableAlumniService _notableAlumniService;

        public NotableAlumniController(IFileUploadService fileUpload, INotableAlumniService notableAlumniService)
        {
            _fileUpload = fileUpload;
            _notableAlumniService = notableAlumniService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _notableAlumniService.GetAllAsync();
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
                var result = await _notableAlumniService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] NotableAlumniDTO model)
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
                        "Notable Alumni"
                    );
            }
            try
            {


                // Create a model for database
                var page = new NotableAlumniDTO
                {
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _notableAlumniService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] NotableAlumniDTO model)
        {
            var existingPage = await _notableAlumniService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Notable Alumni not found."
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
                                "Notable Alumni"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page = new NotableAlumniDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    UpdatedBy = model.UpdatedBy,
                };

                var result = await _notableAlumniService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] NotableAlumniDTO model)
        {
            try
            {
                var result = await _notableAlumniService.deleteAsync(model);
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
