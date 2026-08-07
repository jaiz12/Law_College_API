using DTO.Models.About;
using DTO.Models.DataResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.About_Us
{
    public interface IInfrastructureService
    {

        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(InfrastructureDTO model);
        Task<DataResponse> UpdateAsync(InfrastructureDTO model);
        Task<DataResponse> deleteAsync(InfrastructureDTO model);
    }
}
