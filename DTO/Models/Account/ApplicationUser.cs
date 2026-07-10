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
}
