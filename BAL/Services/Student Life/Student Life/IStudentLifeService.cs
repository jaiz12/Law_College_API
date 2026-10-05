using DTO.Models.Committee_and_Cell;
using DTO.Models.DataResponse;
using DTO.Models.Student_Life;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Student_Life.Student_Life
{
    public interface IStudentLifeService
    {
        Task<DataTable> GetAsync(int Id, string PageName);
        Task<DataResponse> CreateAsync(StudentLifeDTO model);


        Task<DataResponse> UpdateAsync(StudentLifeDTO model);
    }
}
