using DTO.Models.Admissions;
using DTO.Models.Compliance_Or_Disclosures;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Compliance_Or_Disclosures
{
    public interface IComplianceOrDisclosuresService
    {
        Task<DataTable> GetAsync(string PageName);
        Task<DataResponse> CreateAsync(ComplianceOrDisclosuresDTO model);


        Task<DataResponse> UpdateAsync(ComplianceOrDisclosuresDTO model);
    }
}
