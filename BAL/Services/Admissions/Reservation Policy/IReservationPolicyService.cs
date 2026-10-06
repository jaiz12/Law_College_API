using DTO.Models.Admissions;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Admissions.Reservation_Policy
{
    public interface IReservationPolicyService
    {
        Task<DataTable> GetAsync();

        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(ReservationPolicyDTO model);


        Task<DataResponse> UpdateAsync(ReservationPolicyDTO model);
    }
}
