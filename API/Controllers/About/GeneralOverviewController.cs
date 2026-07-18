using API.Controllers.Services;
using BAL.Services.About.GeneralOverviewService;
using Common.DataContext;
using DTO.Models.About;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers.About
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralOverviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUpload;
        private readonly IGeneralOverviewService _generalOverviewService;

        public GeneralOverviewController(
            ApplicationDbContext context, IFileUploadService fileUpload, IGeneralOverviewService generalOverviewService)
        {
            _context = context;
            _fileUpload = fileUpload;
            _generalOverviewService = generalOverviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _generalOverviewService.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] GeneralOverviewPage model)
        {
            try
            {
                string? bannerPath = null;


                // Upload banner image
                if (model.Banner != null)
                {
                    bannerPath =
                        await _fileUpload.UploadAsync(
                            model.Banner,
                            "About",
                            "General Overview"
                        );
                }


                // Create a model for database
                var page =
                    new GeneralOverviewPage
                    {
                        BannerImage =
                            bannerPath,

                        Description =
                            model.Description,

                        MetaTitle =
                            model.MetaTitle,

                        MetaDescription =
                            model.MetaDescription,

                        CreatedBy =
                            model.CreatedBy
                    };


                // Call business service
                var result = await _generalOverviewService.CreateAsync(page);


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
        public async Task<IActionResult> Update(
    [FromForm] GeneralOverviewPage model)
        {
            try
            {
                var existingPage = await _generalOverviewService.GetAsync();


                if (existingPage == null)
                {
                    return NotFound(new
                    {
                        message =
                            "General Overview not found."
                    });
                }

                var row =  existingPage.Rows[0];


                string? oldBannerImage =
                    row["BannerImage"] == DBNull.Value
                        ? null
                        : row["BannerImage"].ToString();

                string? bannerPath = oldBannerImage;


                if (model.Banner != null)
                {
                    bannerPath =
                        await _fileUpload.UploadAsync(

                            model.Banner,

                            "About",

                            "General Overview"

                        );

                    if (!string.IsNullOrWhiteSpace(oldBannerImage))
                    {
                        _fileUpload.Delete(
                            oldBannerImage
                        );
                    }
                }


                var page =
                    new GeneralOverviewPage
                    {
                        Id =
                            model.Id,

                        BannerImage =
                            bannerPath,

                        Description =
                            model.Description,

                        MetaTitle =
                            model.MetaTitle,

                        MetaDescription =
                            model.MetaDescription,

                        UpdatedBy =
                            model.UpdatedBy
                    };


                var result =
                    await _generalOverviewService
                        .UpdateAsync(page);


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
