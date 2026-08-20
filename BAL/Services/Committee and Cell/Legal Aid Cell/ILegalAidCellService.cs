using DTO.Models.Committee_and_Cell;
using DTO.Models.DataResponse;
using DTO.Models.Student_Life;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Committee_and_Cell.Legal_Aid_Cell
{
    public interface ILegalAidCellService
    {
        Task<DataTable> GetAsync();
        Task<DataResponse> CreateAsync(LegalAidCellDTO model);
        Task<DataResponse> UpdateAsync(LegalAidCellDTO model);
        Task<DataResponse> DeleteAsync(string Id);
    }
}
