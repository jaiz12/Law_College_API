using DTO.Models;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Home
{
    public interface IHomeService
    {
        Task<DataTable> GetAsync(string PageName);
        Task<DataResponse> CreateAsync(HomeDTO model);
        Task<DataResponse> UpdateAsync(HomeDTO model);
        Task<DataResponse> DeleteAsync(HomeDTO model);
    }
}
