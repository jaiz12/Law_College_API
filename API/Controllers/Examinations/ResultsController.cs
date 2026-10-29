using API.Controllers.Services;
using BAL.Services.Examinations.Results;
using DTO.Models.Admissions;
using DTO.Models.Examinations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Examinations
{
    [ApiController] 
    [Route("api/[controller]")]
    public class ResultsController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        public readonly IResultsService _resultsService;

        public ResultsController(IFileUploadService fileUpload, IResultsService resultsService)
        {
            _fileUpload = fileUpload;
            _resultsService = resultsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _resultsService.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] ResultsDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        "Results",
                        model.File,
                        "Examinations",
                        "Results"
                    );
            }

            try
            {


                // Create a model for database
                var page =
                    new ResultsDTO
                    {
                        Content = model.Content,
                        FilePath = filePath,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _resultsService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] ResultsDTO model)
        {
            var existingPage = await _resultsService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Results not found."
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
                            "Results",
                            model.File,
                            "Examinations",
                            "Results"
                        );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }

            try
            {


                var page =
                    new ResultsDTO
                    {
                        Id = model.Id,
                        Content = model.Content,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _resultsService.UpdateAsync(page);

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
