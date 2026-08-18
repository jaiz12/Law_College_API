using DTO.Models;
using DTO.Models.DataResponse;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BAL.Services.ContactUs
{
    public interface IContactUsService
    {
        Task<DataTable> GetAsync(int Id, string SectionName);
        Task<DataResponse> CreateAsync(ContactUsDTO model);
        Task<DataResponse> UpdateAsync(ContactUsDTO model);
        Task<DataResponse> DeleteAsync(ContactUsDTO model);
    }
}
