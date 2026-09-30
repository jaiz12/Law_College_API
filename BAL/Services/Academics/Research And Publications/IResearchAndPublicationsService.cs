using DTO.Models.Academics;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.Academics.Research_And_Publications
{
    public interface IResearchAndPublicationsService
    {
        Task<DataTable> GetAllAsync();
        Task<DataResponse> CreateAsync(ResearchAndPublicationsDTO model);
        Task<DataResponse> UpdateAsync(ResearchAndPublicationsDTO model);
        Task<DataResponse> deleteAsync(int Id);
    }
}
