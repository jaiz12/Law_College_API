using API.Services;
using Common.DataContext;
using DTO.Models.Account;
using DTO.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AccountController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid Request",
                    messageDescription = "Please enter email and password.",
                    messageType = "error"
                });
            }

            try
            {
                // Find user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return Unauthorized(new
                    {
                        message = "Login Failed",
                        messageDescription = "Invalid email or password.",
                        messageType = "error"
                    });
                }

                // Check if user is active
                if (!user.UserStatus)
                {
                    return Unauthorized(new
                    {
                        message = "Account Disabled",
                        messageDescription = "Your account has been disabled.",
                        messageType = "error"
                    });
                }

                // Validate password
                var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!passwordValid)
                {
                    return Unauthorized(new
                    {
                        message = "Login Failed",
                        messageDescription = "Invalid email or password.",
                        messageType = "error"
                    });
                }

                // Get user roles
                var roles = await _userManager.GetRolesAsync(user);

                // Generate JWT
                var token = GenerateJwtToken(user, roles);

                return Ok(new
                {
                    message = "Login Successful",
                    messageDescription = "User logged in successfully.",
                    messageType = "success",

                    token,

                    user = new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.PhoneNumber,
                        Roles = roles
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Server Error",
                    messageDescription = ex.Message,
                    messageType = "error"
                });
            }
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid Request",
                    messageDescription = "Please enter a valid email.",
                    messageType = "error"
                });
            }

            var user = await _userManager.FindByEmailAsync(email);

            // Don't reveal whether the account exists
            if (user == null)
            {
                return Ok(new
                {
                    message = "Success",
                    messageDescription = "If an account exists, a password reset link has been sent.",
                    messageType = "success"
                });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token));

            var resetLink =
    $"{_configuration["Jwt:Audience"]}/reset-password?email={Uri.EscapeDataString(user.Email!)}&token={encodedToken}";

            var sent = await SendResetPasswordEmail(user.Email!, resetLink);

            if (!sent)
            {
                return StatusCode(500, new
                {
                    message = "Email sending failed",
                    messageDescription = "Unable to send reset email."
                });
            }

            return Ok(new
            {
                message = "Success",
                messageDescription = "If an account exists, a password reset link has been sent.",
                messageType = "success"
            });
        }


        private async Task<bool> SendResetPasswordEmail(string email, string link)
        {
            var subject = "Reset Your Password";

            var body = $@"
        <h2>Password Reset</h2>

        <p>Click the button below to reset your password.</p>

        <a href='{link}'
           style='padding:12px 20px;
                  background:#2563eb;
                  color:white;
                  text-decoration:none;
                  border-radius:6px'>
            Reset Password
        </a>

        <p>This link expires according to your Identity token configuration.</p>";

            // Call your email service here
            return(await _emailService.SendEmailAsync(email, subject, body));
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest("Invalid request.");
            }

            var decodedToken = Encoding.UTF8.GetString(
                WebEncoders.Base64UrlDecode(model.Token));

            var result = await _userManager.ResetPasswordAsync(
                user,
                decodedToken,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new
            {
                message = "Password reset successfully."
            });
        }


    }
}
