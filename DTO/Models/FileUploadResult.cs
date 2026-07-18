using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class FileUploadResult
    {
        public bool Success { get; set; }

        public string FileName { get; set; } = "";

        public string FilePath { get; set; } = "";

        public string Extension { get; set; } = "";

        public long FileSize { get; set; }

        public string Message { get; set; } = "";
    }
}
