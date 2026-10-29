using API.Controllers.Services;
using BAL.Services.About.About_Us;
using BAL.Services.Committee_and_Cell;
using Common.DataContext;
using DTO.Models.About;
using DTO.Models.Committee_and_Cell;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Committee_and_Cell
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommitteeAndCellController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUpload;
        private readonly ICommitteeAndCellService _committeeAndCellService;
        public CommitteeAndCellController(IFileUploadService fileUpload,
            ApplicationDbContext context, ICommitteeAndCellService committeeAndCellService)
        {
            _fileUpload = fileUpload;
            _context = context;
            _committeeAndCellService = committeeAndCellService;
        }

        [HttpGet("{Id}/{PageName}")]
        public async Task<IActionResult> Get(int Id, string PageName)
        {
            try
            {
                var result = await _committeeAndCellService.GetAsync(Id, PageName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] CommitteeAndCellDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        model.PageName,
                        model.File,
                        "Committee And Cell",
                        model.PageName
                    );
            }
            try
            {


                // Create a model for database
                var page =
                    new CommitteeAndCellDTO
                    {
                        PageName = model.PageName,
                        Content = model.Content,
                        FilePath = filePath,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _committeeAndCellService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] CommitteeAndCellDTO model)
        {
            var existingPage = await _committeeAndCellService.GetAsync(model.Id, model.PageName);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Committee And Cell not found."
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
                        "Committee And Cell",
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
                    new CommitteeAndCellDTO
                    {
                        Id = model.Id,
                        PageName = model.PageName,
                        Content = model.Content,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _committeeAndCellService.UpdateAsync(page);
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
