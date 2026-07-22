using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.About
{
    public class OrganizationalStructureDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int? ParentId { get; set; }

        public IFormFile? Photo { get; set; }
        public string? ProfilePhoto { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
