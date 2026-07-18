using DTO.Models.About;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.About.GeneralOverviewService
{
    public interface IGeneralOverviewService
    {
        Task<DataResponse> CreateAsync(
            GeneralOverviewDTO model);

        Task<DataTable> GetAsync();

        Task<DataResponse> UpdateAsync(GeneralOverviewDTO model);
    }
}
