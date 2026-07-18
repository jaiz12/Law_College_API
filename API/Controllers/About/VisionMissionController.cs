using API.Controllers.Services;
using BAL.Services.About.VisionMission;
using Common.DataContext;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisionMissionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVisionMissionService _visionMissionService ;

        public VisionMissionController(
            ApplicationDbContext context, IFileUploadService fileUpload, IVisionMissionService visionMissionService)
        {
            _context = context;
            _visionMissionService = visionMissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _visionMissionService.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] VisionMissionDTO model)
        {
            try
            {

                // Call business service
                var result = await _visionMissionService.CreateAsync(model);


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
        public async Task<IActionResult> Update(
    [FromForm] VisionMissionDTO model)
        {
            try
            {                

                var result =
                    await _visionMissionService
                        .UpdateAsync(model);


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
