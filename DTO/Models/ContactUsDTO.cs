using System;

namespace DTO.Models
{
    public class ContactUsDTO
    {
        public int Id { get; set; }

        public string? SectionName { get; set; }

        public string? Icon { get; set; }

        public string? Detail { get; set; }

        public string? Name { get; set; }

        public string? Link { get; set; }

        public string? Type { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}