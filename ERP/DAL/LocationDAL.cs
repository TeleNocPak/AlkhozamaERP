using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class LocationDAL
    {
        public LocationDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Select a.LocationId,a.LocationName,a.BranchId,b.CompanyId from Location a
                              inner join Branch b on a.BranchId=b.BranchId  
                              where a.LocationID=@LocationID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LocationID", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Location Records: {0}", exception.Message), exception);
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

                string sql = @"SELECT a.LocationId,a.LocationName,b.BranchName,c.CompanyName FROM Location a
                               inner join Branch b on a.BranchId=b.BranchId  
                               inner join Company c on b.CompanyId=c.CompanyId  
                               where " + WhereClause + " order by a.LocationId desc";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Location Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable LoginIDExist(int UserID, string LoginID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from UserMaster where LoginID=@LoginID and UserId!=@UserId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LoginID", LoginID);
                    sqlCommand.Parameters.AddWithValue("@UserId", UserID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Login Id Exist: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetCompany()
        {
            DataTable RolesDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select CompanyId,CompanyName from Company order by CompanyName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(RolesDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Company Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return RolesDataTable;
        }

        public DataTable GetBranch(int CompanyId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT BranchId,BranchName FROM Branch WHERE CompanyId=@CompanyId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CompanyId", CompanyId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Branch Name Records in Location: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public void InsertRecord(string LocationName,int BranchId)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO Location(LocationName,BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)        
                            VALUES (@LocationName,@BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LocationName", LocationName);
                    sqlCommand.Parameters.AddWithValue("@BranchId", BranchId);

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
                throw new Exception(string.Format("Error occured while inserting Location: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void UpdateRecord(int LocationID, string LocationName, int BranchId)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update Location SET LocationName = @LocationName, BranchId=@BranchId, UpdatedBy=@UpdatedBy,
                               UpdatedOn=@UpdatedOn WHERE LocationID=@LocationID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LocationID", LocationID);
                    sqlCommand.Parameters.AddWithValue("@LocationName", LocationName);
                    sqlCommand.Parameters.AddWithValue("@BranchId", BranchId);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Location Name: {0}", exception.Message), exception);
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
                string sql = @"Delete From Location WHERE LocationID = @LocationID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LocationID", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Location Record: {0}", exception.Message), exception);
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
