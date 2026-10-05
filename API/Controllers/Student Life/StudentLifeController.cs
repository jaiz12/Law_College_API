using BAL.Services.Committee_and_Cell;
using BAL.Services.Student_Life.Student_Life;
using Common.DataContext;
using DTO.Models.Committee_and_Cell;
using DTO.Models.Student_Life;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Student_Life
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentLifeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentLifeService _studentLifeService;

        public StudentLifeController (IStudentLifeService studentLifeService, ApplicationDbContext context)
        {
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

            try
            {


                // Create a model for database
                var page =
                    new StudentLifeDTO
                    {
                        PageName = model.PageName,
                        Content = model.Content,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _studentLifeService.CreateAsync(page);

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

            try
            {


                var page =
                    new StudentLifeDTO
                    {
                        Id = model.Id,
                        PageName = model.PageName,
                        Content = model.Content,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _studentLifeService.UpdateAsync(page);
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
