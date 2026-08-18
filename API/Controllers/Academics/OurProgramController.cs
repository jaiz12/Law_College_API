using BAL.Services.Academics.Our_Program;
using DTO.Models;
using DTO.Models.Academics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Academics
{
    [ApiController]
    [Route("api/[controller]")]
    public class OurProgramController : Controller
    {

        private readonly IOurProgramService _ourProgramService;
        public OurProgramController(IOurProgramService ourProgramService)
        {
            _ourProgramService = ourProgramService;
        }
        // =========================================================
        // GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =
                    await _ourProgramService.GetAsync();

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


        // =========================================================
        // CREATE
        // =========================================================

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] OurProgramDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid our program details."
                    });
                }

                var result =
                    await _ourProgramService.CreateAsync(
                        model
                    );

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
        // =========================================================
        // UPDATE
        // =========================================================

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromForm] OurProgramDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid our program details."
                    });
                }

                if (model.Id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid our program Id."
                    });
                }


                var result =
                    await _ourProgramService.UpdateAsync(
                        model
                    );

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


        // =========================================================
        // DELETE
        // =========================================================

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(
            string Id)
        {
            try
            {

                var result =
                    await _ourProgramService.DeleteAsync(Id);

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
    }
}
