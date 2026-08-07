using DTO.Models.About;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.About.Administrative_Staff
{
    public interface IAdministrativeStaffService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(AdministrativeStaffDTO model);
        Task<DataResponse> UpdateAsync(AdministrativeStaffDTO model);
        Task<DataResponse> deleteAsync(AdministrativeStaffDTO model);
    }
}
