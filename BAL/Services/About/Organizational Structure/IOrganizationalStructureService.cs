using DTO.Models.About;
using DTO.Models.DataResponse;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BAL.Services.About.Organizational_Structure
{
    public interface IOrganizationalStructureService
    {
        Task<DataTable> GetAllAsync();
        Task<DataTable> GetByIdAsync(int Id);
        Task<DataResponse> CreateAsync(OrganizationalStructureDTO model);
        Task<DataResponse> UpdateAsync(OrganizationalStructureDTO model);        
        Task<DataResponse> deleteAsync(OrganizationalStructureDTO model);
    }
}
