using API.Controllers.Services;
using BAL.Services.Compliance_Or_Disclosures;
using DTO.Models.Admissions;
using DTO.Models.Compliance_Or_Disclosures;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Compliance_Or_Disclosures
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplianceOrDisclosuresController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IComplianceOrDisclosuresService _complianceOrDisclosuresService;

        public ComplianceOrDisclosuresController(IFileUploadService fileUpload, IComplianceOrDisclosuresService complianceOrDisclosuresService)
        {
            _fileUpload = fileUpload;
            _complianceOrDisclosuresService = complianceOrDisclosuresService;
        }


        [HttpGet("{PageName}")]
        public async Task<IActionResult> Get(string PageName)
        {
            try
            {
                var result = await _complianceOrDisclosuresService.GetAsync(PageName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] ComplianceOrDisclosuresDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                                model.PageName,
                                model.File,
                                "Compliance Or Disclosures",
                                model.PageName
                    );
            }
            try
            {


                // Create a model for database
                var page =
                    new ComplianceOrDisclosuresDTO
                    {
                        PageName = model.PageName,
                        FilePath = filePath,
                        Content = model.Content,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _complianceOrDisclosuresService.CreateAsync(page);
                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(filePath);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ComplianceOrDisclosuresDTO model)
        {
            var existingPage = await _complianceOrDisclosuresService.GetAsync(model.PageName);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        $"{model.PageName} not found."
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
                                model.PageName,
                                model.File,
                                "Compliance Or Disclosures",
                                model.PageName
                           );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }

            try
            {


                var page =
                    new ComplianceOrDisclosuresDTO
                    {
                        Id = model.Id,
                        PageName = model.PageName,
                        FilePath = filePath,
                        Content = model.Content,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _complianceOrDisclosuresService.UpdateAsync(page);
                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(filePath);
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message =
                        ex.Message
                });
            }
        }
    }
}
