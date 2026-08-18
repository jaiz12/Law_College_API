using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class HomeDTO
    {
        public int Id { get; set; }
        public string? PageName { get; set; }

        public string? Icon { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ExternalLink { get; set; }
        public string? Count { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
