using Common.DbContext;
using DTO.Models.About;
using DTO.Models.Banner;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Banner
{
    public class BannerService: MyDbContext, IBannerService
    {

        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Banner_GetAll", CommandType.StoredProcedure));
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

        public async Task<DataTable> GetByIdAsync(int Id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", Id);
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Banner_GetById", CommandType.StoredProcedure));
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
            BannerDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("PageName", model.PageName);
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("ImagePath", model.ImagePath);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Banner_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Banner Added Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Banner";
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
            BannerDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("PageName", model.PageName);
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("ImagePath", model.ImagePath);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Banner_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Banner Updated Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Banner";
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

        public async Task<DataResponse> deleteAsync(BannerDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Banner_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Banner Deleted Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete Banner";
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
