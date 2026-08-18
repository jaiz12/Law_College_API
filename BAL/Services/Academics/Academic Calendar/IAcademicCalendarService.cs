using DTO.Models.Academics;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.Academics.Academic_Calendar
{
    public interface IAcademicCalendarService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(AcademicCalendarDTO model);
        Task<DataResponse> UpdateAsync(AcademicCalendarDTO model);
        Task<DataResponse> deleteAsync(AcademicCalendarDTO model);
    }
}
