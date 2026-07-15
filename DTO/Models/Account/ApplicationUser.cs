using Microsoft.AspNetCore.Identity;

namespace DTO.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public bool UserStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class CreateUserModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? RoleId { get; set; }

        public string? CreatedBy { get; set; }
    }

    public class UpdateUserModel
    {
        public string Id { get; set; } = "";

        public string UserName { get; set; } = "";

        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public string RoleId { get; set; } = "";

        // Optional
        public string? Password { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
