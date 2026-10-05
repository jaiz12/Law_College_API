using DTO.Models.Academics;
using DTO.Models.DataResponse;
using DTO.Models.Student_Life;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Academics.Academic_Policies
{
    public interface IAcademicPoliciesService
    {
        Task<DataTable> GetAsync();
        Task<DataResponse> CreateAsync(AcademicPoliciesDTO model);


        Task<DataResponse> UpdateAsync(AcademicPoliciesDTO model);
    }
}
