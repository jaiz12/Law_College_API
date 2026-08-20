using Common.DbContext;
using DTO.Models.Committee_and_Cell;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.Committee_and_Cell.Legal_Aid_Cell
{
    public class LegalAidCellService: MyDbContext, ILegalAidCellService
    {
        // ---------------------------------------------
        // GET
        // ---------------------------------------------
        public async Task<DataTable> GetAsync()
        {
            try
            {
                OpenContext();

                var result = await Task.Run(() =>
                    _sqlCommand.Select_Table(
                        "sp_CommitteeAndCell_LegalAidCell_Get",
                        CommandType.StoredProcedure
                    )
                );

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------------
        // CREATE
        // ---------------------------------------------
        public async Task<DataResponse> CreateAsync(
            LegalAidCellDTO model)
        {
            try
            {
                OpenContext();

                string message;
                bool status;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Title",
                    model.Title
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "ExternalLink",
                    model.ExternalLink
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "CreatedBy",
                    model.CreatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_CommitteeAndCell_LegalAidCell_Create",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = "Legal Aid Cell Added successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Legal Aid Cell";
                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------------
        // UPDATE
        // ---------------------------------------------
        public async Task<DataResponse> UpdateAsync(LegalAidCellDTO model)
        {
            try
            {
                OpenContext();

                string message;
                bool status;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    model.Id
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Title",
                    model.Title
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "ExternalLink",
                    model.ExternalLink
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "UpdatedBy",
                    model.UpdatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_CommitteeAndCell_LegalAidCell_Update",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = "Legal Aid Cell Updated Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Legal Aid Cell";
                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------------
        // DELETE
        // ---------------------------------------------
        public async Task<DataResponse> DeleteAsync(
            string Id)
        {
            try
            {
                OpenContext();

                string message;
                bool status;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    Id
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_CommitteeAndCell_LegalAidCell_Delete",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message =
                       "Legal Aid Cell deleted successfully.";

                    status = true;
                }
                else
                {
                    message =
                        "Failed to Delete Legal Aid Cell";

                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
