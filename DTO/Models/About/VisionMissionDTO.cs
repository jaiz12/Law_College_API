using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.About
{
    public class VisionMissionDTO
    {
        public int Id { get; set; }
        public string? Vision { get; set; }

        public string? Mission { get; set; }

        public string? MetaTitle { get; set; }

        public string? MetaDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
