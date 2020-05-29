using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class UserMasterDAL
    {
        public UserMasterDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        private int _ID;
        private string _LoginID;
        private string _Pwd;
        private string _FullName;
        private string _Phone;
        private string _Mobile;
        private string _Email;
        private string _Address;
        private int _Admin;
        private int _RoleID;
        private int _Active;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }

        public string Pwd
        {
            get { return _Pwd; }
            set { _Pwd = value; }
        }

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public int Admin
        {
            get { return _Admin; }
            set { _Admin = value; }
        }

        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        public int Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public string Qualification { get; set; }
        public string References { get; set; }
        public string EmergencyNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Others { get; set; }
        public int BranchId { get; set; }
        public string LocationId { get; set; }

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from UserMaster where UserId=@UserId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserId", ID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting User Master Records: {0}", exception.Message), exception);
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

                string sql = @"SELECT A.USERID,LOGINID,FULLNAME,PHONE,ADDRESS,B.ROLENAME 
                             FROM USERMASTER A INNER JOIN ROLEMASTER B ON A.ROLEID=B.ID where " + WhereClause + " order by A.USERID desc";

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
                throw new Exception(string.Format("Error occured while getting User Master Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable LoginIDExist(int UserID, string LoginID)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from UserMaster where LoginID=@LoginID and UserId!=@UserId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LoginID", LoginID);
                    sqlCommand.Parameters.AddWithValue("@UserId", UserID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Login Id Exist: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetRoles()
        {
            DataTable RolesDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select RoleName,ID from RoleMaster order by RoleName";
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
                throw new Exception(string.Format("Error occured while getting Roles Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return RolesDataTable;
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

        public DataTable GetLocation(int BranchId)
        {
            DataTable BranchDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select LocationId,LocationName from Location where BranchId=@BranchId order by LocationName";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Parameters.AddWithValue("@BranchId", BranchId);
                    sqlAdapter.Fill(BranchDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Location Records in Users: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return BranchDataTable;
        }

        public void InsertRecord(UserMasterDAL objUserMasterDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO usermaster(LoginID ,Password, Phone, Mobile,Email,Address,
                             Admin,RoleID,Active,FullName,Qualification,Reference,EmergencyNo,
                             AppointmentDate,Others,BranchId,LocationId,
                             AddedBy,AddedOn,UpdatedBy,UpdatedOn)
                             VALUES (@LoginID ,@Password,@Phone,@Mobile,
                             @Email,@Address,@Admin,@RoleID,@Active,@FullName,
                             @Qualification,@Reference,@EmergencyNo,@AppointmentDate,@Others,@BranchId,@LocationId,
                             @AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LoginID", objUserMasterDAL.LoginID);
                    sqlCommand.Parameters.AddWithValue("@Password", objUserMasterDAL.Pwd);
                    sqlCommand.Parameters.AddWithValue("@Phone", objUserMasterDAL.Phone);
                    sqlCommand.Parameters.AddWithValue("@Mobile", objUserMasterDAL.Mobile);
                    sqlCommand.Parameters.AddWithValue("@Email", objUserMasterDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@Address", objUserMasterDAL.Address);
                    sqlCommand.Parameters.AddWithValue("@Admin", objUserMasterDAL.Admin);
                    sqlCommand.Parameters.AddWithValue("@RoleID", objUserMasterDAL.RoleID);
                    sqlCommand.Parameters.AddWithValue("@Active", objUserMasterDAL.Active);
                    sqlCommand.Parameters.AddWithValue("@FullName", objUserMasterDAL.FullName);

                    sqlCommand.Parameters.AddWithValue("@Qualification", objUserMasterDAL.Qualification);
                    sqlCommand.Parameters.AddWithValue("@Reference", objUserMasterDAL.References);
                    sqlCommand.Parameters.AddWithValue("@EmergencyNo", objUserMasterDAL.EmergencyNo);
                    sqlCommand.Parameters.AddWithValue("@AppointmentDate", objUserMasterDAL.AppointmentDate);
                    sqlCommand.Parameters.AddWithValue("@Others", objUserMasterDAL.Others);
                    sqlCommand.Parameters.AddWithValue("@BranchId", objUserMasterDAL.BranchId);
                    sqlCommand.Parameters.AddWithValue("@LocationId", objUserMasterDAL.LocationId);

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
                throw new Exception(string.Format("Error occured while inserting User Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void UpdateRecord(UserMasterDAL objUserMasterDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update UserMaster SET LoginID = @LoginID,
                              FullName = @FullName, Phone = @Phone, Mobile = @Mobile, Email = @Email,
                              Address = @Address, Admin = @Admin, RoleID = @RoleID, Active = @Active, 
                              Qualification = @Qualification, Reference=@Reference, EmergencyNo = @EmergencyNo,
                              AppointmentDate = @AppointmentDate, Others = @Others, BranchId = @BranchId,LocationId=@LocationId,                             
                              UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn WHERE UserID = @UserID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LoginID", objUserMasterDAL.LoginID);
                    sqlCommand.Parameters.AddWithValue("@FullName", objUserMasterDAL.FullName);
                    sqlCommand.Parameters.AddWithValue("@Phone", objUserMasterDAL.Phone);
                    sqlCommand.Parameters.AddWithValue("@Mobile", objUserMasterDAL.Mobile);
                    sqlCommand.Parameters.AddWithValue("@Email", objUserMasterDAL.Email);
                    sqlCommand.Parameters.AddWithValue("@Address", objUserMasterDAL.Address);
                    sqlCommand.Parameters.AddWithValue("@Admin", objUserMasterDAL.Admin);
                    sqlCommand.Parameters.AddWithValue("@RoleID", objUserMasterDAL.RoleID);
                    sqlCommand.Parameters.AddWithValue("@Active", objUserMasterDAL.Active);
                    sqlCommand.Parameters.AddWithValue("@UserID", objUserMasterDAL.ID);

                    sqlCommand.Parameters.AddWithValue("@Qualification", objUserMasterDAL.Qualification);
                    sqlCommand.Parameters.AddWithValue("@Reference", objUserMasterDAL.References);
                    sqlCommand.Parameters.AddWithValue("@EmergencyNo", objUserMasterDAL.EmergencyNo);
                    sqlCommand.Parameters.AddWithValue("@AppointmentDate", objUserMasterDAL.AppointmentDate);
                    sqlCommand.Parameters.AddWithValue("@Others", objUserMasterDAL.Others);
                    sqlCommand.Parameters.AddWithValue("@BranchId", objUserMasterDAL.BranchId);
                    sqlCommand.Parameters.AddWithValue("@LocationId", objUserMasterDAL.LocationId);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating User Master: {0}", exception.Message), exception);
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
                string sql = @"Delete From UserMaster WHERE USerID = @USerID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@USerID", ID);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting User Master: {0}", exception.Message), exception);
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
