using API.Controllers.Services;
using BAL.Services.About.Recognitions_And_Affiliations;
using BAL.Services.News_and_Events.Announcemets;
using DTO.Models.About;
using DTO.Models.News_and_Events;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.News_and_Events
{

    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IAnnouncementsService _announcemetsService;
        public AnnouncementsController(IFileUploadService fileUpload, IAnnouncementsService announcemetsService)
        {
            _fileUpload = fileUpload;
            _announcemetsService = announcemetsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIsActive()
        {
            try
            {
                var result = await _announcemetsService.GetAllIsActiveAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("ArchiveNewsAndEvents")]
        public async Task<IActionResult> GetAllInActive()
        {
            try
            {
                var result = await _announcemetsService.GetAllInActiveAsync();
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
                var result = await _announcemetsService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AnnouncementsDTO model)
        {
            string? filePath = null;

            // Upload image
            if (model.File != null)
            {
                filePath =
                    await _fileUpload.UploadAsync(
                        model.File,
                        "News and Events",
                        "Announcemets"
                    );
            }
            try
            {


                // Create a model for database
                var page = new AnnouncementsDTO
                {
                    Title = model.Title,
                    Category = model.Category,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    FilePath = filePath,
                    Urgent = model.Urgent,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _announcemetsService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] AnnouncementsDTO model)
        {
            var existingPage = await _announcemetsService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Announcemets not found."
                });
            }

            var row = existingPage.Rows[0];


            string? oldFilePath =
                row["FilePath"] == DBNull.Value
                    ? null
                    : row["FilePath"].ToString();

            string? filePath = oldFilePath;

            if (model.File != null)
            {
                filePath = await _fileUpload.UploadAsync(
                                model.File,
                                "News and Events",
                                "Announcemets"

                            );

                if (!string.IsNullOrWhiteSpace(oldFilePath))
                {
                    _fileUpload.Delete(oldFilePath);
                }
            }
            try
            {



                var page =
                    new AnnouncementsDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Category = model.Category,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        FilePath = filePath,
                        Urgent = model.Urgent,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _announcemetsService.UpdateAsync(page);

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



        [HttpPut("{Id}/{UpdatedBy}")]
        public async Task<IActionResult> Archive(string Id, string UpdatedBy)
        {
            try
            {


                var result = await _announcemetsService.ArchiveAsync(Id, UpdatedBy);

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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] AnnouncementsDTO model)
        {
            try
            {
                var result = await _announcemetsService.deleteAsync(model);
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
