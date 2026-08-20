using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.Header_and_Footer
{
    public class HeaderAndFooterDTO
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

        public IFormFile? Logo { get; set; }
        public string? LogoPath { get; set; } = string.Empty;

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
