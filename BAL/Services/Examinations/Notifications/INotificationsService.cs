using DTO.Models.DataResponse;
using DTO.Models.Examinations;
using System.Data;

namespace BAL.Services.Examinations.Notifications
{
    public interface INotificationsService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(NotificationsDTO model);
        Task<DataResponse> UpdateAsync(NotificationsDTO model);
        Task<DataResponse> deleteAsync(NotificationsDTO model);
    }
}
