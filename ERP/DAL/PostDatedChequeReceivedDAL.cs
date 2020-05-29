using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class PostDatedChequeReceivedDAL
    {
        public PostDatedChequeReceivedDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int ID { get; set; }
        public int PostDatedChequeId { get; set; }
        public int Dealer { get; set; }
        public int PaidBankId { get; set; }
        public string BranchName { get; set; }
        public string BankName { get; set; }        
        public string ChequeNo { get; set; }
        public string Narration { get; set; }
        public string DepositSlipNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Comments { get; set; }

        #endregion

        #region Methods


        public DataTable GetMasterRecord(int PostDatedChequeId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT a.*, d.DealerName FROM
                               PostDatedChequeMaster a
                               Inner Join Dealers d On a.DealerID=d.DealerID
                               where PostDatedChequeId = @PostDatedChequeId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", PostDatedChequeId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Post Dated Cheque: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetDetailRecords(int PostDatedChequeId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT b.* FROM PostDatedChequeMaster a
                              inner join PostDatedChequeDetail b on a.PostDatedChequeId=b.PostDatedChequeId                              
                              where a.PostDatedChequeId = @PostDatedChequeId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", PostDatedChequeId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Post Dated Cheque Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from PostDatedChequeMaster where PostDatedChequeId=@PostDatedChequeId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Post Dated Cheque Records: {0}", exception.Message), exception);
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
            string WhereClause = "a.BranchId=@BranchId";

            try
            {
                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT a.*, b.*, d.DealerName
                               FROM PostDatedChequeMaster a
                               inner join PostDatedChequeDetail b on a.PostDatedChequeId=b.PostDatedChequeId
                               inner join Dealers d on a.DealerID = d.DealerID
                               where " + WhereClause + " order by a.PostDatedChequeId desc";

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
                throw new Exception(string.Format("Error occured while getting Post Dated Cheque Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetDealer()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select DealerID,DealerName from Dealers";
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
                throw new Exception(string.Format("Error occured while getting Dealer Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }

        public bool GetDealerNameExist(int DealerID, string DealerName)
        {
            DataTable getData = new DataTable();
            bool ProductCodeExist = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select DealerID, DealerName from Dealers where DealerID=DealerID and DealerName=@DealerName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@DealerID", DealerID);
                    sqlCommand.Parameters.AddWithValue("@DealerName", DealerName);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    if (getData.Rows.Count > 0)
                        ProductCodeExist = true;
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Dealer Name: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return ProductCodeExist;
        }

        public DataTable GetMaxRecords()
        {
            DataTable RoleDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT  isNull(Max(PostDatedChequeId),0) as PostDatedChequeId FROM PostDatedChequeMaster";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(RoleDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Post Dated Cheque Master Max Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return RoleDataTable;
        }

        public void InsertMasterRecord(PostDatedChequeReceivedDAL objPostDatedChequeReceivedDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO PostDatedChequeMaster(PostDatedChequeId,DealerId,SubmitDate,Comments,BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn) 
                               VALUES (@PostDatedChequeId,@DealerId,@SubmitDate,@Comments,@BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", objPostDatedChequeReceivedDAL.ID);
                    sqlCommand.Parameters.AddWithValue("@DealerId", objPostDatedChequeReceivedDAL.Dealer);
                    sqlCommand.Parameters.AddWithValue("@SubmitDate", objPostDatedChequeReceivedDAL.SubmitDate);
                    sqlCommand.Parameters.AddWithValue("@Comments", objPostDatedChequeReceivedDAL.Comments);

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
                throw new Exception(string.Format("Error occured while inserting Post Dated Cheque Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void UpdateMasterRecord(PostDatedChequeReceivedDAL objPostDatedChequeReceivedDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update PostDatedCheque SET DealerId=@DealerId, SubmitDate=@SubmitDate, Comments=@Comments,
                               UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn WHERE PostDatedChequeId = @PostDatedChequeId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", objPostDatedChequeReceivedDAL.ID);
                    sqlCommand.Parameters.AddWithValue("@DealerId", objPostDatedChequeReceivedDAL.Dealer);
                    sqlCommand.Parameters.AddWithValue("@SubmitDate", objPostDatedChequeReceivedDAL.SubmitDate);
                    sqlCommand.Parameters.AddWithValue("@Comments", objPostDatedChequeReceivedDAL.Comments);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY("01/01/1900"));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Post Dated Cheque Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void InsertDetailRecord(PostDatedChequeReceivedDAL objPostDatedChequeReceivedDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                int AccountReceived = 0;
                int PaidStatus = 0;

                string sql = @"INSERT INTO PostDatedChequeDetail(PostDatedChequeId,ChequeDate,BankName,BranchName,ChequeNo,Amount,PaidStatus,AccountReceived,
                             AddedBy,AddedOn,UpdatedBy,UpdatedOn)
                             VALUES (@PostDatedChequeId,@ChequeDate,@BankName,@BranchName,@ChequeNo,@Amount,@PaidStatus,@AccountReceived,
                             @AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", objPostDatedChequeReceivedDAL.PostDatedChequeId);
                    sqlCommand.Parameters.AddWithValue("@ChequeDate", objPostDatedChequeReceivedDAL.ChequeDate);
                    sqlCommand.Parameters.AddWithValue("@BankName", objPostDatedChequeReceivedDAL.BankName);
                    sqlCommand.Parameters.AddWithValue("@BranchName", objPostDatedChequeReceivedDAL.BranchName);
                    sqlCommand.Parameters.AddWithValue("@ChequeNo", objPostDatedChequeReceivedDAL.ChequeNo);
                    sqlCommand.Parameters.AddWithValue("@Amount", objPostDatedChequeReceivedDAL.Amount);

                    sqlCommand.Parameters.AddWithValue("@PaidStatus", PaidStatus);
                    sqlCommand.Parameters.AddWithValue("@AccountReceived", AccountReceived);

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
                throw new Exception(string.Format("Error occured while inserting Post Dated Cheque Detail: {0}", exception.Message), exception);
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
                string sql = @"Delete From PostDatedChequeMaster WHERE PostDatedChequeId = @PostDatedChequeId";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
                sql = @"Delete From PostDatedChequeDetail WHERE PostDatedChequeId = @PostDatedChequeId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Post Dated Cheque Detail: {0}", exception.Message), exception);
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
                string sql = @"Delete From PostDatedChequeDetail WHERE PostDatedChequeId = @PostDatedChequeId";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Detail Post Dated Cheque: {0}", exception.Message), exception);
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
                string sql = @"Delete From PostDatedChequeMaster WHERE PostDatedChequeID = @PostDatedChequeID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeID", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Post Dated Cheque: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void UpdateAccountStatus(int ID, bool AccountReceived)
        {
            try
            {
                int PaidStatus = 1;

                SqlCommand pObjCommand = new SqlCommand();

                string sql = @"Update PostDatedChequeDetail SET AccountReceived=@AccountReceived, PaidStatus=@PaidStatus, UpdatedBy=@UpdatedBy,
                               UpdatedOn=@UpdatedOn WHERE PostDatedChequeDetailId=@PostDatedChequeDetailId";

                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.CommandText = sql;

                pObjCommand.Parameters.AddWithValue("@PostDatedChequeDetailId", ID);
                pObjCommand.Parameters.AddWithValue("@AccountReceived", AccountReceived);
                pObjCommand.Parameters.AddWithValue("@PaidStatus ", PaidStatus);
                pObjCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Paid Status: {0}", exception.Message), exception);
            }
        }

        public void UpdateAccontPaid(PostDatedChequeReceivedDAL objPostDatedChequeReceivedDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update PostDatedChequeDetail SET PaidDate=@PaidDate, PaidBankId=@PaidBankId, DepositSlipNo=@DepositSlipNo,
                            Narration=@Narration, UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn WHERE PostDatedChequeDetailId = @PostDatedChequeDetailId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@PostDatedChequeDetailId", objPostDatedChequeReceivedDAL.PostDatedChequeId);
                    sqlCommand.Parameters.AddWithValue("@PaidDate", objPostDatedChequeReceivedDAL.PaidDate);
                    sqlCommand.Parameters.AddWithValue("@PaidBankId", objPostDatedChequeReceivedDAL.PaidBankId);
                    sqlCommand.Parameters.AddWithValue("@DepositSlipNo", objPostDatedChequeReceivedDAL.DepositSlipNo);
                    sqlCommand.Parameters.AddWithValue("@Narration", objPostDatedChequeReceivedDAL.Narration);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Post Dated Cheque Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public DataTable GetBankName()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {

                string sql = "Select BankID,BankName  from Bank";
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
                throw new Exception(string.Format("Error occured while getting Bank Name Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }

        #endregion
    }
}