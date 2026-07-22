using Common.DbContext;
using DTO.Models.About;
using DTO.Models.DataResponse;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BAL.Services.About.Recognitions_And_Affiliations
{
    public class RecognitionsAndAffiliationsService : MyDbContext, IRecognitionsAndAffiliationsService
    {
        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_RecognitionsAndAffiliations_GetAll", CommandType.StoredProcedure));
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
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_RecognitionsAndAffiliations_GetById", CommandType.StoredProcedure));
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
            RecognitionsAndAffiliationsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("Description", model.Description);
                _sqlCommand.Add_Parameter_WithValue("ExternalUrl", model.ExternalUrl);
                _sqlCommand.Add_Parameter_WithValue("CoverImage", model.CoverImage);
                _sqlCommand.Add_Parameter_WithValue("DisplayOrder", model.DisplayOrder);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_RecognitionsAndAffiliations_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Recognitions And Affiliations Added Successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Recognitions And Affiliations";
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
            RecognitionsAndAffiliationsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("Description", model.Description);
                _sqlCommand.Add_Parameter_WithValue("ExternalUrl", model.ExternalUrl);
                _sqlCommand.Add_Parameter_WithValue("CoverImage", model.CoverImage);
                _sqlCommand.Add_Parameter_WithValue("DisplayOrder", model.DisplayOrder);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_RecognitionsAndAffiliations_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Recognitions And Affiliations Updated successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Recognitions And Affiliations";
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

        public async Task<DataResponse> deleteAsync(RecognitionsAndAffiliationsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_RecognitionsAndAffiliations_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Recognitions And Affiliations Deleted successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete Recognitions And Affiliations";
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
