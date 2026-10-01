using DTO.Models.Admissions;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.Admissions
{
    public interface IProspectusService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(ProspectusDTO model);
        Task<DataResponse> UpdateAsync(ProspectusDTO model);
        Task<DataResponse> deleteAsync(ProspectusDTO model);
    }
}
