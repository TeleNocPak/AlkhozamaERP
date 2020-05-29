using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class SubDepartmentsDAL
    {
        public SubDepartmentsDAL()
        { }

        # region Variables
        DataOperations objDataOperations = new DataOperations();
        # endregion

       #region properties

        private int _DepartmentParentId;
        public int DepartmentParentId
        {
            get { return _DepartmentParentId; }
            set { _DepartmentParentId = value; }
        }
       # endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            try
            {
                SqlCommand pObjCommand = new SqlCommand();
                string sql = "Select * from Departments where DepartmentId=@DepartmentId";

                pObjCommand.CommandText = sql;
                pObjCommand.Parameters.AddWithValue("@DepartmentId", ID);

                UserDataTable = objDataOperations.GetData(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Sub Departments Records: {0}", exception.Message), exception);
            }
            return UserDataTable;
        }

        public DataTable GetDepartmentName()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT * FROM Departments WHERE DepartmentParentId = '0'";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("DepartmentParentId", DepartmentParentId);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Departments Records: {0}", exception.Message), exception);
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

                string sql = @"SELECT * FROM Departments WHERE DepartmentParentId != '0' and " + WhereClause + " order by DepartmentId desc";

                pObjCommand.CommandText = sql;
                pObjCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                pObjCommand.Parameters.AddWithValue("@DepartmentParentId", DepartmentParentId);
                UserDataTable = objDataOperations.GetData(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Sub Departments Records: {0}", exception.Message), exception);
            }
            return UserDataTable;
        }
        
        public void InsertRecord(int DepartmentParentId, string DepartmentName)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                string sql = @"INSERT INTO Departments(DepartmentParentId, DepartmentName, BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)        
                            VALUES (@DepartmentParentId, @DepartmentName, @BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                pObjCommand.CommandText = sql;
                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.Parameters.AddWithValue("@DepartmentParentId", DepartmentParentId);
                pObjCommand.Parameters.AddWithValue("@DepartmentName", DepartmentName);                
                pObjCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                pObjCommand.Parameters.AddWithValue("@AddedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@AddedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                pObjCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY("01/01/1900"));
                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Sub Departments: {0}", exception.Message), exception);
            }
        }

        public void UpdateRecord(int DepartmentId, int DepartmentParentId, string DepartmentName)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                string sql = @"Update Departments SET DepartmentParentId = @DepartmentParentId, DepartmentName = @DepartmentName,
                             UpdatedBy=@UpdatedBy, UpdatedOn=@UpdatedOn WHERE DepartmentId=@DepartmentId";

                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.CommandText = sql;

                pObjCommand.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                pObjCommand.Parameters.AddWithValue("@DepartmentParentId", DepartmentParentId);
                pObjCommand.Parameters.AddWithValue("@DepartmentName", DepartmentName);
                pObjCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Sub Departments Name: {0}", exception.Message), exception);
            }
        }

        public void DeleteRecord(int ID)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();
                string sql = @"Delete From Departments WHERE DepartmentId = @DepartmentId";

                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.CommandText = sql;

                pObjCommand.Parameters.AddWithValue("@DepartmentId", ID);
                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Sub Departments Record: {0}", exception.Message), exception);
            }
        }

        #endregion
    }
}
