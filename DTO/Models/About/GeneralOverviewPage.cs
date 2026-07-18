using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.About
{
    public class GeneralOverviewPage
    {
        public int Id { get; set; }

        public IFormFile? Banner { get; set; }
        public string? BannerImage { get; set; }
        public string? RemovedBannerImage { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? MetaTitle { get; set; }

        public string? MetaDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
