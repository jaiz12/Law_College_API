using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Auth
{
    public class AspNetRoles_DTO
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
    }
}
