using Common.DbContext;
using DTO.Models.Admissions;
using DTO.Models.Compliance_Or_Disclosures;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Compliance_Or_Disclosures
{
    public class ComplianceOrDisclosuresService: MyDbContext, IComplianceOrDisclosuresService
    {

        public async Task<DataTable> GetAsync(string PageName)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("PageName", PageName);
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_ComplianceOrDisclosures_Get", CommandType.StoredProcedure));
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
            ComplianceOrDisclosuresDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("PageName", model.PageName);
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("FilePath", model.FilePath);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_ComplianceOrDisclosures_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"{model.PageName} Saved Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Save {model.PageName}";
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
            ComplianceOrDisclosuresDTO model)
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
                _sqlCommand.Add_Parameter_WithValue("FilePath", model.FilePath);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_ComplianceOrDisclosures_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"{model.PageName} Updated Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Update {model.PageName}";
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
