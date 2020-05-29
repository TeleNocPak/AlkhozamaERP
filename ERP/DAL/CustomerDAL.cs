using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class CustomerDAL
    {
        public CustomerDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int CustomerID { get; set; }             
        public string CustomerCode { get; set; }             
        public string  CustomerName { get; set; } 
        public string  BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string Zone { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }                     
        public string Phone2 { get; set; }                     
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }                     
        public string WebSite { get; set; }
        public string GSTNo { get; set; }
        public int LimitID { get; set; }             

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from Customers where CustomerID=@CustomerID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CustomerID", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Customer Records: {0}", exception.Message), exception);
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
            string WhereClause = "BranchId=@BranchId";
            
            try
            {
                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT * FROM Customers  where " + WhereClause + " order by CustomerID desc";

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
                throw new Exception(string.Format("Error occured while getting Customer Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetCreditLimit()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"select LimitId,convert(varchar(20),LimitAmount) + ' - ' + 
                               convert(varchar(20),LimitDays) + ' Days' as LimitAmountDays from LimitInformation";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Credit Limit Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }


        public void InsertRecord(CustomerDAL objCustomerDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO Customers(CustomerCode,CustomerName,BillingAddress,ShippingAddress,
                             City, Zone, Email,Phone1,Phone2,MobileNo,
                             FaxNo,WebSite,GSTNo,LimitId,BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)
                             VALUES (@CustomerCode,@CustomerName,@BillingAddress,@ShippingAddress,
                             @City, @Zone, @Email,@Phone1,@Phone2,@MobileNo,
                             @FaxNo,@WebSite,@GSTNo,@LimitId,@BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CustomerCode", objCustomerDAL.CustomerCode);
                    sqlCommand.Parameters.AddWithValue("@CustomerName", objCustomerDAL.CustomerName);
                    sqlCommand.Parameters.AddWithValue("@BillingAddress", objCustomerDAL.BillingAddress);
                    sqlCommand.Parameters.AddWithValue("@ShippingAddress", objCustomerDAL.ShippingAddress);
                    sqlCommand.Parameters.AddWithValue("@City", objCustomerDAL.City);
                    sqlCommand.Parameters.AddWithValue("@Zone", objCustomerDAL.Zone);
                    sqlCommand.Parameters.AddWithValue("@Email", objCustomerDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@Phone1", objCustomerDAL.Phone1);
                    sqlCommand.Parameters.AddWithValue("@Phone2", objCustomerDAL.Phone2);
                    sqlCommand.Parameters.AddWithValue("@MobileNo", objCustomerDAL.MobileNo);
                    sqlCommand.Parameters.AddWithValue("@FaxNo", objCustomerDAL.FaxNo);
                    sqlCommand.Parameters.AddWithValue("@WebSite", objCustomerDAL.WebSite);
                    sqlCommand.Parameters.AddWithValue("@GSTNo", objCustomerDAL.GSTNo);
                    sqlCommand.Parameters.AddWithValue("@LimitID", objCustomerDAL.LimitID);

                    sqlCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
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
                throw new Exception(string.Format("Error occured while inserting Customer: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void UpdateRecord(CustomerDAL objCustomerDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update Customers SET CustomerCode = @CustomerCode, CustomerName = @CustomerName,
                              BillingAddress = @BillingAddress, ShippingAddress = @ShippingAddress, 
                              City = @City, Zone = @Zone, Email = @Email, Phone1 = @Phone1,Phone2 = @Phone2,
                              MobileNo = @MobileNo, FaxNo = @FaxNo, WebSite = @WebSite, GSTNo = @GSTNo, LimitID=@LimitID,
                              UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn WHERE CustomerID = @CustomerID";
            
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CustomerID", objCustomerDAL.CustomerID);
                    sqlCommand.Parameters.AddWithValue("@CustomerCode", objCustomerDAL.CustomerCode);
                    sqlCommand.Parameters.AddWithValue("@CustomerName", objCustomerDAL.CustomerName);
                    sqlCommand.Parameters.AddWithValue("@BillingAddress", objCustomerDAL.BillingAddress);
                    sqlCommand.Parameters.AddWithValue("@ShippingAddress", objCustomerDAL.ShippingAddress);
                    sqlCommand.Parameters.AddWithValue("@City", objCustomerDAL.City);
                    sqlCommand.Parameters.AddWithValue("@Zone", objCustomerDAL.Zone);
                    sqlCommand.Parameters.AddWithValue("@Email", objCustomerDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@Phone1", objCustomerDAL.Phone1);
                    sqlCommand.Parameters.AddWithValue("@Phone2", objCustomerDAL.Phone2);
                    sqlCommand.Parameters.AddWithValue("@MobileNo", objCustomerDAL.MobileNo);
                    sqlCommand.Parameters.AddWithValue("@FaxNo", objCustomerDAL.FaxNo);
                    sqlCommand.Parameters.AddWithValue("@WebSite", objCustomerDAL.WebSite);
                    sqlCommand.Parameters.AddWithValue("@GSTNo", objCustomerDAL.GSTNo);
                    sqlCommand.Parameters.AddWithValue("@LimitID", objCustomerDAL.LimitID);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Customer: {0}", exception.Message), exception);
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
                string sql = @"Delete From Customers WHERE CustomerID = @CustomerID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CustomerID", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Branch: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
    }

        #endregion
    }

