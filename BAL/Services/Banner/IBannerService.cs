using DTO.Models.About;
using DTO.Models.Banner;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Banner
{
    public interface IBannerService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(BannerDTO model);
        Task<DataResponse> UpdateAsync(BannerDTO model);
        Task<DataResponse> deleteAsync(BannerDTO model);
    }
}
