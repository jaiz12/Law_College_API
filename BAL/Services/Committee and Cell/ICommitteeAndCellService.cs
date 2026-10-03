using DTO.Models.Committee_and_Cell;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.Committee_and_Cell
{
    public interface ICommitteeAndCellService
    {
        Task<DataTable> GetAsync(int Id, string PageName);
        Task<DataResponse> CreateAsync(CommitteeAndCellDTO model);


        Task<DataResponse> UpdateAsync(CommitteeAndCellDTO model);
    }
}
