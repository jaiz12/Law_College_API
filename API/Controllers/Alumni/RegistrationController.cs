using API.Controllers.Services;
using BAL.Services.Alumni.Registration;
using DTO.Models.Alumni;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Controllers.Alumni
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IRegistrationService _registrationService;
        public RegistrationController(IRegistrationService registrationService, IFileUploadService fileUploadService) {
            _fileUpload = fileUploadService;    
            _registrationService = registrationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _registrationService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
           [FromForm] RegistrationDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.FullName,
                        model.Photo,
                        "Alumni",
                        "Registration"
                    );
            }
            try
            {


                // Create a model for database
                var page = new RegistrationDTO
                {
                    RegistrationId= model.RegistrationId,
                    FullName = model.FullName,
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    CourseProgramme = model.CourseProgramme,
                    BatchGraduationYear = model.BatchGraduationYear,
                    CurrentProfessionRole = model.CurrentProfessionRole,
                    CurrentOrganisationChamber = model.CurrentOrganisationChamber,
                    CurrentCityLocation = model.CurrentCityLocation,
                    ConnectionPreferences = model.ConnectionPreferences,
                    LinkedInProfile = model.LinkedInProfile,
                    ProfilePhoto = imagePath
                };


                // Call business service
                var result = await _registrationService.CreateAsync(page);
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

    }
}
