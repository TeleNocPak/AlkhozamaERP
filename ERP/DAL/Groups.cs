using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class Groups
    {
        public Groups()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int MasterActive { get; set; }

        public int GroupRightsID { get; set; }
        public int FunctionID { get; set; }
        public int AllowAdd { get; set; }
        public int AllowEdit { get; set; }
        public int AllowDelete { get; set; }
        public int AllowView { get; set; }
        public int Type { get; set; }

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int GroupId)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT  * FROM Groups Where ID=@GroupID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@GroupID", GroupId);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Role Master Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetDetailRecords(int RoleID, int isReport)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = string.Empty;
                if (!RoleID.Equals(0))
                {
                    sql = @"SELECT isNull(A.ID,0) AS GROUPID,A.GROUPNAME,isNull(B.ID,0) AS GROUPRIGHTSID,
                            isNull(B.FUNCTIONID,0) as FUNCTIONID,isNull(B.CANADD,0) as ALLOWADD,
                            isNull(B.CANUPDATE,0) as ALLOWEDIT,isNull(B.CANDELETE,0) as ALLOWDELETE,
                            isNull(B.CANACCESS,0) as ALLOWVIEW,isNull(B.TYPE,0),isNull(C.ID,0) AS LINKID,C.NAME,
                            C.ISREPORT FROM ROLEMASTER A 
                            inner JOIN ROLEDETAIL B ON A.ID=B.ROLEID
                            right  JOIN LINKS C ON B.FUNCTIONID=C.ID and A.ID=" + RoleID + " WHERE C.ISREPORT=@ISREPORT and c.SubId!=0 order by Name";
                }
                else
                {
                    sql = @"select Id as LinkId, 0 as RoleDetailID,Name,0 as ALLOWADD, 
                           0 as ALLOWEDIT,0 as ALLOWDELETE,0 as ALLOWVIEW 
                           from Links where ISREPORT=@ISREPORT and SubId!=0 order by Name";
                }

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ISREPORT", isReport);
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

        public DataTable GetAllMasterRecords(string Search)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            string WhereClause = "1=1";

            try
            {
                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = "SELECT  * FROM Groups where " + WhereClause + "";
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
                throw new Exception(string.Format("Error occured while getting Role Master Records: {0}", exception.Message), exception);
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
                string sql = "SELECT  isNull(Max(ID),0) as RoleID FROM Groups";
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
                throw new Exception(string.Format("Error occured while getting Role Max Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return RoleDataTable;
        }

        public DataTable GetALLForms(int FormType)
        {
            DataTable FormsDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "select * from Links where isReport=@isReport and SubId!=0 order by subID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@isReport", FormType);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(FormsDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting forms Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return FormsDataTable;
        }

        public string GetFormsGroup(int GroupID)
        {
            DataTable RolesDataTable = new DataTable();
            String GroupName = string.Empty;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "select Name from Links where ID=@ID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", GroupID);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(RolesDataTable);
                    if (RolesDataTable.Rows.Count > 0)
                        GroupName = RolesDataTable.Rows[0]["Name"].ToString();
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
            return GroupName;
        }

        public DataTable GetRoleDetailByID(int RoleId, int FunctionId)
        {
            DataTable FormsDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "select * from GroupsRights where GroupsID=@RoleId And FunctionId=@FunctionId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@RoleId", RoleId);
                    sqlCommand.Parameters.AddWithValue("@FunctionId", FunctionId);
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(FormsDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting forms Role Detail Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return FormsDataTable;
        }

        public void InsertRoleMasterRecord(int RoleID, string RoleName, int Active)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO Groups(ID,Name,Active,CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn)
                             VALUES (@ID,@RoleName ,@Active,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", RoleID);
                    sqlCommand.Parameters.AddWithValue("@RoleName", RoleName);
                    sqlCommand.Parameters.AddWithValue("@Active", Active);

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
                throw new Exception(string.Format("Error occured while inserting User Role Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void UpdateRoleMasterRecord(int RoleMasterID, string RoleName, int Active)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update Groups SET Name = @RoleName, Active = @Active, 
                              LastModifiedBy=@UpdatedBy,LastModifiedOn=@UpdatedOn WHERE ID = @ID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@RoleName", RoleName);
                    sqlCommand.Parameters.AddWithValue("@Active", Active);
                    sqlCommand.Parameters.AddWithValue("@ID", RoleMasterID);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating User Role Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void InsertRoleDetailRecord(Groups objUserRoleDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO RoleDetail(RoleID,FunctionID,AllowAdd,AllowEdit,AllowDelete,AllowView,Type)
                             VALUES (@RoleID,@FunctionID,@AllowAdd,@AllowEdit,@AllowDelete,@AllowView,@Type)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@RoleID", objUserRoleDAL.GroupID);
                    sqlCommand.Parameters.AddWithValue("@FunctionID", objUserRoleDAL.FunctionID);
                    sqlCommand.Parameters.AddWithValue("@AllowAdd", objUserRoleDAL.AllowAdd);
                    sqlCommand.Parameters.AddWithValue("@AllowEdit", objUserRoleDAL.AllowEdit);
                    sqlCommand.Parameters.AddWithValue("@AllowDelete", objUserRoleDAL.AllowDelete);
                    sqlCommand.Parameters.AddWithValue("@AllowView", objUserRoleDAL.AllowView);
                    sqlCommand.Parameters.AddWithValue("@Type", objUserRoleDAL.Type);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting User Role Detail: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void UpdateRoleDetailRecord(Groups objUserRoleDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Update RoleDetail SET RoleID = @RoleID, FunctionID = @FunctionID,AllowAdd=@AllowAdd,
                               AllowEdit=@AllowEdit,AllowDelete=@AllowDelete,AllowView=@AllowView,Type=@Type
                               Where ID=@ID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", objUserRoleDAL.GroupRightsID);
                    sqlCommand.Parameters.AddWithValue("@RoleID", objUserRoleDAL.GroupID);
                    sqlCommand.Parameters.AddWithValue("@FunctionID", objUserRoleDAL.FunctionID);
                    sqlCommand.Parameters.AddWithValue("@AllowAdd", objUserRoleDAL.AllowAdd);
                    sqlCommand.Parameters.AddWithValue("@AllowEdit", objUserRoleDAL.AllowEdit);
                    sqlCommand.Parameters.AddWithValue("@AllowDelete", objUserRoleDAL.AllowDelete);
                    sqlCommand.Parameters.AddWithValue("@AllowView", objUserRoleDAL.AllowView);
                    sqlCommand.Parameters.AddWithValue("@Type", objUserRoleDAL.Type);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating User Role Detail: {0}", exception.Message), exception);
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
                string sql = @"Delete From ROLEMaster WHERE ID = @ID";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.ExecuteNonQuery();
                }
                sql = @"Delete From ROLEDETAIL WHERE RoleId = @RoleId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@RoleId", ID);
                    sqlCommand.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting User Role: {0}", exception.Message), exception);
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
