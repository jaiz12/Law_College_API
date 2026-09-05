using API.Controllers.Services;
using BAL.Services.About.Faculty;
using BAL.Services.Banner;
using DTO.Models.About;
using DTO.Models.Banner;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Banner
{
    [ApiController]
    [Route("api/[controller]")]
    public class BannerController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IBannerService _bannerService;

        public BannerController(IFileUploadService fileUpload, IBannerService bannerService)
        {
            _fileUpload = fileUpload;
            _bannerService = bannerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _bannerService.GetAllAsync();
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
                var result = await _bannerService.GetByIdAsync(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] BannerDTO model)
        {
            string? imagePath = null;

            // Upload image
            if (model.Image != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Image,
                        "Banner",
                        ""
                    );
            }
            try
            {


                // Create a model for database
                var page = new BannerDTO
                {
                    PageName = model.PageName,
                    Content = model.Content,
                    ImagePath = imagePath,
                    CreatedBy = model.CreatedBy,
                };


                // Call business service
                var result = await _bannerService.CreateAsync(page);
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
        public async Task<IActionResult> Update([FromForm] BannerDTO model)
        {
            var existingPage = await _bannerService.GetByIdAsync(model.Id);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "Banner not found."
                });
            }

            var row = existingPage.Rows[0];


            string? oldImage =
                row["ImagePath"] == DBNull.Value
                    ? null
                    : row["ImagePath"].ToString();

            string? imagePath = oldImage;

            if (model.Image != null)
            {
                imagePath = await _fileUpload.UploadAsync(
                                model.Image,
                                "Banner",
                                ""

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }
            try
            {



                var page = new BannerDTO
                {
                    Id = model.Id,
                    PageName = model.PageName,
                    Content = model.Content,
                    ImagePath = imagePath,
                    CreatedBy = model.CreatedBy,
                };


                var result = await _bannerService.UpdateAsync(page);

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
        public async Task<IActionResult> Delete([FromForm] BannerDTO model)
        {
            try
            {
                var result = await _bannerService.deleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.ImagePath != null)
                    {
                        _fileUpload.Delete(model.ImagePath);
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
