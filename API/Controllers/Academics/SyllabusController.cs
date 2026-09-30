using API.Controllers.Services;
using BAL.Services.Academics.Syllabus;
using DTO.Models.Academics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Academics
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyllabusController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly ISyllabusService _syllabusService;
        public SyllabusController(IFileUploadService fileUpload, ISyllabusService syllabusService)
        {
            _fileUpload = fileUpload;
            _syllabusService = syllabusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _syllabusService.GetAllAsync();
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
                var result = await _syllabusService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] SyllabusDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        model.File,
                        "Academic",
                        "Syllabus"
                    );
            }
            try
            {


                // Create a model for database
                var page = new SyllabusDTO
                {
                    Title = model.Title,
                    Content = model.Content,
                    FilePath = filePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _syllabusService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] SyllabusDTO model)
        {
            var existingPage = await _syllabusService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Syllabus not found."
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
                                "Syllabus"

                            );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }
            try
            {

                var page =
                    new SyllabusDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Content = model.Content,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _syllabusService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] SyllabusDTO model)
        {
            try
            {
                var result = await _syllabusService.deleteAsync(model);
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
