using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class LoginDAL
    {
        #region Methods

        public DataTable GetLogin(string UserName, string Password)
        {
            DataTable LoginDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"select a.ID,a.GroupsID,a.UserId,
                               c.CompanyName,c.CompanyId,a.LocationId from Users a
                               inner join Location b on a.LocationId=b.LocationId
                               inner join Company c on b.CompanyId=c.CompanyId
                               where a.UserId=@UserId and a.Password=@Password";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserId", UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(LoginDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting User Login: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return LoginDataTable;
        }

        public DataTable GetLocations(string LocationId)
        {
            DataTable LoginDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                //                string sql = @"select LocationId,locationName FROM location
                //                               where locationId IN (select * from dbo.Split(@LocationId,','))";

                string sql = @"select LocationId,locationName FROM location
                             where locationId IN (" + LocationId + ")";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    //sqlCommand.Parameters.AddWithValue("@LocationId", LocationId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(LoginDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Locations: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return LoginDataTable;
        }

        public Boolean PasswordConfirmation(int UserId, string Password)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            Boolean isLogin = false;
            try
            {
                string sql = "Select * from Users where UserId=@UserId and Password=@Password";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    if (UserDataTable.Rows.Count > 0)
                    {
                        isLogin = true;
                        sqlAdapter.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Password Confirmation: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return isLogin;
        }

        public void UpdateLoginPassword(int UserId, string NewPassword)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update Users SET Password = @Password WHERE UserID = @UserID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Password", NewPassword);
                    sqlCommand.Parameters.AddWithValue("@UserID", UserId);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating User Password: {0}", exception.Message), exception);
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
