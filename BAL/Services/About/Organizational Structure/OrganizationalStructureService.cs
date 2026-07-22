using Common.DbContext;
using DTO.Models.About;
using DTO.Models.DataResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.About.Organizational_Structure
{
    public class OrganizationalStructureService : MyDbContext, IOrganizationalStructureService
    {

        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_OrganizationalStructure_GetAll", CommandType.StoredProcedure));
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
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_OrganizationalStructure_GetById", CommandType.StoredProcedure));
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
            OrganizationalStructureDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Name", model.Name);
                _sqlCommand.Add_Parameter_WithValue("Designation", model.Designation);
                _sqlCommand.Add_Parameter_WithValue("Email", model.Email);
                _sqlCommand.Add_Parameter_WithValue("Phone", model.Phone);
                _sqlCommand.Add_Parameter_WithValue("ParentId", model.ParentId);
                _sqlCommand.Add_Parameter_WithValue("ProfilePhoto", model.ProfilePhoto);
                _sqlCommand.Add_Parameter_WithValue("DisplayOrder", model.DisplayOrder);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_OrganizationalStructure_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Organizational Structure Added Successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Organizational Structure";
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
            OrganizationalStructureDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("Name", model.Name);
                _sqlCommand.Add_Parameter_WithValue("Designation", model.Designation);
                _sqlCommand.Add_Parameter_WithValue("Email", model.Email);
                _sqlCommand.Add_Parameter_WithValue("Phone", model.Phone);
                _sqlCommand.Add_Parameter_WithValue("ParentId", model.ParentId);
                _sqlCommand.Add_Parameter_WithValue("ProfilePhoto", model.ProfilePhoto);
                _sqlCommand.Add_Parameter_WithValue("DisplayOrder", model.DisplayOrder);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_OrganizationalStructure_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Organizational Structure Updated successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Organizational Structure";
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

        public async Task<DataResponse> deleteAsync(OrganizationalStructureDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_OrganizationalStructure_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Organizational Structure Deleted successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete Organizational Structure";
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
