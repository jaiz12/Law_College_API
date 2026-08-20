using BAL.Services.Home;
using DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Home
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        // =========================================================
        // GET
        // =========================================================

        [HttpGet("{PageName}")]
        public async Task<IActionResult> Get(
            string PageName)
        {
            try
            {
                var result =
                    await _homeService.GetAsync(
                       PageName
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
        // CREATE
        // =========================================================

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] HomeDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid home details."
                    });
                }

                var result =
                    await _homeService.CreateAsync(
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
            [FromForm] HomeDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid home details."
                    });
                }

                if (model.Id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid home Id."
                    });
                }


                var result =
                    await _homeService.UpdateAsync(
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

        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromForm] HomeDTO model)
        {
            try
            {

                var result =
                    await _homeService.DeleteAsync(model);

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
