using DTO.Models.About;
using DTO.Models.Alumni;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Alumni.Newsletters
{
    public interface INewslettersService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(NewslettersDTO model);
        Task<DataResponse> UpdateAsync(NewslettersDTO model);
        Task<DataResponse> deleteAsync(NewslettersDTO model);
    }
}
