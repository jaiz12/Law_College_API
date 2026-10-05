using Common.DbContext;
using DTO.Models.Academics;
using DTO.Models.Admissions;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Admissions.Contact_Admission_Office
{
    public class ContactAdmissionOfficeService: MyDbContext, IContactAdmissionOfficeService
    {
        public async Task<DataTable> GetAsync()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Admissions_ContactAdmissionOffice_Get", CommandType.StoredProcedure));
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
            ContactAdmissionOfficesDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Admissions_ContactAdmissionOffice_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"Contact Admission Office Saved Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Save Contact Admission Office";
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
            ContactAdmissionOfficesDTO model)
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
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Admissions_ContactAdmissionOffice_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = $"Contact Admission Office Updated Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Update Contact Admission Office";
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
