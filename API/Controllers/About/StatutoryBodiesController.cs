using API.Controllers.Services;
using BAL.Services.About.Statutory_Bodies;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatutoryBodiesController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IStatutoryBodiesService _istatutoryBodiesService;

        public StatutoryBodiesController(IFileUploadService fileUpload, IStatutoryBodiesService statutoryBodiesService)
        {
            _fileUpload = fileUpload;
            _istatutoryBodiesService = statutoryBodiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _istatutoryBodiesService.GetAllAsync();
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
                var result = await _istatutoryBodiesService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] StatutoryBodiesDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Photo,
                        "About",
                        "Statutory Bodies"
                    );
            }
            try
            {


                // Create a model for database
                var page = new StatutoryBodiesDTO
                {
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _istatutoryBodiesService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] StatutoryBodiesDTO model)
        {
            var existingPage = await _istatutoryBodiesService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Statutory Body not found."
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
                                "Statutory Bodies"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page = new StatutoryBodiesDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    UpdatedBy = model.UpdatedBy,
                };

                var result = await _istatutoryBodiesService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] StatutoryBodiesDTO model)
        {
            try
            {
                var result = await _istatutoryBodiesService.deleteAsync(model);
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
