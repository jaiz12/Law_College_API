using API.Controllers.Services;
using BAL.Services.About.About_Us;
using BAL.Services.About.Administrative_Staff;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfrastructureController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IInfrastructureService _infrastructureService;

        public InfrastructureController(IFileUploadService fileUpload, IInfrastructureService infrastructureService)
        {
            _fileUpload = fileUpload;
            _infrastructureService = infrastructureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _infrastructureService.GetAllAsync();
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
                var result = await _infrastructureService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] InfrastructureDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Photo,
                        "About",
                        "Infrastructure"
                    );
            }
            try
            {


                // Create a model for database
                var page = new InfrastructureDTO
                {
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _infrastructureService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] InfrastructureDTO model)
        {
            var existingPage = await _infrastructureService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Infrastructure not found."
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
                                model.Photo,
                                "About",
                                "Infrastructure"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page = new InfrastructureDTO
                {
                    Id =model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    UpdatedBy = model.UpdatedBy,
                };

                var result = await _infrastructureService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] InfrastructureDTO model)
        {
            try
            {
                var result = await _infrastructureService.deleteAsync(model);
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
