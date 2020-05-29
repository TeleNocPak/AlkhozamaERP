using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class BankDAL
    {
        public BankDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        private int _ID;
        private string _BankName;
        private string _ContactPerson;
        private string _Address;
        private string _ContactPhone;
        private string _ContactMobile;
        private string _ContactFaxNo;
        private string _Email;
        private string _WebSite;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; }
        }

        public string ContactMobile
        {
            get { return _ContactMobile; }
            set { _ContactMobile = value; }
        }

        public string ContactFaxNo
        {
            get { return _ContactFaxNo; }
            set { _ContactFaxNo = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string WebSite
        {
            get { return _WebSite; }
            set { _WebSite = value; }
        }

        public decimal LCLimit { get; set; }
        public decimal SGLimit { get; set; }
        public decimal ImportLoan { get; set; }
        public decimal SGLimitPer { get; set; }

        public int InnerLimitSG { get; set; }
        public int InnerLimitLoan { get; set; }

        public int AccountId { get; set; }
        public string BankAbbreviation { get; set; }
        public string SWIFTCode { get; set; }
        
        public string BankType { get; set; }

        public string AccountTitle { get; set; }
        public string AccountCode { get; set; }
        
        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Select a.*, b.AccountCode, b.AccountName from Bank a
                             LEFT JOIN COA b on a.AccountId = b.AccountId
                             WHERE BankId=@BankId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BankId", ID);
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

        public DataTable GetContactDetailRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from BankDetail where BankId=@BankId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BankId", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Contact Bank Detail Records: {0}", exception.Message), exception);
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

                string sql = @"SELECT BankID,BankName,ContactPerson,Address,ContactMobile,BankType FROM Bank where " + WhereClause + " order by BankId desc";

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
                throw new Exception(string.Format("Error occured while getting Bank Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetMaxRecords()
        {
            DataTable RoleDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT  isNull(Max(BankID),0) as BankID FROM Bank";
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
                throw new Exception(string.Format("Error occured while getting Bank Max Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return RoleDataTable;
        }

        public void InsertRecord(BankDAL objBankDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO Bank(BankName,BankType,ContactPerson,Address,ContactPhone,
                             ContactMobile,ContactFaxNo,Email,WebSite,LCLimit,SGLimit,ImportLoan,SGLimitPer,InnerLimitSG,
                             InnerLimitLoan,AccountId,BankAbbreviation,SWIFTCode,AccountTitle,AccountCode,BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn) VALUES (@BankName,@BankType,@ContactPerson,
                             @Address,@ContactPhone,@ContactMobile,@ContactFaxNo,@Email,@WebSite,
                             @LCLimit,@SGLimit,@ImportLoan,@SGLimitPer,@InnerLimitSG,@InnerLimitLoan,@AccountId,@BankAbbreviation,@SWIFTCode,@AccountTitle,@AccountCode,
                             @BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BankName", objBankDAL.BankName);
                    sqlCommand.Parameters.AddWithValue("@BankType", objBankDAL.BankType);

                    sqlCommand.Parameters.AddWithValue("@ContactPerson", objBankDAL.ContactPerson);
                    sqlCommand.Parameters.AddWithValue("@Address", objBankDAL.Address);
                    sqlCommand.Parameters.AddWithValue("@ContactPhone", objBankDAL.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@ContactMobile", objBankDAL.ContactMobile);
                    sqlCommand.Parameters.AddWithValue("@ContactFaxNo", objBankDAL.ContactFaxNo);
                    sqlCommand.Parameters.AddWithValue("@Email", objBankDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@WebSite", objBankDAL.WebSite);

                    sqlCommand.Parameters.AddWithValue("@LCLimit", objBankDAL.LCLimit);
                    sqlCommand.Parameters.AddWithValue("@SGLimit", objBankDAL.SGLimit);
                    sqlCommand.Parameters.AddWithValue("@ImportLoan", objBankDAL.ImportLoan);

                    sqlCommand.Parameters.AddWithValue("@SGLimitPer", objBankDAL.SGLimitPer);

                    sqlCommand.Parameters.AddWithValue("@InnerLimitSG", objBankDAL.InnerLimitSG);
                    sqlCommand.Parameters.AddWithValue("@InnerLimitLoan", objBankDAL.InnerLimitLoan);

                    sqlCommand.Parameters.AddWithValue("@AccountId", objBankDAL.AccountId);
                    sqlCommand.Parameters.AddWithValue("@BankAbbreviation", objBankDAL.BankAbbreviation);
                    sqlCommand.Parameters.AddWithValue("@SWIFTCode", objBankDAL.SWIFTCode);

                    sqlCommand.Parameters.AddWithValue("@AccountTitle", objBankDAL.AccountTitle);
                    sqlCommand.Parameters.AddWithValue("@AccountCode", objBankDAL.AccountCode);

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
                throw new Exception(string.Format("Error occured while inserting Bank: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void UpdateRecord(BankDAL objBankDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update Bank SET BankName = @BankName,BankType = @BankType, ContactPerson = @ContactPerson,
                              Address = @Address, ContactPhone = @ContactPhone, ContactMobile = @ContactMobile,
                              ContactFaxNo = @ContactFaxNo, Email = @Email, WebSite = @WebSite,
                              LCLimit = @LCLimit, SGLimit = @SGLimit, ImportLoan = @ImportLoan,SGLimitPer=@SGLimitPer,
                              InnerLimitSG = @InnerLimitSG, InnerLimitLoan = @InnerLimitLoan,
                              AccountId=@AccountId,BankAbbreviation=@BankAbbreviation,SWIFTCode=@SWIFTCode,
                              AccountTitle=@AccountTitle,AccountCode=@AccountCode,
                              UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn WHERE BankID = @BankID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BankName", objBankDAL.BankName);
                    sqlCommand.Parameters.AddWithValue("@BankType", objBankDAL.BankType);
                    sqlCommand.Parameters.AddWithValue("@ContactPerson", objBankDAL.ContactPerson);
                    sqlCommand.Parameters.AddWithValue("@Address", objBankDAL.Address);
                    sqlCommand.Parameters.AddWithValue("@ContactPhone", objBankDAL.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@ContactMobile", objBankDAL.ContactMobile);
                    sqlCommand.Parameters.AddWithValue("@ContactFaxNo", objBankDAL.ContactFaxNo);
                    sqlCommand.Parameters.AddWithValue("@Email", objBankDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@WebSite", objBankDAL.WebSite);
                    sqlCommand.Parameters.AddWithValue("@BankID", objBankDAL.ID);
                    
                    sqlCommand.Parameters.AddWithValue("@LCLimit", objBankDAL.LCLimit);
                    sqlCommand.Parameters.AddWithValue("@SGLimit", objBankDAL.SGLimit);
                    sqlCommand.Parameters.AddWithValue("@ImportLoan", objBankDAL.ImportLoan);

                    sqlCommand.Parameters.AddWithValue("@SGLimitPer", objBankDAL.SGLimitPer);

                    sqlCommand.Parameters.AddWithValue("@InnerLimitSG", objBankDAL.InnerLimitSG);
                    sqlCommand.Parameters.AddWithValue("@InnerLimitLoan", objBankDAL.InnerLimitLoan);

                    sqlCommand.Parameters.AddWithValue("@AccountId", objBankDAL.AccountId);
                    sqlCommand.Parameters.AddWithValue("@BankAbbreviation", objBankDAL.BankAbbreviation);
                    sqlCommand.Parameters.AddWithValue("@SWIFTCode", objBankDAL.SWIFTCode);

                    sqlCommand.Parameters.AddWithValue("@AccountTitle", objBankDAL.AccountTitle);
                    sqlCommand.Parameters.AddWithValue("@AccountCode", objBankDAL.AccountCode);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Bank: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void InsertContactRecord(int BankId,string ContactPerson,string ContactPhone,string ContactMobile,string Email)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO BankDetail(BankID,ContactPerson,ContactPhone,ContactMobile,Email) 
                               VALUES (@BankID,@ContactPerson,@ContactPhone,@ContactMobile,@Email)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BankID", BankId);
                    sqlCommand.Parameters.AddWithValue("@ContactPerson", ContactPerson);
                    sqlCommand.Parameters.AddWithValue("@ContactPhone", ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@ContactMobile", ContactMobile);
                    sqlCommand.Parameters.AddWithValue("@Email", Email);
                   
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Bank Detail: {0}", exception.Message), exception);
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
                string sql = @"Delete From Bank WHERE BankID = @BankID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BankID", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Bank: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
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

        public DataTable GetAccountCode(int AccountId)
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Select AccountCode from COA where AccountId=@AccountId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@AccountId", AccountId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Product Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }

        public void DeleteContactDetailRecord(int ID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Delete From BankDetail WHERE BankDetailId = @BankDetailId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BankDetailId", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Detail Contact Bank: {0}", exception.Message), exception);
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
