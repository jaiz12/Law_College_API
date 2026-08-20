using BAL.Services.Committee_and_Cell.Legal_Aid_Cell;
using DTO.Models.Committee_and_Cell;
using DTO.Models.Student_Life;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Committee_and_Cell
{

    [ApiController]
    [Route("api/[controller]")]
    public class LegalAidCellController : Controller
    {
        private readonly ILegalAidCellService _legalAidCellService;
        public LegalAidCellController (ILegalAidCellService legalAidCellService)
        {
            _legalAidCellService = legalAidCellService;
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
                    await _legalAidCellService.GetAsync();

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
            [FromForm] LegalAidCellDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid Legal Aid Cell details."
                    });
                }

                var result =
                    await _legalAidCellService.CreateAsync(
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
            [FromForm] LegalAidCellDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid Legal Aid Cell details."
                    });
                }

                if (model.Id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid Legal Aid Cell Id."
                    });
                }


                var result =
                    await _legalAidCellService.UpdateAsync(
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
                    await _legalAidCellService.DeleteAsync(Id);

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
