using DTO.Models.About;
using DTO.Models.DataResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.Recognitions_And_Affiliations
{
    public interface IRecognitionsAndAffiliationsService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(RecognitionsAndAffiliationsDTO model);
        Task<DataResponse> UpdateAsync(RecognitionsAndAffiliationsDTO model);
        Task<DataResponse> deleteAsync(RecognitionsAndAffiliationsDTO model);
    }
}
