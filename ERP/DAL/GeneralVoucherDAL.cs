using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class GeneralVoucherDAL
    {
        public GeneralVoucherDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string BookType { get; set; }

        public DateTime ChequeDate { get; set; }
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
                               inner join VoucherDetail b on a.VoucherId=b.VoucherId
                               inner join COA c on b.AccountId=c.AccountId
                               where a.VoucherId=@VoucherId";

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
                throw new Exception(string.Format("Error occured while getting Voucher Detail Records: {0}", exception.Message), exception);
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
            string WhereClause = "BookType='JV'";

            try
            {
                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT  VoucherId,VoucherCode,VoucherDate,
                                CASE AutoPost
                                    WHEN 0 THEN 'UNPOST'
                                    ELSE 'UNPOSTED'
                                END AutoPost
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
                throw new Exception(string.Format("Error occured while getting Journal Voucher Master Records: {0}", exception.Message), exception);
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

        public int InsertMasterRecord(GeneralVoucherDAL objGeneralVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            int MaxCode = 0;
            try
            {
                MaxCode = GetMaxRecords();
                string VoucherAutoCode = "JV" + System.DateTime.Now.ToString("MM") + System.DateTime.Now.ToString("yy") + FormatMaxCode(MaxCode);

                string sql = @"INSERT INTO VoucherMaster (VoucherId,VoucherCode,VoucherDate,BookType,AutoPost,NarrationMaster,
                             BranchId,LocationId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)
                             VALUES (@VoucherId,@VoucherCode,@VoucherDate,@BookType,@AutoPost,@NarrationMaster,
                             @BranchId,@LocationId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    int AutoPost = 0;
                    sqlCommand.Parameters.AddWithValue("@VoucherId", MaxCode);
                    sqlCommand.Parameters.AddWithValue("@VoucherCode", VoucherAutoCode);
                    sqlCommand.Parameters.AddWithValue("@VoucherDate", objGeneralVoucherDAL.VoucherDate);
                    sqlCommand.Parameters.AddWithValue("@BookType", objGeneralVoucherDAL.BookType);
                    sqlCommand.Parameters.AddWithValue("@AutoPost", AutoPost);
                    sqlCommand.Parameters.AddWithValue("@NarrationMaster", objGeneralVoucherDAL.NarrationMaster);

                    sqlCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                    sqlCommand.Parameters.AddWithValue("@LocationId", CommonObjects.GetLocationId());

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
                throw new Exception(string.Format("Error occured while inserting Journal Voucher Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }
        public void UpdateMasterRecord(GeneralVoucherDAL objGeneralVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Update VoucherMaster SET VoucherDate = @VoucherDate, NarrationMaster=@NarrationMaster,
                                UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn Where VoucherId=@VoucherId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherDate", objGeneralVoucherDAL.VoucherDate);   
                    sqlCommand.Parameters.AddWithValue("@NarrationMaster", objGeneralVoucherDAL.NarrationMaster);
                    sqlCommand.Parameters.AddWithValue("@VoucherId", objGeneralVoucherDAL.VoucherId);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Journal Voucher Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void InsertDetailRecord(GeneralVoucherDAL objGeneralVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO VoucherDetail(VoucherId,AccountId,DebitAmount,CreditAmount,NarrationDetail)
                             VALUES (@VoucherId,@AccountId,@DebitAmount,@CreditAmount,@NarrationDetail)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", objGeneralVoucherDAL.VoucherId);
                    sqlCommand.Parameters.AddWithValue("@AccountId", objGeneralVoucherDAL.AccountId);
                    sqlCommand.Parameters.AddWithValue("@DebitAmount", objGeneralVoucherDAL.DebitAmount);
                    sqlCommand.Parameters.AddWithValue("@CreditAmount", objGeneralVoucherDAL.CreditAmount);
                    sqlCommand.Parameters.AddWithValue("@NarrationDetail", objGeneralVoucherDAL.NarrationDetail);
                    
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Journal Voucher Detail: {0}", exception.Message), exception);
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
                sql = @"Delete From VoucherDetail WHERE VoucherId = @VoucherId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Journal Master & Detail : {0}", exception.Message), exception);
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
                throw new Exception(string.Format("Error occured while Deleting Journal Voucher Detail : {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        private string FormatMaxCode(int Code)
        {
            string NewCode = string.Empty;
            if (Code.ToString().Length.Equals(1))
                NewCode = "0000" + Code.ToString();
            else if (Code.ToString().Length.Equals(2))
                NewCode = "000" + Code.ToString();
            else if (Code.ToString().Length.Equals(3))
                NewCode = "00" + Code.ToString();
            else if (Code.ToString().Length.Equals(4))
                NewCode = "0" + Code.ToString();
            else
                NewCode = Code.ToString();

            return NewCode;
        }

        #endregion
    }
}

