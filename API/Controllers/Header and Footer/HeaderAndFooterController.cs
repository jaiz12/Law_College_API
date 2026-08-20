using API.Controllers.Services;
using BAL.Services.About.About_Us;
using BAL.Services.Header_and_Footer.Logo_And_Title;
using DTO.Models;
using DTO.Models.Header_and_Footer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Header_and_Footer
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeaderAndFooterController : Controller
    {
        private readonly IFileUploadService _fileUpload;
        private readonly IHeaderAndFooterService _headerAndFooterService;

        public HeaderAndFooterController(IFileUploadService fileUpload, IHeaderAndFooterService headerAndFooterService)
        {
            _fileUpload = fileUpload;
            _headerAndFooterService = headerAndFooterService;
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
                    await _headerAndFooterService.GetAsync(
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
            [FromForm] HeaderAndFooterDTO model)
        {

            string? logoPath = null;


            // Upload image
            if (model.LogoPath != null)
            {
                logoPath =
                    await _fileUpload.UploadAsync(
                        model.Logo,
                        "Header And Footer",
                        model.SectionName
                    );
            }
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        message = $"Invalid {model.SectionName} details."
                    });
                }

                model.LogoPath = logoPath;
                var result =
                    await _headerAndFooterService.CreateAsync(
                        model
                    );

                if (!result.IsSucceeded)
                {
                    if (model.Logo != null)
                    {
                        _fileUpload.Delete(logoPath);
                    }
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


        // =========================================================
        // UPDATE
        // =========================================================

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromForm] HeaderAndFooterDTO model)
        {

            var existingPage = await _headerAndFooterService.GetAsync(model.Id, model.SectionName);
            if (existingPage == null)
            {
                return NotFound(new
                {
                    message =
                        "General Overview not found."
                });
            }

            var row = existingPage.Rows[0];

            string? oldLogo =
                row["LogoPath"] == DBNull.Value
                    ? null
                    : row["LogoPath"].ToString();

            string? logoPath = oldLogo;

            if (model.Logo != null)
            {
                logoPath = await _fileUpload.UploadAsync(
                                model.Logo,
                                "Header And Footer",
                                model.SectionName

                            );

                if (!string.IsNullOrWhiteSpace(oldLogo))
                {
                    _fileUpload.Delete(oldLogo);
                }
            }
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

                model.LogoPath = logoPath;

                var result =
                    await _headerAndFooterService.UpdateAsync(
                        model
                    );

                if (!result.IsSucceeded)
                {
                    if (model.Logo != null)
                    {
                        _fileUpload.Delete(logoPath);
                    }
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


        // =========================================================
        // DELETE
        // =========================================================

        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromForm] HeaderAndFooterDTO model)
        {
            try
            {

                var result =
                    await _headerAndFooterService.DeleteAsync(model);
                if (!result.IsSucceeded)
                {
                    if (model.Logo != null)
                    {
                        _fileUpload.Delete(model.LogoPath);
                    }
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
    }
}
