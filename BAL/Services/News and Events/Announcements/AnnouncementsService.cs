using Common.DbContext;
using DTO.Models.About;
using DTO.Models.DataResponse;
using DTO.Models.News_and_Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.News_and_Events.Announcemets
{
    public class AnnouncementsService: MyDbContext, IAnnouncementsService
    {
        public async Task<DataTable> GetAllIsActiveAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_NewsAndEvents_Announcemets_GetAll_IsActive", CommandType.StoredProcedure));
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

        public async Task<DataTable> GetAllInActiveAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_NewsAndEvents_Announcemets_GetAll_InActive", CommandType.StoredProcedure));
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
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_NewsAndEvents_Announcemets_GetById", CommandType.StoredProcedure));
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
            AnnouncementsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("Category", model.Category);
                _sqlCommand.Add_Parameter_WithValue("StartDate", model.StartDate);
                _sqlCommand.Add_Parameter_WithValue("EndDate", model.EndDate);
                _sqlCommand.Add_Parameter_WithValue("FilePath", model.FilePath);
                _sqlCommand.Add_Parameter_WithValue("Urgent", model.Urgent);
                _sqlCommand.Add_Parameter_WithValue("CreatedBy", model.CreatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_NewsAndEvents_Announcemets_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Announcemets Added Successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Announcemets";
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
            AnnouncementsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                _sqlCommand.Add_Parameter_WithValue("Title", model.Title);
                _sqlCommand.Add_Parameter_WithValue("Category", model.Category);
                _sqlCommand.Add_Parameter_WithValue("StartDate", model.StartDate);
                _sqlCommand.Add_Parameter_WithValue("EndDate", model.EndDate);
                _sqlCommand.Add_Parameter_WithValue("FilePath", model.FilePath);
                _sqlCommand.Add_Parameter_WithValue("Urgent", model.Urgent);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", model.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_NewsAndEvents_Announcemets_Update", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Announcemets Updated Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Announcemets";
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


        public async Task<DataResponse> ArchiveAsync(string Id, string UpdatedBy)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", Id);
                _sqlCommand.Add_Parameter_WithValue("UpdatedBy", UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_NewsAndEvents_Announcemets_Archive", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Announcemets Archived Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Archive Announcemets";
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

        public async Task<DataResponse> deleteAsync(AnnouncementsDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("Id", model.Id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_NewsAndEvents_Announcemets_Delete", CommandType.StoredProcedure));
                if (item)
                {
                    message = "News & Events Archives Deleted Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Delete News & Events Archives";
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
