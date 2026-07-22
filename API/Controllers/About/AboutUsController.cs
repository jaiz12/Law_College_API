using API.Controllers.Services;
using BAL.Services.About.About_Us;
using Common.DataContext;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutUsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUpload;
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(
            ApplicationDbContext context, IFileUploadService fileUpload, IAboutUsService aboutUsService)
        {
            _context = context;
            _fileUpload = fileUpload;
            _aboutUsService = aboutUsService;
        }

        [HttpGet("{Id}/{PageName}")]
        public async Task<IActionResult> Get(int Id, string PageName)
        {
            try
            {
                var result = await _aboutUsService.GetAsync(Id, PageName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] AboutUsDTO model)
        {
            string? bannerPath = null;
            string? imagePath = null;


            // Upload banner image
            if (model.Banner != null)
            {
                bannerPath =
                    await _fileUpload.UploadAsync(
                        model.Banner,
                        "About",
                        model.PageName
                    );
            }
            // Upload image
            if (model.Photo != null)
            {
                imagePath =
                    await _fileUpload.UploadAsync(
                        model.Photo,
                        "About",
                        model.PageName
                    );
            }

            try
            {
                

                // Create a model for database
                var page =
                    new AboutUsDTO
                    {
                        PageName = model.PageName,
                        BannerImage = bannerPath,
                        Image = imagePath,
                        Name = model.Name,
                        Description = model.Description,
                        MetaTitle = model.MetaTitle,
                        MetaDescription = model.MetaDescription,
                        CreatedBy = model.CreatedBy
                    };


                // Call business service
                var result = await _aboutUsService.CreateAsync(page);

                if (!result.IsSucceeded)
                {
                    if (model.Banner != null)
                    {
                        _fileUpload.Delete(bannerPath);
                    }
                    if (model.Photo != null)
                    {
                        _fileUpload.Delete(imagePath);
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                if (model.Banner != null)
                {
                    _fileUpload.Delete(bannerPath);
                }
                if (model.Photo != null)
                {
                    _fileUpload.Delete(imagePath);
                }
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] AboutUsDTO model)
        {

            var existingPage = await _aboutUsService.GetAsync(model.Id, model.PageName);


            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "General Overview not found."
                });
            }

            var row = existingPage.Rows[0];

            string? oldBannerImage =
                row["BannerImage"] == DBNull.Value
                    ? null
                    : row["BannerImage"].ToString();

            string? bannerPath = oldBannerImage;

            string? oldImage =
                row["Image"] == DBNull.Value
                    ? null
                    : row["Image"].ToString();

            string? imagePath = oldImage;


            if (model.Banner != null)
            {
                bannerPath = await _fileUpload.UploadAsync(
                                model.Banner,
                                "About",
                                model.PageName

                            );
                if (!string.IsNullOrWhiteSpace(oldBannerImage))
                {
                    _fileUpload.Delete(
                        oldBannerImage
                    );
                }
            }

            if (model.Photo != null)
            {
                imagePath = await _fileUpload.UploadAsync(
                                model.Photo,
                                "About",
                                model.PageName

                            );

                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileUpload.Delete(oldImage);
                }
            }

            try
            {
                

                var page =
                    new AboutUsDTO
                    {
                        Id = model.Id,
                        PageName = model.PageName,
                        BannerImage = bannerPath,
                        Image = imagePath,
                        Name = model.Name,
                        Description = model.Description,
                        MetaTitle = model.MetaTitle,
                        MetaDescription = model.MetaDescription,
                        UpdatedBy = model.UpdatedBy
                    };


                var result = await _aboutUsService.UpdateAsync(page);
                if (!result.IsSucceeded)
                {
                    if (model.Banner != null)
                    {
                        _fileUpload.Delete(bannerPath);
                    }
                    if (model.Photo != null)
                    {
                        _fileUpload.Delete(imagePath);
                    }
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                if (model.Banner != null)
                {
                    _fileUpload.Delete(bannerPath);
                }
                if (model.Photo != null)
                {
                    _fileUpload.Delete(imagePath);
                }
                return BadRequest(new
                {
                    message =
                        ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] AboutUsDTO model)
        {
            try
            {
                var result = await _aboutUsService.deleteAsync(model);
                if (result.IsSucceeded)
                {
                    if (model.Image != null)
                    {
                        _fileUpload.Delete(model.Image);
                    }
                    else if (model.BannerImage != null) 
                    {
                        _fileUpload.Delete(model.BannerImage);
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
