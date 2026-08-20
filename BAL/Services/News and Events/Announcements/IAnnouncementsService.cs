using DTO.Models.About;
using DTO.Models.DataResponse;
using DTO.Models.News_and_Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.News_and_Events.Announcemets
{
    public interface IAnnouncementsService
    {
        Task<DataTable> GetAllIsActiveAsync();
        Task<DataTable> GetAllInActiveAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(AnnouncementsDTO model);
        Task<DataResponse> UpdateAsync(AnnouncementsDTO model);
        Task<DataResponse> ArchiveAsync(string Id, string UpdatedBy);
        Task<DataResponse> deleteAsync(AnnouncementsDTO model);
    }
}
