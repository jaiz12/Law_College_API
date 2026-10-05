using DTO.Models.Admissions;
using DTO.Models.DataResponse;
using DTO.Models.Examinations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Examinations.Results
{
    public interface IResultsService
    {
        Task<DataTable> GetAsync();
        Task<DataResponse> CreateAsync(ResultsDTO model);


        Task<DataResponse> UpdateAsync(ResultsDTO model);
    }
}
