using Common.DbContext;
using DTO.Models.Admissions;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Admissions.Eligibility_Admission_Process_And_Intake
{
    public class EligibilityAdmissionProcessAndIntakeService: MyDbContext, IEligibilityAdmissionProcessAndIntakeService
    {
        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Admissions_EligibilityAdmissionProcessAndIntake_GetAll", CommandType.StoredProcedure));
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
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Admissions_EligibilityAdmissionProcessAndIntake_GetById", CommandType.StoredProcedure));
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
            EligibilityAdmissionProcessAndIntakeDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("FilePath", model.FilePath);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Admissions_EligibilityAdmissionProcessAndIntake_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Eligibility Admission Process And Intake Saved Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Save Eligibility Admission Process And Intake";
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
            EligibilityAdmissionProcessAndIntakeDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("FilePath", model.FilePath);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Admissions_EligibilityAdmissionProcessAndIntake_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Eligibility Admission Process And Intake Updated Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Eligibility Admission Process And Intake";
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

        public async Task<DataResponse> deleteAsync(EligibilityAdmissionProcessAndIntakeDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Admissions_EligibilityAdmissionProcessAndIntake_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Eligibility Admission Process And Intake Deleted Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete Eligibility Admission Process And Intake";
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
