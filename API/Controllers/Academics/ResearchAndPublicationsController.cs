using API.Controllers.Services;
using BAL.Services.Academics.Research_And_Publications;
using BAL.Services.Academics.Syllabus;
using DTO.Models.About;
using DTO.Models.Academics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Academics
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResearchAndPublicationsController : Controller
    {
        private readonly IResearchAndPublicationsService _researchAndPublicationsService;
        public ResearchAndPublicationsController(IResearchAndPublicationsService researchAndPublicationsService)
        {
            _researchAndPublicationsService = researchAndPublicationsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _researchAndPublicationsService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] ResearchAndPublicationsDTO model)
        {
            try
            {


                // Create a model for database
                var page = new ResearchAndPublicationsDTO
                {
                    Title = model.Title,
                    Description = model.Description,
                    Link = model.Link,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _researchAndPublicationsService.CreateAsync(page);
                
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
        public async Task<IActionResult> Update([FromForm] ResearchAndPublicationsDTO model)
        {
            try
            {



                var page =
                    new ResearchAndPublicationsDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Description = model.Description,
                        Link = model.Link,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _researchAndPublicationsService.UpdateAsync(page);

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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var result = await _researchAndPublicationsService.deleteAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
