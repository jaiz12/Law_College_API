using API.Controllers.Services;
using BAL.Services.Examinations;
using BAL.Services.Examinations;
using DTO.Models.Examinations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Examinations
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly INotificationsService  _notificationsService;
        public NotificationsController(IFileUploadService fileUpload, INotificationsService notificationsService)
        {
            _fileUpload = fileUpload;
            _notificationsService = notificationsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _notificationsService.GetAllAsync();
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
                var result = await _notificationsService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] NotificationsDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        model.Title,
                        model.File,
                        "Examinations",
                        "Notifications"
                    );
            }
            try
            {


                // Create a model for database
                var page = new NotificationsDTO
                {
                    Title = model.Title,
                    FilePath = filePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _notificationsService.CreateAsync(page);
                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(filePath);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _fileUpload.Delete(filePath);
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] NotificationsDTO model)
        {
            var existingPage = await _notificationsService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Notifications not found."
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
                        model.Title,
                                model.File,
                                "Examinations",
                                "Notifications"

                            );

                if (!string.IsNullOrWhiteSpace(oldfilePath))
                {
                    _fileUpload.Delete(oldfilePath);
                }
            }
            try
            {

                var page =
                    new NotificationsDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        FilePath = filePath,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _notificationsService.UpdateAsync(page);

                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(filePath);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                _fileUpload.Delete(filePath);
                return BadRequest(new
                {
                    message =
                        ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] NotificationsDTO model)
        {
            try
            {
                var result = await _notificationsService.deleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.FilePath != null)
                    {
                        _fileUpload.Delete(model.FilePath);
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
