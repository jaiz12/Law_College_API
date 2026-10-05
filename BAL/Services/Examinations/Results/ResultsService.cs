using Common.DbContext;
using DTO.Models.Admissions;
using DTO.Models.DataResponse;
using DTO.Models.Examinations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Examinations.Results
{
    public class ResultsService: MyDbContext, IResultsService
    {
        public async Task<DataTable> GetAsync()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Examination_Results_Get", CommandType.StoredProcedure));
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
            ResultsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Examination_Results_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"Result Saved Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Save Result";
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
            ResultsDTO model)
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
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Examination_Results_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"Result Updated Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Update Result";
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
