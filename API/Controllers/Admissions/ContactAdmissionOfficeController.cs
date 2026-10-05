using BAL.Services.Admissions.Contact_Admission_Office;
using DTO.Models.Academics;
using DTO.Models.Admissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admissions
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactAdmissionOfficeController : Controller
    {
        private readonly IContactAdmissionOfficeService _contactAdmissionOfficeService;

        public ContactAdmissionOfficeController(IContactAdmissionOfficeService contactAdmissionOfficeService)
        {
            _contactAdmissionOfficeService = contactAdmissionOfficeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _contactAdmissionOfficeService.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] ContactAdmissionOfficesDTO model)
        {

            try
            {


                // Create a model for database
                var page =
                    new ContactAdmissionOfficesDTO
                    {
                        Content = model.Content,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _contactAdmissionOfficeService.CreateAsync(page);

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
        public async Task<IActionResult> Update([FromForm] ContactAdmissionOfficesDTO model)
        {

            try
            {


                var page =
                    new ContactAdmissionOfficesDTO
                    {
                        Id = model.Id,
                        Content = model.Content,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _contactAdmissionOfficeService.UpdateAsync(page);
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
