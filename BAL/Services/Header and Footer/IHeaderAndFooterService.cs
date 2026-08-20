using DTO.Models.DataResponse;
using DTO.Models.Header_and_Footer;
using System.Data;

namespace BAL.Services.Header_and_Footer.Logo_And_Title
{
    public interface IHeaderAndFooterService
    {
        Task<DataTable> GetAsync(int Id, string SectionName);
        Task<DataResponse> CreateAsync(HeaderAndFooterDTO model);
        Task<DataResponse> UpdateAsync(HeaderAndFooterDTO model);
        Task<DataResponse> DeleteAsync(HeaderAndFooterDTO model);
    }
}
