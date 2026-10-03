using API.Controllers.Services;
using BAL.Services.About.Statutory_Bodies;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatutoryBodiesController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IStatutoryBodiesService _istatutoryBodiesService;

        public StatutoryBodiesController(IFileUploadService fileUpload, IStatutoryBodiesService statutoryBodiesService)
        {
            _fileUpload = fileUpload;
            _istatutoryBodiesService = statutoryBodiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _istatutoryBodiesService.GetAllAsync();
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
                var result = await _istatutoryBodiesService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] StatutoryBodiesDTO model)
        {
            
            try
            {


                // Create a model for database
                var page = new StatutoryBodiesDTO
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _istatutoryBodiesService.CreateAsync(page);

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
        public async Task<IActionResult> Update([FromForm] StatutoryBodiesDTO model)
        {
            
            try
            {

                var page = new StatutoryBodiesDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    UpdatedBy = model.UpdatedBy,
                };

                var result = await _istatutoryBodiesService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                var result = await _istatutoryBodiesService.deleteAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
