using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class CompanyDAL
    {
        public CompanyDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        private int _ID;
        private string _CompanyName;
        private string _ContactPerson;
        private string _FullName;
        private string _Address;
        private string _POBox;
        private string _PostalCode;
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

        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string POBox
        {
            get { return _POBox; }
            set { _POBox = value; }
        }

        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
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

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from Company where CompanyID=@CompanyID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CompanyID", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Company Records: {0}", exception.Message), exception);
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

                string sql = @"SELECT CompanyID,CompanyName,ContactPerson,ContactMobile FROM Company where " + WhereClause + " order by CompanyID desc";

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
                throw new Exception(string.Format("Error occured while getting Company Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public void InsertRecord(CompanyDAL objCompanyDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO Company(CompanyName,ContactPerson,Address,
                             POBox,PostalCode,ContactPhone,ContactMobile,ContactFaxNo,Email,WebSite,
                             AddedBy,AddedOn,UpdatedBy,UpdatedOn) 
                             VALUES (@CompanyName,@ContactPerson,@Address,
                             @POBox,@PostalCode,@ContactPhone,@ContactMobile,@ContactFaxNo,@Email,
                             @WebSite,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CompanyName", objCompanyDAL.CompanyName);
                    sqlCommand.Parameters.AddWithValue("@ContactPerson", objCompanyDAL.ContactPerson);
                    sqlCommand.Parameters.AddWithValue("@Address", objCompanyDAL.Address);                    
                    sqlCommand.Parameters.AddWithValue("@POBox", objCompanyDAL.POBox);
                    sqlCommand.Parameters.AddWithValue("@PostalCode", objCompanyDAL.PostalCode);
                    sqlCommand.Parameters.AddWithValue("@ContactPhone", objCompanyDAL.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@ContactMobile", objCompanyDAL.ContactMobile);
                    sqlCommand.Parameters.AddWithValue("@ContactFaxNo", objCompanyDAL.ContactFaxNo);
                    sqlCommand.Parameters.AddWithValue("@Email", objCompanyDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@WebSite", objCompanyDAL.WebSite);

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
                throw new Exception(string.Format("Error occured while inserting Company: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void UpdateRecord(CompanyDAL objCompanyDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update Company SET CompanyName = @CompanyName, ContactPerson = @ContactPerson,
                              Address = @Address,POBox = @POBox, PostalCode = @PostalCode, ContactPhone = @ContactPhone,
                              ContactMobile = @ContactMobile,
                              ContactFaxNo = @ContactFaxNo, Email = @Email, WebSite = @WebSite,
                              UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn WHERE CompanyID = @CompanyID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CompanyName", objCompanyDAL.CompanyName);
                    sqlCommand.Parameters.AddWithValue("@ContactPerson", objCompanyDAL.ContactPerson);
                    sqlCommand.Parameters.AddWithValue("@Address", objCompanyDAL.Address);
                    sqlCommand.Parameters.AddWithValue("@POBox", objCompanyDAL.POBox);
                    sqlCommand.Parameters.AddWithValue("@PostalCode", objCompanyDAL.PostalCode);
                    sqlCommand.Parameters.AddWithValue("@ContactPhone", objCompanyDAL.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@ContactMobile", objCompanyDAL.ContactMobile);
                    sqlCommand.Parameters.AddWithValue("@ContactFaxNo", objCompanyDAL.ContactFaxNo);
                    sqlCommand.Parameters.AddWithValue("@Email", objCompanyDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@WebSite", objCompanyDAL.WebSite);
                    sqlCommand.Parameters.AddWithValue("@CompanyID", objCompanyDAL.ID);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Company: {0}", exception.Message), exception);
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
                string sql = @"Delete From Company WHERE CompanyID = @CompanyID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CompanyID", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Company: {0}", exception.Message), exception);
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
