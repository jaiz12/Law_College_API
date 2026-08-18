using Common.DbContext;
using DTO.Models;
using DTO.Models.Academics;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Academics.Our_Program
{
    public class OurProgramService: MyDbContext, IOurProgramService
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
                        "sp_Academics_OurProgram_Get",
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
            OurProgramDTO model)
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
                    "ShortDescription",
                    model.ShortDescription
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Description",
                    model.Description
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "CreatedBy",
                    model.CreatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_Academics_OurProgram_Create",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = "Our Program  detail added successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to add our program  detail.";
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
        public async Task<DataResponse> UpdateAsync(OurProgramDTO model)
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
                    "ShortDescription",
                    model.ShortDescription
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Description",
                    model.Description
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "UpdatedBy",
                    model.UpdatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_Academics_OurProgram_Update",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = "Our Program detail updated successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to update our program detail.";
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
                        "sp_Academics_OurProgram_Delete",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message =
                        "Our Program detail deleted successfully.";

                    status = true;
                }
                else
                {
                    message =
                        "Failed to delete our program detail.";

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
