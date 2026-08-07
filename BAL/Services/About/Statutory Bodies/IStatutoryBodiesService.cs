using DTO.Models.About;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.Statutory_Bodies
{
    public interface IStatutoryBodiesService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(StatutoryBodiesDTO model);
        Task<DataResponse> UpdateAsync(StatutoryBodiesDTO model);
        Task<DataResponse> deleteAsync(StatutoryBodiesDTO model);
    }
}
