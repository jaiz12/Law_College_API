using DTO.Models.Alumni;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Alumni.Alumni_Events
{
    public interface IAlumniEventsService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(AlumniEventsDTO model);
        Task<DataResponse> UpdateAsync(AlumniEventsDTO model);
        Task<DataResponse> deleteAsync(AlumniEventsDTO model);
    }
}
