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
        private readonly ICommitteeAndCellService _committeeAndCellService;
        public CommitteeAndCellController(
            ApplicationDbContext context, ICommitteeAndCellService committeeAndCellService)
        {
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

            try
            {


                // Create a model for database
                var page =
                    new CommitteeAndCellDTO
                    {
                        PageName = model.PageName,
                        Content = model.Content,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _committeeAndCellService.CreateAsync(page);

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

           try
            {


                var page =
                    new CommitteeAndCellDTO
                    {
                        Id = model.Id,
                        PageName = model.PageName,
                        Content = model.Content,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _committeeAndCellService.UpdateAsync(page);
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
