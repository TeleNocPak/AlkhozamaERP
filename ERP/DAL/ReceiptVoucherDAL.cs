﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class ReceiptVoucherDAL
    {
        public ReceiptVoucherDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string BookType { get; set; }
        public string AccountType { get; set; }

        public string ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public string NarrationMaster { get; set; }

        public int VoucherrDetailId { get; set; }
        public int AccountId { get; set; }
        public Decimal DebitAmount { get; set; }
        public Decimal CreditAmount { get; set; }
        public string NarrationDetail { get; set; }

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int VoucherId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT * FROM VoucherMaster where VoucherId=@VoucherId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@VoucherId", VoucherId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Voucher Master Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetDetailRecords(int VoucherId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT b.*,c.AccountName,c.AccountCode FROM VoucherMaster a
                               inner join voucherDetail b on a.VoucherId=b.VoucherId
                               inner join COA c on b.AccountId=c.AccountId
                               where a.VoucherId=@VoucherId order by VoucherDetailId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@VoucherId", VoucherId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Order Requisition Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            string WhereClause = "BookType!='JV' and BookType!='CP' and BookType!='BP' ";

            try
            {
                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT  VoucherId,VoucherCode,VoucherDate,NarrationMaster,
                                CASE AutoPost
                                    WHEN 0 THEN 'UNPOST'
                                    ELSE 'UNPOSTED'
                                END AutoPost,
                                CASE BookType
                                    WHEN 'CR' THEN 'CASH RECEIPT'
                                    WHEN 'CP' THEN 'CASH PAYMENT'
                                    WHEN 'BR' THEN 'BANK RECEIPT'
                                    ELSE 'BANK PAYMENT'
                                END BookType 
                              FROM VoucherMaster where " + WhereClause + " order by VoucherId desc";

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
                throw new Exception(string.Format("Error occured while getting Order Requisition Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public string GetAccountCode(int AccountId)
        {
            DataTable getData = new DataTable();
            string ProductCode = string.Empty;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select AccountCode from COA where AccountId=@AccountId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@AccountId", AccountId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    if (getData.Rows.Count > 0)
                        ProductCode = getData.Rows[0]["AccountCode"].ToString();
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Product Code: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return ProductCode;
        }

        public DataTable OrderExistinConfirm(int VoucherId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"SELECT * FROM OrderConfirmationMaster   
                               where VoucherId=@VoucherId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@VoucherId", VoucherId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Order Confirm detail Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public bool GetAccountNameIdExist(int AccountId, string AccountName)
        {
            DataTable getData = new DataTable();
            bool ProductCodeExist = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select AccountCode from COA where AccountId=@AccountId and AccountName=@AccountName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@AccountId", AccountId);
                    sqlCommand.Parameters.AddWithValue("@AccountName", AccountName);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    if (getData.Rows.Count > 0)
                        ProductCodeExist = true;
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting COA: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return ProductCodeExist;
        }

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

        public int GetMaxRecords()
        {
            int MaxCode = 0;
            DataTable DataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT  isNull(Max(VoucherId),0) as MaxCode FROM VoucherMaster";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(DataTable);

                    if (Convert.ToInt32(DataTable.Rows[0]["MaxCode"]) != 0)
                        MaxCode = Convert.ToInt32(DataTable.Rows[0]["MaxCode"]) + 1;
                    else
                        MaxCode = 1;

                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Max Records Voucher Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }

        public int InsertMasterRecord(ReceiptVoucherDAL objReceiptVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            int MaxCode = 0;
            try
            {
                MaxCode = GetMaxRecords();
                string VoucherAutoCode = objReceiptVoucherDAL.BookType +  CommonObjects.FormatMaxCode(MaxCode);

                string sql = @"INSERT INTO VoucherMaster (VoucherId,VoucherCode,VoucherDate,
                             BookType,ChequeNo,ChequeDate,AutoPost,NarrationMaster,BranchId,LocationId,AddedBy,AddedOn)
                             VALUES (@VoucherId,@VoucherCode,@VoucherDate,@BookType,@ChequeNo,@ChequeDate,@AutoPost,
                             @NarrationMaster,@BranchId,@LocationId,@AddedBy,@AddedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    int AutoPost = 1;
                    sqlCommand.Parameters.AddWithValue("@VoucherId", MaxCode);
                    sqlCommand.Parameters.AddWithValue("@VoucherCode", VoucherAutoCode);
                    sqlCommand.Parameters.AddWithValue("@VoucherDate", objReceiptVoucherDAL.VoucherDate);
                    sqlCommand.Parameters.AddWithValue("@BookType", objReceiptVoucherDAL.BookType);

                    sqlCommand.Parameters.AddWithValue("@ChequeNo", objReceiptVoucherDAL.ChequeNo);
                    if (string.IsNullOrWhiteSpace(objReceiptVoucherDAL.ChequeDate))
                        sqlCommand.Parameters.AddWithValue("@ChequeDate", DBNull.Value);
                    else
                        sqlCommand.Parameters.AddWithValue("@ChequeDate", CommonObjects.ConvertMMDDYYYY(objReceiptVoucherDAL.ChequeDate));

                    sqlCommand.Parameters.AddWithValue("@AutoPost", AutoPost);
                    sqlCommand.Parameters.AddWithValue("@NarrationMaster", objReceiptVoucherDAL.NarrationMaster);

                    sqlCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                    sqlCommand.Parameters.AddWithValue("@LocationId", CommonObjects.GetLocationId());

                    sqlCommand.Parameters.AddWithValue("@AddedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@AddedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Payment Voucher Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }
        public void UpdateMasterRecord(ReceiptVoucherDAL objReceiptVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Update VoucherMaster SET VoucherDate = @VoucherDate, BookType = @BookType,ChequeNo=@ChequeNo,ChequeDate=@ChequeDate ,
                              NarrationMaster=@NarrationMaster, UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn Where VoucherId=@VoucherId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherDate", objReceiptVoucherDAL.VoucherDate);
                    sqlCommand.Parameters.AddWithValue("@BookType", objReceiptVoucherDAL.BookType);
                    sqlCommand.Parameters.AddWithValue("@NarrationMaster", objReceiptVoucherDAL.NarrationMaster);

                    sqlCommand.Parameters.AddWithValue("@ChequeNo", objReceiptVoucherDAL.ChequeNo);
                    if (string.IsNullOrWhiteSpace(objReceiptVoucherDAL.ChequeDate))
                        sqlCommand.Parameters.AddWithValue("@ChequeDate", DBNull.Value);
                    else
                        sqlCommand.Parameters.AddWithValue("@ChequeDate", CommonObjects.ConvertMMDDYYYY(objReceiptVoucherDAL.ChequeDate));

                    sqlCommand.Parameters.AddWithValue("@VoucherId", objReceiptVoucherDAL.VoucherId);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Payment Voucher Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void InsertDetailRecord(ReceiptVoucherDAL objReceiptVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO voucherDetail(VoucherId,AccountId,DebitAmount,CreditAmount,AccountType)
                             VALUES (@VoucherId,@AccountId,@DebitAmount,@CreditAmount,@AccountType)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", objReceiptVoucherDAL.VoucherId);
                    sqlCommand.Parameters.AddWithValue("@AccountId", objReceiptVoucherDAL.AccountId);
                    sqlCommand.Parameters.AddWithValue("@DebitAmount", objReceiptVoucherDAL.DebitAmount);
                    sqlCommand.Parameters.AddWithValue("@CreditAmount", objReceiptVoucherDAL.CreditAmount);
                    sqlCommand.Parameters.AddWithValue("@AccountType", objReceiptVoucherDAL.AccountType);
                    
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Payment Voucher Detail: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void DeleteMasterDetailRecord(int ID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Delete From VoucherMaster WHERE VoucherId = @VoucherId";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
                sql = @"Delete From voucherDetail WHERE VoucherId = @VoucherId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Payment voucher Master & Detail : {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void DeleteDetailRecord(int ID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Delete From VoucherDetail WHERE VoucherId = @VoucherId";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting voucher Detail : {0}", exception.Message), exception);
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




