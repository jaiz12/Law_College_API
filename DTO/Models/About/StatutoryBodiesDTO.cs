using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.About
{
    public class StatutoryBodiesDTO
    {
        public int Id { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

