using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.Admissions
{
    public class ProspectusDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? FilePath { get; set; }
        public IFormFile? File { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
