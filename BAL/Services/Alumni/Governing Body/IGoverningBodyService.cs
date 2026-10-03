using DTO.Models.Alumni;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Alumni.Governing_Body
{
    public interface IGoverningBodyService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(GoverningBodyDTO model);
        Task<DataResponse> UpdateAsync(GoverningBodyDTO model);
        Task<DataResponse> deleteAsync(GoverningBodyDTO model);
    }
}
