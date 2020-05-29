using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class CityDAL
    {
        public CityDAL()
        { }

        # region Variables
        DataOperations objDataOperations = new DataOperations();
        # endregion

       #region properties

        private int _CountryParentId;
        public int CountryParentId
        {
            get { return _CountryParentId; }
            set { _CountryParentId = value; }
        }
       # endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            try
            {
                SqlCommand pObjCommand = new SqlCommand();
                string sql = "Select * from Country where CountryId=@CountryId";

                pObjCommand.CommandText = sql;
                pObjCommand.Parameters.AddWithValue("@CountryId", ID);

                UserDataTable = objDataOperations.GetData(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Country Records: {0}", exception.Message), exception);
            }
            return UserDataTable;
        }

        public DataTable GetCountryName()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT * FROM Country WHERE CountryParentId = '0'";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("CountryParentId", CountryParentId);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Country Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }

        public DataTable GetAllRecords(string Search)
        {
            DataTable UserDataTable = new DataTable();
            string WhereClause = "BranchId=@BranchId";

            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT * FROM Country WHERE CountryParentId != '0' and " + WhereClause + " order by CountryId desc";

                pObjCommand.CommandText = sql;
                pObjCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                pObjCommand.Parameters.AddWithValue("@CountryParentId", CountryParentId);
                UserDataTable = objDataOperations.GetData(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Country Records: {0}", exception.Message), exception);
            }
            return UserDataTable;
        }

        public void InsertRecord(int CountryParentId, string Name)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                string sql = @"INSERT INTO Country(CountryParentId, Name, BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)        
                            VALUES (@CountryParentId, @Name, @BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                pObjCommand.CommandText = sql;
                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.Parameters.AddWithValue("@CountryParentId", CountryParentId);
                pObjCommand.Parameters.AddWithValue("@Name", Name);                
                pObjCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                pObjCommand.Parameters.AddWithValue("@AddedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@AddedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                pObjCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY("01/01/1900"));
                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Country: {0}", exception.Message), exception);
            }
        }

        public void UpdateRecord(int CountryId, int CountryParentId, string Name)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                string sql = @"Update Country SET CountryParentId = @CountryParentId, Name = @Name,
                             UpdatedBy=@UpdatedBy, UpdatedOn=@UpdatedOn WHERE CountryId=@CountryId";

                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.CommandText = sql;

                pObjCommand.Parameters.AddWithValue("@CountryId", CountryId);
                pObjCommand.Parameters.AddWithValue("@CountryParentId", CountryParentId);
                pObjCommand.Parameters.AddWithValue("@Name", Name);
                pObjCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Sub Country Name: {0}", exception.Message), exception);
            }
        }

        public void DeleteRecord(int ID)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();
                string sql = @"Delete From Country WHERE CountryId = @CountryId";

                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.CommandText = sql;

                pObjCommand.Parameters.AddWithValue("@CountryId", ID);
                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Country Record: {0}", exception.Message), exception);
            }
        }

        #endregion
    }
}
