using DTO.Models.Academics;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.Academics.Our_Program
{
    public interface IOurProgramService
    {
        Task<DataTable> GetAsync();
        Task<DataResponse> CreateAsync(OurProgramDTO model);
        Task<DataResponse> UpdateAsync(OurProgramDTO model);
        Task<DataResponse> DeleteAsync(string Id);
    }
}
