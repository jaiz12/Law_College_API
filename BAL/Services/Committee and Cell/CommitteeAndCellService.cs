using Common.DbContext;
using DTO.Models.About;
using DTO.Models.Committee_and_Cell;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Committee_and_Cell
{
    public class CommitteeAndCellService: MyDbContext, ICommitteeAndCellService
    {
        public async Task<DataTable> GetAsync(int Id, string PageName)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", Id == 0 ? null : Id);
                _sqlCommand.Add_Parameter_WithValue("PageName", PageName);
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_CommitteeAndCell_Get", CommandType.StoredProcedure));
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
            CommitteeAndCellDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("PageName", model.PageName);
                _sqlCommand.Add_Parameter_WithValue("Content", model.Content);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_CommitteeAndCell_Create", CommandType.StoredProcedure));
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
            CommitteeAndCellDTO model)
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
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_CommitteeAndCell_Update", CommandType.StoredProcedure));
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
