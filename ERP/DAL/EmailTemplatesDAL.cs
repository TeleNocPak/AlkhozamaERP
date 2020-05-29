using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class EmailTemplatesDAL
    {
        public EmailTemplatesDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int ID { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public int Active { get; set; }

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select TemplateName,Subject,EmailBody,Active from EmailTemplates where TemplateId=@TemplateId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@TemplateId", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Email Tamplate Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }
        public DataTable GetAllRecords(string Search)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            string WhereClause = "1=1";

            try
            {
                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT TemplateId,TemplateName,Subject,EmailBody,Active FROM EmailTemplates where " + WhereClause + "";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Email Tamplate Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }
        public void InsertRecord(EmailTemplatesDAL objEmailTemplatesDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                //
                string sql = @"INSERT INTO EmailTemplates(TemplateName,Subject,EmailBody,Active,BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn) 
                             VALUES (@TemplateName,@Subject,@EmailBody,@Active,@BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@TemplateName", objEmailTemplatesDAL.TemplateName);
                    sqlCommand.Parameters.AddWithValue("@Subject", objEmailTemplatesDAL.Subject);
                    sqlCommand.Parameters.AddWithValue("@EmailBody", objEmailTemplatesDAL.EmailBody);
                    sqlCommand.Parameters.AddWithValue("@Active", objEmailTemplatesDAL.Active);
                    sqlCommand.Parameters.AddWithValue("@BranchId",  CommonObjects.GetBranchId());

                    sqlCommand.Parameters.AddWithValue("@AddedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@AddedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY("01/01/1900"));
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Company: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void UpdateRecord(EmailTemplatesDAL objEmailTemplatesDAL)
        {
            //BranchId=@BranchId,
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {             
              
                string sql = @"Update EmailTemplates SET  TemplateName=@TemplateName, Subject=@Subject, EmailBody=@EmailBody,
                Active=@Active,  UpdatedBy=@UpdatedBy, UpdatedOn=@UpdatedOn WHERE TemplateId = @TemplateId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@TemplateId", objEmailTemplatesDAL.TemplateId);
                    sqlCommand.Parameters.AddWithValue("@TemplateName", objEmailTemplatesDAL.TemplateName);
                    sqlCommand.Parameters.AddWithValue("@Subject", objEmailTemplatesDAL.Subject);
                    sqlCommand.Parameters.AddWithValue("@EmailBody", objEmailTemplatesDAL.EmailBody);
                    sqlCommand.Parameters.AddWithValue("@Active", objEmailTemplatesDAL.Active);
                    //sqlCommand.Parameters.AddWithValue("@BranchId", objEmailTemplatesDAL.);//////                   

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Email Tamplate: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void DeleteRecord(int ID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Delete From EmailTemplates WHERE TemplateId = @TemplateId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@TemplateId", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Email Template: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        #endregion
    }
}
