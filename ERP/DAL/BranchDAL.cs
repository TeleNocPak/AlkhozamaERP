using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class BranchDAL
    {
        public BranchDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        private int _ID;
        private string _BranchName;
        private string _ContactPerson;
        private string _FullName;
        private string _Address;
        private string _Country;
        private string _State;
        private string _City;
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

        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
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

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
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

        public int CompanyId { get; set; }

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from Branch where BranchID=@BranchID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BranchID", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
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

                string sql = @"SELECT a.BranchID,BranchName,a.ContactPerson,a.Country,
                               a.City,a.ContactMobile,b.CompanyName FROM Branch a
                               inner join Company b on a.CompanyId=b.CompanyId
                               where " + WhereClause + " order by a.BranchId desc";

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
                throw new Exception(string.Format("Error occured while getting Branch Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetCompany()
        {
            DataTable RolesDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select CompanyId,CompanyName from Company order by CompanyName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(RolesDataTable);
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
            return RolesDataTable;
        }

        public void InsertRecord(BranchDAL objBranchDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO Branch(BranchName,ContactPerson,Address,Country,State,City,
                             POBox,PostalCode,ContactPhone,ContactMobile,ContactFaxNo,Email,WebSite,CompanyId,
                             AddedBy,AddedOn,UpdatedBy,UpdatedOn) 
                             VALUES (@BranchName,@ContactPerson,@Address,@Country,@State,@City,
                             @POBox,@PostalCode,@ContactPhone,@ContactMobile,@ContactFaxNo,@Email,
                             @WebSite,@CompanyId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BranchName", objBranchDAL.BranchName);
                    sqlCommand.Parameters.AddWithValue("@ContactPerson", objBranchDAL.ContactPerson);
                    sqlCommand.Parameters.AddWithValue("@Address", objBranchDAL.Address);
                    sqlCommand.Parameters.AddWithValue("@Country", objBranchDAL.Country);
                    sqlCommand.Parameters.AddWithValue("@State", objBranchDAL.State);
                    sqlCommand.Parameters.AddWithValue("@City", objBranchDAL.City);
                    sqlCommand.Parameters.AddWithValue("@POBox", objBranchDAL.POBox);
                    sqlCommand.Parameters.AddWithValue("@PostalCode", objBranchDAL.PostalCode);
                    sqlCommand.Parameters.AddWithValue("@ContactPhone", objBranchDAL.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@ContactMobile", objBranchDAL.ContactMobile);
                    sqlCommand.Parameters.AddWithValue("@ContactFaxNo", objBranchDAL.ContactFaxNo);
                    sqlCommand.Parameters.AddWithValue("@Email", objBranchDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@WebSite", objBranchDAL.WebSite);
                    sqlCommand.Parameters.AddWithValue("@CompanyId", objBranchDAL.CompanyId);

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
                throw new Exception(string.Format("Error occured while inserting Branch: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void UpdateRecord(BranchDAL objBranchDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update Branch SET BranchName = @BranchName, ContactPerson = @ContactPerson,
                              Address = @Address, Country = @Country, State = @State, City = @City,
                              POBox = @POBox, PostalCode = @PostalCode, ContactPhone = @ContactPhone,
                              ContactMobile = @ContactMobile,
                              ContactFaxNo = @ContactFaxNo, Email = @Email, WebSite = @WebSite, CompanyId = @CompanyId,
                              UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn WHERE BranchID = @BranchID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BranchName", objBranchDAL.BranchName);
                    sqlCommand.Parameters.AddWithValue("@ContactPerson", objBranchDAL.ContactPerson);
                    sqlCommand.Parameters.AddWithValue("@Address", objBranchDAL.Address);
                    sqlCommand.Parameters.AddWithValue("@Country", objBranchDAL.Country);
                    sqlCommand.Parameters.AddWithValue("@State", objBranchDAL.State);
                    sqlCommand.Parameters.AddWithValue("@City", objBranchDAL.City);
                    sqlCommand.Parameters.AddWithValue("@POBox", objBranchDAL.POBox);
                    sqlCommand.Parameters.AddWithValue("@PostalCode", objBranchDAL.PostalCode);
                    sqlCommand.Parameters.AddWithValue("@ContactPhone", objBranchDAL.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@ContactMobile", objBranchDAL.ContactMobile);
                    sqlCommand.Parameters.AddWithValue("@ContactFaxNo", objBranchDAL.ContactFaxNo);
                    sqlCommand.Parameters.AddWithValue("@Email", objBranchDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@WebSite", objBranchDAL.WebSite);
                    sqlCommand.Parameters.AddWithValue("@BranchID", objBranchDAL.ID);
                    sqlCommand.Parameters.AddWithValue("@CompanyId", objBranchDAL.CompanyId);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Branch: {0}", exception.Message), exception);
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
                string sql = @"Delete From Branch WHERE BranchID = @BranchID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@BranchID", ID);
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

        #endregion
    }
}
