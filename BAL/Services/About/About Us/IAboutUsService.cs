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
    public interface IAboutUsService
    {

        Task<DataTable> GetAsync(int Id, string PageName);
        Task<DataResponse> CreateAsync(AboutUsDTO model);


        Task<DataResponse> UpdateAsync(AboutUsDTO model);
        Task<DataResponse> deleteAsync([FromForm] AboutUsDTO model);
    }
}
