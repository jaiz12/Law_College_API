using DTO.Models.Alumni;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Alumni.Notable_Alumni
{
    public interface INotableAlumniService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(NotableAlumniDTO model);
        Task<DataResponse> UpdateAsync(NotableAlumniDTO model);
        Task<DataResponse> deleteAsync(NotableAlumniDTO model);
    }
}
