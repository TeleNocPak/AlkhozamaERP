using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class CommonMethodsDAL
    {
        #region Methods

        public static bool RecordAlredayExist(int Id, string Name, string ColumnId, string ColumnName, string TableName)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            bool isExist = false;
            try
            {
                string sql = "Select * from " + TableName + " where " + ColumnName + " =@Name and " + ColumnId + "!=@Id";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", Id);
                    sqlCommand.Parameters.AddWithValue("@Name", Name);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    if (UserDataTable.Rows.Count > 0)
                        isExist = true;
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting  Record Alreday Exist: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return isExist;
        }

        

        #endregion
    }
}
