using DTO.Models.Alumni;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Alumni.Registration
{
    public interface IRegistrationService
    {
        Task<DataTable> GetAllAsync();
        Task<DataResponse> CreateAsync(RegistrationDTO model);
    }
}
