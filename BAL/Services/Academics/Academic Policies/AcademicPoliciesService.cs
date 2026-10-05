using Common.DbContext;
using DTO.Models.Academics;
using DTO.Models.DataResponse;
using DTO.Models.Student_Life;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Academics.Academic_Policies
{
    public class AcademicPoliciesService: MyDbContext, IAcademicPoliciesService
    {
        public async Task<DataTable> GetAsync()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Academic_AcademicPolicies_Get", CommandType.StoredProcedure));
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
            AcademicPoliciesDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Academic_AcademicPolicies_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"Academic Policies Saved Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Save Academic Policies";
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
            AcademicPoliciesDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Academic_AcademicPolicies_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"Academic Policies Updated Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Update Academic Policies";
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
