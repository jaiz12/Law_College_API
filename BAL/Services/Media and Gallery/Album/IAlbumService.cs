using DTO.Models;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.Media_and_Gallery.Album
{
    public interface IAlbumService
    {
        Task<DataTable> GetAllAsync();

        Task<DataTable> GetByIdAsync(int Id);

        Task<DataResponse> CreateAsync(
            AlbumDTO model
        );

        Task<DataResponse> UpdateAsync(
            AlbumDTO model
        );

        Task<DataResponse> DeleteAsync(
            AlbumDTO model
        );
    }
}
