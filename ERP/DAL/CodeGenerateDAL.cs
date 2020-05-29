using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class CodeGenerateDAL
    {
        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region Methods

        public int GetAutoCodeGenerate(string CodeType)
        {
            int MaxCode = 0;
            DataTable DataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT  isNull(Max(Code),0) as MaxCode FROM uniquecode where CodeType=@CodeType";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CodeType", CodeType);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(DataTable);
                    
                    //MaxCode = Convert.ToInt32(DataTable.Rows[0]["MaxCode"])!=0 : 
                    if (Convert.ToInt32(DataTable.Rows[0]["MaxCode"]) != 0)
                        MaxCode = Convert.ToInt32(DataTable.Rows[0]["MaxCode"]) + 1;
                    else
                        MaxCode = 1;

                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Order Requisition Max Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }

        #endregion
    }
}

