using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.Committee_and_Cell
{
    public class LegalAidCellDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? ExternalLink { get; set; } = null;

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
