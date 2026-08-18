using API.Controllers.Services;
using BAL.Services.Academics.Academic_Calendar;
using DTO.Models.Academics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Academics
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademicCalendarController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IAcademicCalendarService _academicCalendarService;

        public AcademicCalendarController(IFileUploadService fileUpload, IAcademicCalendarService academicCalendarService)
        {
            _fileUpload = fileUpload;
            _academicCalendarService = academicCalendarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _academicCalendarService.GetAllAsync();
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
                var result = await _academicCalendarService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AcademicCalendarDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        model.File,
                        "Academic",
                        "Academic Calendar"
                    );
            }
            try
            {


                // Create a model for database
                var page = new AcademicCalendarDTO
                {
                    Title = model.Title,
                    Content = model.Content,
                    FilePath = filePath,
                    IsActive = model.IsActive,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _academicCalendarService.CreateAsync(page);
                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(filePath);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _fileUpload.Delete(filePath);
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] AcademicCalendarDTO model)
        {
            var existingPage = await _academicCalendarService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Academic Calendar not found."
                });
            }

            var row = existingPage.Rows[0];


            string? oldfilePath =
                row["FilePath"] == DBNull.Value
                    ? null
                    : row["FilePath"].ToString();

            string? filePath = oldfilePath;

            if (model.File != null)
            {
                filePath = await _fileUpload.UploadAsync(
                                model.File,
                                "Academic",
                                "Academic Calendar"

                            );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }
            try
            {

                var page =
                    new AcademicCalendarDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Content = model.Content,
                        FilePath = filePath,
                        IsActive = model.IsActive,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _academicCalendarService.UpdateAsync(page);

                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(filePath);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                _fileUpload.Delete(filePath);
                return BadRequest(new
                {
                    message =
                        ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] AcademicCalendarDTO model)
        {
            try
            {
                var result = await _academicCalendarService.deleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.FilePath != null)
                    {
                        _fileUpload.Delete(model.FilePath);
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
