using API.Controllers.Services;
using BAL.Services.Academics.Academic_Policies;
using BAL.Services.Student_Life.Student_Life;
using Common.DataContext;
using DTO.Models.Academics;
using DTO.Models.Student_Life;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Academics
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademicPoliciesController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly ApplicationDbContext _context;
        private readonly IAcademicPoliciesService _academicPoliciesService;

        public AcademicPoliciesController(IFileUploadService fileUpload, IAcademicPoliciesService academicPoliciesService, ApplicationDbContext context)
        {
            _fileUpload = fileUpload;
            _context = context;
            _academicPoliciesService = academicPoliciesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _academicPoliciesService.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AcademicPoliciesDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        "Academic Policies",
                        model.File,
                        "Academic",
                        "Academic Policies"
                    );
            }
            try
            {


                // Create a model for database
                var page =
                    new AcademicPoliciesDTO
                    {
                        Content = model.Content,
                        FilePath = filePath,
                        CreatedBy = model.CreatedBy,

                    };


                // Call business service
                var result = await _academicPoliciesService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] AcademicPoliciesDTO model)
        {
            var existingPage = await _academicPoliciesService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Academic Policies not found."
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
                        "Academic Policies",
                        model.File,
                        "Academic",
                        "Academic Policies"
                    );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }

            try
            {


                var page =
                    new AcademicPoliciesDTO
                    {
                        Id = model.Id,
                        Content = model.Content,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _academicPoliciesService.UpdateAsync(page);

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
