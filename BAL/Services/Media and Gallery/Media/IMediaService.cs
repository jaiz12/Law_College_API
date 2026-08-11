using DTO.Models;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Media_and_Gallery.Media
{
    public interface IMediaService
    {
        Task<DataTable> GetAllAsync();

        Task<DataTable> GetByIdAsync(
            int Id
        );

        Task<DataTable> GetByAlbumIdAsync(
            int AlbumId
        );

        Task<DataResponse> CreateAsync(
            MediaDTO model
        );

        Task<DataResponse> UpdateAsync(
            MediaDTO model
        );

        Task<DataResponse> DeleteAsync(
            MediaDTO model
        );
    }
}
