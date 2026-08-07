using API.Controllers.Services;
using BAL.Services.About.Administrative_Staff;
using BAL.Services.About.Faculty;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministrativeStaffController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IAdministrativeStaffService _administrativeStaffService;

        public AdministrativeStaffController(IFileUploadService fileUpload, IAdministrativeStaffService administrativeStaffService)
        {
            _fileUpload = fileUpload;
            _administrativeStaffService = administrativeStaffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _administrativeStaffService.GetAllAsync();
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
                var result = await _administrativeStaffService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AdministrativeStaffDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Photo,
                        "About",
                        "Administrative Staff"
                    );
            }
            try
            {


                // Create a model for database
                var page = new AdministrativeStaffDTO
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
                var result = await _administrativeStaffService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] AdministrativeStaffDTO model)
        {
            var existingPage = await _administrativeStaffService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Administrative Staff not found."
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
                                "Administrative Staff"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page =
                    new AdministrativeStaffDTO
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


                var result = await _administrativeStaffService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] AdministrativeStaffDTO model)
        {
            try
            {
                var result = await _administrativeStaffService.deleteAsync(model);
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
