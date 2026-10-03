using API.Controllers.Services;
using BAL.Services.Alumni.Alumni_Events;
using DTO.Models.Alumni;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Alumni
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumniEventsController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IAlumniEventsService _alumniEventsService;

        public AlumniEventsController(IFileUploadService fileUpload, IAlumniEventsService alumniEventsService)
        {
            _fileUpload = fileUpload;
            _alumniEventsService = alumniEventsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _alumniEventsService.GetAllAsync();
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
                var result = await _alumniEventsService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AlumniEventsDTO model)
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
                        "Alumni Events"
                    );
            }
            try
            {


                // Create a model for database
                var page = new AlumniEventsDTO
                {
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _alumniEventsService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] AlumniEventsDTO model)
        {
            var existingPage = await _alumniEventsService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Alumni Events not found."
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
                                "Alumni Events"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page = new AlumniEventsDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Image = imagePath,
                    UpdatedBy = model.UpdatedBy,
                };

                var result = await _alumniEventsService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] AlumniEventsDTO model)
        {
            try
            {
                var result = await _alumniEventsService.deleteAsync(model);
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
