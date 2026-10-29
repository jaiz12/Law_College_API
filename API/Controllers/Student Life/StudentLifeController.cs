using API.Controllers.Services;
using BAL.Services.Committee_and_Cell;
using BAL.Services.Student_Life.Student_Life;
using Common.DataContext;
using DTO.Models.Committee_and_Cell;
using DTO.Models.Student_Life;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.Controllers.Student_Life
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentLifeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUpload;
        private readonly IStudentLifeService _studentLifeService;

        public StudentLifeController (IFileUploadService fileUpload, IStudentLifeService studentLifeService, ApplicationDbContext context)
        {
            _fileUpload = fileUpload;
            _context = context;
            _studentLifeService = studentLifeService;
        }

        [HttpGet("{Id}/{PageName}")]
        public async Task<IActionResult> Get(int Id, string PageName)
        {
            try
            {
                var result = await _studentLifeService.GetAsync(Id, PageName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] StudentLifeDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        model.PageName,
                        model.File,
                        "Student Life",
                        model.PageName
                    );
            }

            try
            {


                // Create a model for database
                var page =
                    new StudentLifeDTO
                    {
                        PageName = model.PageName,
                        Content = model.Content,
                        FilePath = filePath,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _studentLifeService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] StudentLifeDTO model)
        {
            var existingPage = await _studentLifeService.GetAsync(model.Id, model.PageName);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Student Life not found."
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
                        "Student Life",
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
                    new StudentLifeDTO
                    {
                        Id = model.Id,
                        PageName = model.PageName,
                        Content = model.Content,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _studentLifeService.UpdateAsync(page);
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
