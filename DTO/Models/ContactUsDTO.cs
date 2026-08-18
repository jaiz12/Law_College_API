using System;

namespace DTO.Models
{
    public class ContactUsDTO
    {
        public int Id { get; set; }

        public string? SectionName { get; set; }

        public string? Icon { get; set; } = null;

        public string? Detail { get; set; } = null;

        public string? Name { get; set; } = null;

        public string? Link { get; set; } = null;

        public string? Type { get; set; } = null;

        public string? Latitude { get; set; } = null;

        public string? Longitude { get; set; } = null;

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}