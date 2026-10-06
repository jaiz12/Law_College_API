using DTO.Models.Admissions;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Admissions.Eligibility_Admission_Process_And_Intake
{
    public interface IEligibilityAdmissionProcessAndIntakeService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(EligibilityAdmissionProcessAndIntakeDTO model);
        Task<DataResponse> UpdateAsync(EligibilityAdmissionProcessAndIntakeDTO model);
        Task<DataResponse> deleteAsync(EligibilityAdmissionProcessAndIntakeDTO model);
    }
}
