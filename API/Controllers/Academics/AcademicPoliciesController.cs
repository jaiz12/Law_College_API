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
        private readonly ApplicationDbContext _context;
        private readonly IAcademicPoliciesService _academicPoliciesService;

        public AcademicPoliciesController(IAcademicPoliciesService academicPoliciesService, ApplicationDbContext context)
        {
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

            try
            {


                // Create a model for database
                var page =
                    new AcademicPoliciesDTO
                    {
                        Content = model.Content,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _academicPoliciesService.CreateAsync(page);

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

            try
            {


                var page =
                    new AcademicPoliciesDTO
                    {
                        Id = model.Id,
                        Content = model.Content,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _academicPoliciesService.UpdateAsync(page);
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
