using Common.DbContext;
using DTO.Models.About;
using DTO.Models.Academics;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Academics.Research_And_Publications
{
    public class ResearchAndPublicationsService: MyDbContext, IResearchAndPublicationsService
    {
        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Academics_ResearchAndPublications_GetAll", CommandType.StoredProcedure));
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
            ResearchAndPublicationsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("Description", model.Description);
                _sqlCommand.Add_Parameter_WithValue("Link", model.Link);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Academics_ResearchAndPublications_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Research And Publications Saved Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Save Research And Publications";
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
            ResearchAndPublicationsDTO model)
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
                _sqlCommand.Add_Parameter_WithValue("Link", model.Link);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Academics_ResearchAndPublications_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Research And Publications Updated Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Research And Publications";
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

        public async Task<DataResponse> deleteAsync(int Id)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Academics_ResearchAndPublications_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Research And Publications Deleted Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete Research And Publications";
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
