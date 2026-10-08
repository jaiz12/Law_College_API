using API.Controllers.Services;
using BAL.Services.Examinations.Student_Achievers;
using DTO.Models.About;
using DTO.Models.Examinations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Examinations
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAchieversController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IStudentAchieversService _studentAchieversService;

        public StudentAchieversController(IFileUploadService fileUploadService, IStudentAchieversService studentAchieversService)
        {
            _fileUpload = fileUploadService;
            _studentAchieversService = studentAchieversService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _studentAchieversService.GetAllAsync();
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
                var result = await _studentAchieversService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] StudentAchieversDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Name,
                        model.Photo,
                        "Examinations",
                        "Student Achievers"
                    );
            }
            try
            {


                // Create a model for database
                var page = new StudentAchieversDTO
                {
                    Name = model.Name,
                    Content = model.Content,
                    ProfileImage = imagePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _studentAchieversService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] StudentAchieversDTO model)
        {
            var existingPage = await _studentAchieversService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Student Achiever not found."
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
                    model.Name,
                                model.Photo,
                                "Examinations",
                               "Student Achievers"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page = new StudentAchieversDTO
                {
                    Id = model.Id,
                    Name = model.Name,
                    Content = model.Content,
                    ProfileImage = imagePath,
                    UpdatedBy = model.UpdatedBy,
                };

                var result = await _studentAchieversService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] StudentAchieversDTO model)
        {
            try
            {
                var result = await _studentAchieversService.deleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.ProfileImage != null)
                    {
                        _fileUpload.Delete(model.ProfileImage);
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
