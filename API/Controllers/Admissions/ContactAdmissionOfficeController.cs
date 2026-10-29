using API.Controllers.Services;
using BAL.Services.Admissions.Contact_Admission_Office;
using DTO.Models.Academics;
using DTO.Models.Admissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admissions
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactAdmissionOfficeController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IContactAdmissionOfficeService _contactAdmissionOfficeService;

        public ContactAdmissionOfficeController(IFileUploadService fileUpload, IContactAdmissionOfficeService contactAdmissionOfficeService)
        {
            _fileUpload = fileUpload;
            _contactAdmissionOfficeService = contactAdmissionOfficeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _contactAdmissionOfficeService.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] ContactAdmissionOfficesDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        "Contact Admission Offices",
                        model.File,
                        "Admission",
                        "Contact Admission Offices"
                    );
            }

            try
            {


                // Create a model for database
                var page =
                    new ContactAdmissionOfficesDTO
                    {
                        Content = model.Content,
                        FilePath = filePath,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _contactAdmissionOfficeService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] ContactAdmissionOfficesDTO model)
        {
            var existingPage = await _contactAdmissionOfficeService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Contact Admission Offices not found."
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
                        "Contact Admission Offices",
                        model.File,
                        "Admission",
                        "Contact Admission Offices"
                    );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }
            try
            {


                var page =
                    new ContactAdmissionOfficesDTO
                    {
                        Id = model.Id,
                        Content = model.Content,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _contactAdmissionOfficeService.UpdateAsync(page);

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
