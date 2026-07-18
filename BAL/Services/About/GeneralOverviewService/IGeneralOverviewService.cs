using DTO.Models.About;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.GeneralOverviewService
{
    public interface IGeneralOverviewService
    {
        Task<DataResponse> CreateAsync(
            GeneralOverviewPage model);

        Task<DataTable> GetAsync();

        Task<DataResponse> UpdateAsync(GeneralOverviewPage model);
    }
}
