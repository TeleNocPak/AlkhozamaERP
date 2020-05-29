using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class ReportParameterInventoryDAL
    {
        public ReportParameterInventoryDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        #endregion

        #region Methods   


        public DataTable GetDeliveryPoint()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select BranchId,BranchName from Branch where CompanyId=@CompanyId order by BranchName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CompanyId", CommonObjects.GetCompanyId());
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Branch Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }

        public DataTable GetWareHouse(int BranchId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Select WarehouseId,WarehouseName from warehouse where BranchId=@BranchId order by WarehouseName";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BranchId", BranchId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting warehouse in Invtory Report: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        #endregion
    }
}
