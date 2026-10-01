using API.Controllers.Services;
using BAL.Services.Academics.Syllabus;
using BAL.Services.Admissions;
using DTO.Models.Academics;
using DTO.Models.Admissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admissions
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProspectusController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IProspectusService _prospectusService;
        public ProspectusController(IFileUploadService fileUpload, IProspectusService prospectusService)
        {
            _fileUpload = fileUpload;
            _prospectusService = prospectusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _prospectusService.GetAllAsync();
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
                var result = await _prospectusService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] ProspectusDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        model.File,
                        "Admissions",
                        "Prospectus"
                    );
            }
            try
            {


                // Create a model for database
                var page = new ProspectusDTO
                {
                    Title = model.Title,
                    FilePath = filePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _prospectusService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] ProspectusDTO model)
        {
            var existingPage = await _prospectusService.GetByIdAsync(model.Id);


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
                                "Admissions",
                                "Prospectus"

                            );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }
            try
            {

                var page =
                    new ProspectusDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _prospectusService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] ProspectusDTO model)
        {
            try
            {
                var result = await _prospectusService.deleteAsync(model);
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
