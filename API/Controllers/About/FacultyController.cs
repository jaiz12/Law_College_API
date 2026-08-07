using API.Controllers.Services;
using BAL.Services.About.Faculty;
using Common.DataContext;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultyController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IFacultyService _facultyService;

        public FacultyController(IFileUploadService fileUpload, IFacultyService facultyService)
        {
            _fileUpload = fileUpload;
            _facultyService = facultyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _facultyService.GetAllAsync();
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
                var result = await _facultyService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] FacultyDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Photo,
                        "About",
                        "Faculties"
                    );
            }
            try
            {


                // Create a model for database
                var page = new FacultyDTO
                {
                    Name = model.Name,
                    Designation = model.Designation,
                    Email = model.Email,
                    Phone = model.Phone,
                    ParentId = model.ParentId,
                    ProfilePhoto = imagePath,
                    DisplayOrder = model.DisplayOrder,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _facultyService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] FacultyDTO model)
        {
            var existingPage = await _facultyService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Faculty not found."
                });
            }

            var row = existingPage.Rows[0];


            string? oldImage =
                row["ProfilePhoto"] == DBNull.Value
                    ? null
                    : row["ProfilePhoto"].ToString();

            string? imagePath = oldImage;

            if (model.Photo != null)
            {
                imagePath = await _fileUpload.UploadAsync(
                                model.Photo,
                                "About",
                                "Faculties"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page =
                    new FacultyDTO
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Designation = model.Designation,
                        Email = model.Email,
                        Phone = model.Phone,
                        ParentId = model.ParentId,
                        ProfilePhoto = imagePath,
                        DisplayOrder = model.DisplayOrder,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _facultyService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] FacultyDTO model)
        {
            try
            {
                var result = await _facultyService.deleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.ProfilePhoto != null)
                    {
                        _fileUpload.Delete(model.ProfilePhoto);
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
