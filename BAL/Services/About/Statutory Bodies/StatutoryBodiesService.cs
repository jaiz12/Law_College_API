using BAL.Services.About.About_Us;
using Common.DbContext;
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
    public class StatutoryBodiesService : MyDbContext, IStatutoryBodiesService
    {
        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_AboutUs_StatutoryBodies_GetAll", CommandType.StoredProcedure));
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
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_AboutUs_StatutoryBodies_GetById", CommandType.StoredProcedure));
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
            StatutoryBodiesDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("Image", model.Image);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_StatutoryBodies_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Statutory Body Added Successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Statutory Body";
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
            StatutoryBodiesDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("Image", model.Image);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_StatutoryBodies_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Statutory Body Updated successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Statutory Body";
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

        public async Task<DataResponse> deleteAsync(StatutoryBodiesDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_AboutUs_StatutoryBodies_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Statutory Body Deleted successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete Statutory Body";
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
