using DTO.Models.About;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.About.VisionMission
{
    public interface IVisionMissionService
    {
        Task<DataResponse> CreateAsync(VisionMissionDTO model);

        Task<DataTable> GetAsync();

        Task<DataResponse> UpdateAsync(VisionMissionDTO model);
    }
}
