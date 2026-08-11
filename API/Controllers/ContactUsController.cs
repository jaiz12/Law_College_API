using API.Controllers.Services;
using BAL.Services.About.About_Us;
using BAL.Services.ContactUs;
using Common.DataContext;
using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _contactUsService;
        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }
        // =========================================================
        // GET
        // =========================================================

        [HttpGet("{Id}/{SectionName}")]
        public async Task<IActionResult> Get(
            int Id,
            string SectionName)
        {
            try
            {
                var result =
                    await _contactUsService.GetAsync(
                        Id,
                        SectionName
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
            [FromForm] ContactUsDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid contact details."
                    });
                }

                var result =
                    await _contactUsService.CreateAsync(
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
            [FromForm] ContactUsDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = "Invalid contact details."
                    });
                }

                if (model.Id <= 0)
                {
                    return BadRequest(new
                    {
                        message = "Invalid contact Id."
                    });
                }


                var result =
                    await _contactUsService.UpdateAsync(
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
                    await _contactUsService.DeleteAsync(Id);

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
