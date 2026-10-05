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
        public readonly IResultsService _resultsService;

        public ResultsController(IResultsService resultsService)
        {
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

            try
            {


                // Create a model for database
                var page =
                    new ResultsDTO
                    {
                        Content = model.Content,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _resultsService.CreateAsync(page);

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

            try
            {


                var page =
                    new ResultsDTO
                    {
                        Id = model.Id,
                        Content = model.Content,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _resultsService.UpdateAsync(page);
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
