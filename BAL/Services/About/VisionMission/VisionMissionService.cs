using Common.DbContext;
using DTO.Models.About;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.About.VisionMission
{
    public class VisionMissionService : MyDbContext, IVisionMissionService
    {
        public async Task<DataResponse> CreateAsync(
            VisionMissionDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Vision", model.Vision);
                _sqlCommand.Add_Parameter_WithValue("Mission", model.Mission);
                _sqlCommand.Add_Parameter_WithValue("MetaTitle", model.MetaTitle);
                _sqlCommand.Add_Parameter_WithValue("MetaDescription", model.MetaDescription);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_VisionMission_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Vision and Mission Added successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Vision and Mission";
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

        public async Task<DataTable> GetAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_VisionMission_Get", CommandType.StoredProcedure));
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
        public async Task<DataResponse> UpdateAsync(
            VisionMissionDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("Vision", model.Vision);
                _sqlCommand.Add_Parameter_WithValue("Mission", model.Mission);
                _sqlCommand.Add_Parameter_WithValue("MetaTitle", model.MetaTitle);
                _sqlCommand.Add_Parameter_WithValue("MetaDescription", model.MetaDescription);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_VisionMission_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Vision and Mission Updated successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Vision and Mission";
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
