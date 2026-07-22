using API.Controllers.Services;
using BAL.Services.About.Recognitions_And_Affiliations;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecognitionsAndAffiliationsController : Controller
    {

        private readonly IFileUploadService _fileUpload;
        private readonly IRecognitionsAndAffiliationsService _recognitionsAndAffiliationsService;
        public RecognitionsAndAffiliationsController( IFileUploadService fileUpload, IRecognitionsAndAffiliationsService recognitionsAndAffiliationsService)
        {
            _fileUpload = fileUpload;
            _recognitionsAndAffiliationsService = recognitionsAndAffiliationsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _recognitionsAndAffiliationsService.GetAllAsync();
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
                var result = await _recognitionsAndAffiliationsService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] RecognitionsAndAffiliationsDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Image != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Image,
                        "About",
                        "Recognitions and Affiliations"
                    );
            }
            try
            {


                // Create a model for database
                var page = new RecognitionsAndAffiliationsDTO
                {
                    Title = model.Title,
                    Description = model.Description,
                    ExternalUrl = model.ExternalUrl,
                    CoverImage = imagePath,
                    DisplayOrder = model.DisplayOrder,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _recognitionsAndAffiliationsService.CreateAsync(page);
                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(imagePath);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _fileUpload.Delete(imagePath);
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] RecognitionsAndAffiliationsDTO model)
        {
            var existingPage = await _recognitionsAndAffiliationsService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Recognitions and Affiliations not found."
                });
            }

            var row = existingPage.Rows[0];


            string? oldImage =
                row["CoverImage"] == DBNull.Value
                    ? null
                    : row["CoverImage"].ToString();

            string? imagePath = oldImage;

            if (model.Image != null)
            {
                imagePath = await _fileUpload.UploadAsync(
                                model.Image,
                                "About",
                                "Recognitions and Affiliations"

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page =
                    new RecognitionsAndAffiliationsDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Description = model.Description,
                        ExternalUrl = model.ExternalUrl,
                        CoverImage = imagePath,
                        DisplayOrder = model.DisplayOrder,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _recognitionsAndAffiliationsService.UpdateAsync(page);

                if (!result.IsSucceeded)
                {
                    _fileUpload.Delete(imagePath);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                _fileUpload.Delete(imagePath);
                return BadRequest(new
                {
                    message =
                        ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] RecognitionsAndAffiliationsDTO model)
        {
            try
            {
                var result = await _recognitionsAndAffiliationsService.deleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.CoverImage != null)
                    {
                        _fileUpload.Delete(model.CoverImage);
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
