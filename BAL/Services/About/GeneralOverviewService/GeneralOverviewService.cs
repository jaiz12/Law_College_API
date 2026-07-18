using Common.DataContext;
using Common.DbContext;
using DTO.Models.About;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.GeneralOverviewService
{
    public class GeneralOverviewService : MyDbContext, IGeneralOverviewService
    {
       

        public async Task<DataResponse> CreateAsync(
            GeneralOverviewPage model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("BannerImage", model.BannerImage);
                _sqlCommand.Add_Parameter_WithValue("Description" , model.Description);
                _sqlCommand.Add_Parameter_WithValue("MetaTitle", model.MetaTitle);
                _sqlCommand.Add_Parameter_WithValue("MetaDescription", model.MetaDescription);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_GeneralOverview_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "General Overview Added successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Add General Overview";
                    status = false;
                }
                return new DataResponse(message, status);
            }
            catch(Exception ex) {
                throw ex;
            }
            finally { 
                CloseContext();
            }
        }

        public async Task<DataTable> GetAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_GereralOverview_Get", CommandType.StoredProcedure));
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
            GeneralOverviewPage model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("BannerImage", model.BannerImage);
                _sqlCommand.Add_Parameter_WithValue("Description", model.Description);
                _sqlCommand.Add_Parameter_WithValue("MetaTitle", model.MetaTitle);
                _sqlCommand.Add_Parameter_WithValue("MetaDescription", model.MetaDescription);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_GeneralOverview_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "General Overview Updated successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update General Overview";
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
