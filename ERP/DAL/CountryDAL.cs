using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class CountryDAL
    {
        public CountryDAL()
        { }

        # region Variables
        DataOperations objDataOperations = new DataOperations();
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

        public DataTable GetAllRecords(string Search)
        {
            DataTable UserDataTable = new DataTable();
            string WhereClause = "BranchId=@BranchId";

            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT * FROM Country WHERE CountryParentId = '0' and " + WhereClause + " order by CountryId DESC";

                pObjCommand.CommandText = sql;                
                pObjCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());

                UserDataTable = objDataOperations.GetData(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Country Records: {0}", exception.Message), exception);
            }
            return UserDataTable;
        }
        
        public void InsertRecord(string Name)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();
                string CountryParentId = "0";

                string sql = @"INSERT INTO Country(Name, CountryParentId, BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)        
                            VALUES (@Name, @CountryParentId, @BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                pObjCommand.CommandText = sql;
                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.Parameters.AddWithValue("@Name", Name);
                pObjCommand.Parameters.AddWithValue("@CountryParentId", CountryParentId);
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

        public void UpdateRecord(int CountryId, string Name)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                string sql = @"Update Country SET Name = @Name,UpdatedBy=@UpdatedBy,
                               UpdatedOn=@UpdatedOn WHERE CountryId=@CountryId";

                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.CommandText = sql;

                pObjCommand.Parameters.AddWithValue("@CountryId", CountryId);
                pObjCommand.Parameters.AddWithValue("@Name", Name);
                pObjCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Country Name: {0}", exception.Message), exception);
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
