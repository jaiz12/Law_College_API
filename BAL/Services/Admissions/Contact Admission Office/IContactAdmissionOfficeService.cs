using DTO.Models.Academics;
using DTO.Models.Admissions;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Admissions.Contact_Admission_Office
{
    public interface IContactAdmissionOfficeService
    {
        Task<DataTable> GetAsync();
        Task<DataResponse> CreateAsync(ContactAdmissionOfficesDTO model);


        Task<DataResponse> UpdateAsync(ContactAdmissionOfficesDTO model);
    }
}
