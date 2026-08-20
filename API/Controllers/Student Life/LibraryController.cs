
using BAL.Services.Student_Life.Library;
using DTO.Models;
using DTO.Models.Student_Life;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Student_Life
{

    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
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
                    await _libraryService.GetAsync();

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
            [FromForm] LibraryDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid library details."
                    });
                }

                var result =
                    await _libraryService.CreateAsync(
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
            [FromForm] LibraryDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid library details."
                    });
                }

                if (model.Id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid library Id."
                    });
                }


                var result =
                    await _libraryService.UpdateAsync(
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
                    await _libraryService.DeleteAsync(Id);

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
