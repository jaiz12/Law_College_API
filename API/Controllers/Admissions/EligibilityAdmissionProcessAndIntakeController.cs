using API.Controllers.Services;
using BAL.Services.Admissions.Eligibility_Admission_Process_And_Intake;
using DTO.Models.Admissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admissions
{
    [ApiController]
    [Route("api/[controller]")]
    public class EligibilityAdmissionProcessAndIntakeController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IEligibilityAdmissionProcessAndIntakeService _eligibilityAdmissionProcessAndIntakeService;

        public EligibilityAdmissionProcessAndIntakeController(IFileUploadService fileUpload, IEligibilityAdmissionProcessAndIntakeService eligibilityAdmissionProcessAndIntakeService)
        {
            _fileUpload = fileUpload;
            _eligibilityAdmissionProcessAndIntakeService = eligibilityAdmissionProcessAndIntakeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _eligibilityAdmissionProcessAndIntakeService.GetAllAsync();
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
                var result = await _eligibilityAdmissionProcessAndIntakeService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] EligibilityAdmissionProcessAndIntakeDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        model.Title,
                        model.File,
                        "Admissions",
                        "Eligibility Admission Process And Intake"
                    );
            }
            try
            {


                // Create a model for database
                var page = new EligibilityAdmissionProcessAndIntakeDTO
                {
                    Title = model.Title,
                    FilePath = filePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _eligibilityAdmissionProcessAndIntakeService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] EligibilityAdmissionProcessAndIntakeDTO model)
        {
            var existingPage = await _eligibilityAdmissionProcessAndIntakeService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Eligibility Admission Process And Intake not found."
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
                    model.Title,
                                model.File,
                                "Admissions",
                                "Eligibility Admission Process And Intake"

                            );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }
            try
            {

                var page =
                    new EligibilityAdmissionProcessAndIntakeDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _eligibilityAdmissionProcessAndIntakeService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] EligibilityAdmissionProcessAndIntakeDTO model)
        {
            try
            {
                var result = await _eligibilityAdmissionProcessAndIntakeService.deleteAsync(model);
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
