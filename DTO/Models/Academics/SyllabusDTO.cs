using Microsoft.AspNetCore.Http;

namespace DTO.Models.Academics
{
    public class SyllabusDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? FilePath { get; set; }
        public IFormFile? File { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
