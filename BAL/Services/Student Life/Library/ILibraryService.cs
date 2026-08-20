using DTO.Models;
using DTO.Models.DataResponse;
using DTO.Models.Student_Life;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Student_Life.Library
{
    public interface ILibraryService
    {

        Task<DataTable> GetAsync();
        Task<DataResponse> CreateAsync(LibraryDTO model);
        Task<DataResponse> UpdateAsync(LibraryDTO model);
        Task<DataResponse> DeleteAsync(string Id);
    }
}
