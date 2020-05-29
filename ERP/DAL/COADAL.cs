using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class COADAL
    {
        public COADAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int AccountId { get; set; }

        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public int ParentAccountId { get; set; }

        public int AccountBranchId { get; set; }
        public long OpeningBalance { get; set; }

        public int LocationId { get; set; }
        public int Appeared { get; set; }

        #endregion

        #region Methods

        public DataTable GetCOARecord()
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                //string sql = "Select  AccountId,AccountCode,AccountName,ParentAccountId from COA";
                string sql = @"Select AccountId,AccountBranchId,AccountCode ,
                                Case isnull(b.BranchName,'')
                                   When '' then AccountName
                                   Else  AccountName + ' (' + b.BranchName + ')'
                                End  AccountName,ParentAccountId from COA a
                                left join Branch b on a.AccountBranchId=b.BranchId";

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
                throw new Exception(string.Format("Error occured while getting COA Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from COA where AccountId=@AccountId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@AccountId", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting COA Records: {0}", exception.Message), exception);
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
            string WhereClause = "1=1";

            try
            {
                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT BankID,BankName,ContactPerson,Address,ContactMobile FROM Bank where " + WhereClause + "";

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
                throw new Exception(string.Format("Error occured while getting Bank Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetParentLevelCOA(int ParentAccountId, int CompanyId)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from COA where ParentAccountId=@ParentAccountId and BranchId=@BranchId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ParentAccountId", ParentAccountId);
                    sqlCommand.Parameters.AddWithValue("@CompanyId", CompanyId);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting COA Parrent Level Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetLocations(int BranchId)
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select LocationId,LocationName from Location where BranchId=@BranchId order by LocationName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Parameters.AddWithValue("@BranchId", BranchId);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Location Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }

        public DataTable GetBranch()
        {
            DataTable BranchDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"select a.BranchID,BranchName + ' (' + CompanyName + ')' as BranchName  from Branch a
                            inner join Company b on a.CompanyId=b.CompanyID order by BranchName";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(BranchDataTable);
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
            return BranchDataTable;
        }

        public DataTable GetLocationCOA(int AccountId, int LocationId)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Select COALocationId,AccountId,LocationId,OpeningBalance,Appeared 
                               from COALocation where AccountId=@AccountId and LocationId=@LocationId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@AccountId", AccountId);
                    sqlCommand.Parameters.AddWithValue("@LocationId", LocationId);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Location COA Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public int GetMaxRecords()
        {
            int MaxCode = 0;
            DataTable DataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT  isNull(Max(AccountId),0) as MaxCode FROM COA";
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
                throw new Exception(string.Format("Error occured while getting Max Records COA: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }

        public string GetBranchName(int BranchId)
        {
            DataTable BranchDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            string BranchName = "";
            try
            {
                string sql = "Select BranchName from Branch where BranchId=@BranchId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BranchId", BranchId);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(BranchDataTable);
                    if (BranchDataTable.Rows.Count > 0)
                        BranchName = BranchDataTable.Rows[0]["BranchName"].ToString();
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
            return BranchName;
        }

        public bool AccountNameAlredayExist(int AccountId, int AccountBranchId, string AccountName)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            bool isExist = false;
            try
            {
                string sql = @"Select AccountId,AccountCode from COA where AccountName=@AccountName and 
                             AccountId!=@AccountId and AccountBranchId=@AccountBranchId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@AccountId", AccountId);
                    sqlCommand.Parameters.AddWithValue("@AccountBranchId", AccountBranchId);
                    sqlCommand.Parameters.AddWithValue("@AccountName", AccountName);
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

        public string GetMaxNewAccountCode(int AccountBranchId, int ParentAccountId, int Start, int Length)
        {
            int MaxCode = 0;
            string SetFormatMaxCode = string.Empty;
            DataTable DataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT isNull(Max(substring(AccountCode,@Start,@Length)),0) as MaxCode
                               FROM COA WHERE ParentAccountId=@ParentAccountId and AccountBranchId=@AccountBranchId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@Start", Start);
                    sqlCommand.Parameters.AddWithValue("@Length", Length);
                    sqlCommand.Parameters.AddWithValue("@ParentAccountId", ParentAccountId);
                    sqlCommand.Parameters.AddWithValue("@AccountBranchId", AccountBranchId);

                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(DataTable);

                    MaxCode = Convert.ToInt32(DataTable.Rows[0]["MaxCode"]);
                    if (MaxCode.Equals(0))
                    {
                        SetFormatMaxCode = Length.Equals(4) ? "0001" : "01";
                    }
                    else if (MaxCode < 9)
                    {
                        SetFormatMaxCode = Length.Equals(4) ? "000" + (MaxCode + 1) : "0" + (MaxCode + 1).ToString();
                    }
                    else if (MaxCode < 99)
                    {
                        SetFormatMaxCode = Length.Equals(4) ? "00" + (MaxCode + 1) : (MaxCode + 1).ToString();
                    }
                    else if (MaxCode < 999)
                    {
                        SetFormatMaxCode = Length.Equals(4) ? "0" + (MaxCode + 1) : (MaxCode + 1).ToString();
                    }
                    else
                    {
                        SetFormatMaxCode = (MaxCode + 1).ToString();
                    }
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Max Account Code: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return SetFormatMaxCode;
        }

        public int InsertRecord(COADAL objCOADAL, out string COA)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            int MaxCode = 0;
            try
            {
                MaxCode = GetMaxRecords();
                int Start = 0;
                int Length = 0;
                if (objCOADAL.AccountCode.Length < 6)
                {
                    Start = objCOADAL.AccountCode.Length + 2;
                    Length = 2;
                }
                else
                {
                    Start = objCOADAL.AccountCode.Length + 2;
                    Length = 4;
                }

                string NewAccountCode = objCOADAL.AccountCode + "-" + GetMaxNewAccountCode(objCOADAL.AccountBranchId, objCOADAL.ParentAccountId, Start, Length);
                COA = NewAccountCode;

                string sql = @"INSERT INTO COA(AccountId,AccountBranchId,AccountCode,AccountName,ParentAccountId,OpeningBalance,
                                             BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)
                                             VALUES (@AccountId,@AccountBranchId,@AccountCode,@AccountName,@ParentAccountId,@OpeningBalance,
                                             @BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@AccountId", MaxCode);
                    sqlCommand.Parameters.AddWithValue("@AccountBranchId", AccountBranchId);
                    sqlCommand.Parameters.AddWithValue("@AccountCode", NewAccountCode);
                    sqlCommand.Parameters.AddWithValue("@AccountName", objCOADAL.AccountName);
                    sqlCommand.Parameters.AddWithValue("@ParentAccountId", objCOADAL.ParentAccountId);
                    sqlCommand.Parameters.AddWithValue("@OpeningBalance", objCOADAL.OpeningBalance);

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
                throw new Exception(string.Format("Error occured while inserting COA: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }

        public void InsertCOALocationDetailRecord(COADAL objCOADAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO COALocation(AccountId,LocationId,OpeningBalance,Appeared,
                                             BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)
                                             VALUES (@AccountId,@LocationId,@OpeningBalance,@Appeared,
                                             @BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@AccountId", objCOADAL.AccountId);
                    sqlCommand.Parameters.AddWithValue("@LocationId", objCOADAL.LocationId);
                    sqlCommand.Parameters.AddWithValue("@OpeningBalance", objCOADAL.OpeningBalance);
                    sqlCommand.Parameters.AddWithValue("@Appeared", objCOADAL.Appeared);

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
                throw new Exception(string.Format("Error occured while inserting COA Location Detail: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void UpdateRecord(COADAL objCOADAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update COA SET AccountName = @AccountName,
                              UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn WHERE AccountId = @AccountId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@AccountId", objCOADAL.AccountId);
                    sqlCommand.Parameters.AddWithValue("@AccountName", objCOADAL.AccountName);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating COA: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void UpdateCOALocationDetailRecord(COADAL objCOADAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Update COALocation SET OpeningBalance = @OpeningBalance,
                               Appeared = @Appeared,UpdatedBy=@UpdatedBy,
                               UpdatedOn=@UpdatedOn WHERE AccountId=@AccountId and LocationId=@LocationId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@AccountId", objCOADAL.AccountId);
                    sqlCommand.Parameters.AddWithValue("@LocationId", objCOADAL.LocationId);
                    sqlCommand.Parameters.AddWithValue("@OpeningBalance", objCOADAL.OpeningBalance);
                    sqlCommand.Parameters.AddWithValue("@Appeared", objCOADAL.Appeared);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating COA Location Detail: {0}", exception.Message), exception);
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
                string sql = @"Delete From COA WHERE AccountId = @AccountId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@AccountId", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting COA: {0}", exception.Message), exception);
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

