using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.News_and_Events
{
    public class AnnouncementsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;

        public IFormFile? File { get; set; }

        public string? FilePath { get; set; }
        public bool Urgent { get; set; } = false;

        public bool IsActive { get; set; } = false;

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
