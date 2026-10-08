using DTO.Models.About;
using DTO.Models.DataResponse;
using DTO.Models.Examinations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Examinations.Student_Achievers
{
    public interface IStudentAchieversService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(StudentAchieversDTO model);
        Task<DataResponse> UpdateAsync(StudentAchieversDTO model);
        Task<DataResponse> deleteAsync(StudentAchieversDTO model);
    }
}
