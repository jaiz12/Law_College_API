using BAL.Services.About.Faculty;
using Common.DbContext;
using DTO.Models.About;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.Administrative_Staff
{
    public class AdministrativeStaffService : MyDbContext, IAdministrativeStaffService
    {

        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_AboutUs_AdministrativeStaff_GetAll", CommandType.StoredProcedure));
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
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_AboutUs_AdministrativeStaff_GetById", CommandType.StoredProcedure));
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
            AdministrativeStaffDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Name", model.Name);
                _sqlCommand.Add_Parameter_WithValue("Designation", model.Designation);
                _sqlCommand.Add_Parameter_WithValue("Email", model.Email);
                _sqlCommand.Add_Parameter_WithValue("Phone", model.Phone);
                _sqlCommand.Add_Parameter_WithValue("ParentId", model.ParentId);
                _sqlCommand.Add_Parameter_WithValue("ProfilePhoto", model.ProfilePhoto);
                _sqlCommand.Add_Parameter_WithValue("DisplayOrder", model.DisplayOrder);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_AdministrativeStaff_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Administrative Staff Added Successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Administrative Staff";
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
            AdministrativeStaffDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("Name", model.Name);
                _sqlCommand.Add_Parameter_WithValue("Designation", model.Designation);
                _sqlCommand.Add_Parameter_WithValue("Email", model.Email);
                _sqlCommand.Add_Parameter_WithValue("Phone", model.Phone);
                _sqlCommand.Add_Parameter_WithValue("ParentId", model.ParentId);
                _sqlCommand.Add_Parameter_WithValue("ProfilePhoto", model.ProfilePhoto);
                _sqlCommand.Add_Parameter_WithValue("DisplayOrder", model.DisplayOrder);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_AdministrativeStaff_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Administrative Staff Updated successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Administrative Staff";
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

        public async Task<DataResponse> deleteAsync(AdministrativeStaffDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_AdministrativeStaff_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Administrative Staff Deleted successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete Administrative Staff";
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
