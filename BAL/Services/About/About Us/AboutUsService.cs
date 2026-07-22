using Common.DbContext;
using DTO.Models.About;
using DTO.Models.DataResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.About_Us
{
    internal class AboutUsService : MyDbContext, IAboutUsService
    {
        public async Task<DataTable> GetAsync(int Id, string PageName)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", Id == 0 ? null : Id);
                _sqlCommand.Add_Parameter_WithValue("PageName", PageName);
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_AboutUs_Get", CommandType.StoredProcedure));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataResponse> CreateAsync(
            AboutUsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("PageName", model.PageName);
                _sqlCommand.Add_Parameter_WithValue("BannerImage", model.BannerImage);
                _sqlCommand.Add_Parameter_WithValue("Image", model.Image);
                _sqlCommand.Add_Parameter_WithValue("Name", model.Name);
                _sqlCommand.Add_Parameter_WithValue("Description", model.Description);
                _sqlCommand.Add_Parameter_WithValue("MetaTitle", model.MetaTitle);
                _sqlCommand.Add_Parameter_WithValue("MetaDescription", model.MetaDescription);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"{model.PageName } Added Successfully.";
                    status = true;
                }
                else
                {
                    message = $"Failed to Add {model.PageName}";
                    status = false;
                }
                return new DataResponse(message, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        
        public async Task<DataResponse> UpdateAsync(
            AboutUsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("BannerImage", model.BannerImage);
                _sqlCommand.Add_Parameter_WithValue("Image", model.Image);
                _sqlCommand.Add_Parameter_WithValue("Name", model.Name);
                _sqlCommand.Add_Parameter_WithValue("Description", model.Description);
                _sqlCommand.Add_Parameter_WithValue("MetaTitle", model.MetaTitle);
                _sqlCommand.Add_Parameter_WithValue("MetaDescription", model.MetaDescription);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"{model.PageName } Updated successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Update {model.PageName}";
                    status = false;
                }
                return new DataResponse(message, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataResponse> deleteAsync([FromForm] AboutUsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);               
                _sqlCommand.Add_Parameter_WithValue("PageName", model.PageName);               
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"{model.PageName } Deleted successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Delete {model.PageName}";
                    status = false;
                }
                return new DataResponse(message, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
