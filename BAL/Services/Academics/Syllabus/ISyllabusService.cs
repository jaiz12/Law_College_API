using DTO.Models.Academics;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Academics.Syllabus
{
    public interface ISyllabusService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(SyllabusDTO model);
        Task<DataResponse> UpdateAsync(SyllabusDTO model);
        Task<DataResponse> deleteAsync(SyllabusDTO model);
    }
}
