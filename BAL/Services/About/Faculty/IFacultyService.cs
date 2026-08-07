using DTO.Models.About;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.Faculty
{
    public interface IFacultyService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(FacultyDTO model);
        Task<DataResponse> UpdateAsync(FacultyDTO model);
        Task<DataResponse> deleteAsync(FacultyDTO model);
    }
}
