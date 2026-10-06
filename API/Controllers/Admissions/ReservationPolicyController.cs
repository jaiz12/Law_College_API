using API.Controllers.Services;
using BAL.Services.Admissions.Eligibility_Admission_Process_And_Intake;
using BAL.Services.Admissions.Reservation_Policy;
using DTO.Models.Admissions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admissions
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationPolicyController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IReservationPolicyService _reservationPolicyService;

        public ReservationPolicyController(IFileUploadService fileUpload, IReservationPolicyService reservationPolicyService)
        {
            _fileUpload = fileUpload;
            _reservationPolicyService = reservationPolicyService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _reservationPolicyService.GetAsync();
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
                var result = await _reservationPolicyService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] ReservationPolicyDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        "Reservation Policy",
                        model.File,
                        "Admissions",
                        "Reservation Policy"
                    );
            }
            try
            {


                // Create a model for database
                var page =
                    new ReservationPolicyDTO
                    {
                        FilePath = filePath,
                        Content = model.Content,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _reservationPolicyService.CreateAsync(page);
                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(filePath);
                }
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
        public async Task<IActionResult> Update([FromForm] ReservationPolicyDTO model)
        {
            var existingPage = await _reservationPolicyService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Reservation Policy not found."
                });
            }

            var row = existingPage.Rows[0];


            string? oldfilePath =
                row["FilePath"] == DBNull.Value
                    ? null
                    : row["FilePath"].ToString();

            string? filePath = oldfilePath;

            if (model.File != null)
            {
                filePath = await _fileUpload.UploadAsync(
                                "Reservation Policy",
                                model.File,
                                "Admissions",
                                "Reservation Policy"

                            );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }

            try
            {


                var page =
                    new ReservationPolicyDTO
                    {
                        Id = model.Id,
                        FilePath = filePath,
                        Content = model.Content,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _reservationPolicyService.UpdateAsync(page);
                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(filePath);
                }
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
