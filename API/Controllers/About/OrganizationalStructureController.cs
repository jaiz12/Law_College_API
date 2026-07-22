using API.Controllers.Services;
using BAL.Services.About.About_Us;
using BAL.Services.About.Organizational_Structure;
using Common.DataContext;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationalStructureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUpload;
        private readonly IOrganizationalStructureService _organizationalStructureService;

        public OrganizationalStructureController(
           ApplicationDbContext context, IFileUploadService fileUpload, IOrganizationalStructureService organizationalStructureService)
        {
            _context = context;
            _fileUpload = fileUpload;
            _organizationalStructureService = organizationalStructureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _organizationalStructureService.GetAllAsync();
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
                var result = await _organizationalStructureService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] OrganizationalStructureDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Photo,
                        "About",
                        "Organizational Structure"
                    );
            }
            try
            {           


                // Create a model for database
                var page = new OrganizationalStructureDTO
                    {
                        Name = model.Name,
                        Designation = model.Designation,
                        Email = model.Email,
                        Phone   = model.Phone,
                        ParentId = model.ParentId,
                        ProfilePhoto = imagePath,
                        DisplayOrder = model.DisplayOrder,
                        CreatedBy   = model.CreatedBy,
                    };


                // Call business service
                var result = await _organizationalStructureService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] OrganizationalStructureDTO model)
        {
            var existingPage = await _organizationalStructureService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Organizational Structure not found."
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
                                "Organizational Structure"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {
                


                var page =
                    new OrganizationalStructureDTO
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


                var result = await _organizationalStructureService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] OrganizationalStructureDTO model)
        {
            try
            {
                var result = await _organizationalStructureService.deleteAsync(model);
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
