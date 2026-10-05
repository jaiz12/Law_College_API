using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.Student_Life
{
    public class StudentLifeDTO
    {
        public int Id { get; set; }
        public string? PageName { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
