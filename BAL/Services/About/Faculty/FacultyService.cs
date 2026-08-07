using Common.DbContext;
using DTO.Models.About;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.Faculty
{
    public class FacultyService : MyDbContext, IFacultyService
    {

        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_AboutUs_Faculties_GetAll", CommandType.StoredProcedure));
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
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_AboutUs_Faculties_GetById", CommandType.StoredProcedure));
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
            FacultyDTO model)
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
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_Faculties_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Faculty Added Successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Faculty";
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
            FacultyDTO model)
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
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_Faculties_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Faculty Updated successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Faculty";
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

        public async Task<DataResponse> deleteAsync(FacultyDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_Faculties_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Faculty Deleted successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete Faculty";
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
