using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class UserRoleBR
    {
        #region Variables

        Groups objUserRoleDAL = new Groups();

        #endregion

        #region Methods


        public DataTable GetMasterRecord(int ID)
        {
            return objUserRoleDAL.GetMasterRecord(ID);
        }

        public DataTable GetDetailRecords(int ID, int isReport)
        {
            return objUserRoleDAL.GetDetailRecords(ID, isReport);
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            return objUserRoleDAL.GetAllMasterRecords(Search);
        }

        public DataTable GetMaxRecords()
        {
            return objUserRoleDAL.GetMaxRecords();
        }

        public DataTable GetALLForms(int FormType)
        {
            return objUserRoleDAL.GetALLForms(FormType);
        }

        public string GetFormsGroup(int GroupID)
        {
            return objUserRoleDAL.GetFormsGroup(GroupID);
        }

        public DataTable GetRoleDetailByID(int RoleId,int FunctionId)
        {
            return objUserRoleDAL.GetRoleDetailByID(RoleId, FunctionId);
        }

        public void InsertRoleMasterRecord(int RoleID, string RoleName, int Active)
        {
            objUserRoleDAL.InsertRoleMasterRecord(RoleID, RoleName, Active);
        }
        public void UpdateRoleMasterRecord(int RoleMasterID, string RoleName, int Active)
        {
            objUserRoleDAL.UpdateRoleMasterRecord(RoleMasterID, RoleName, Active);
        }

        public void InsertRoleDetailRecord(ArrayList inputArrayList)
        {
            objUserRoleDAL.GroupID = Convert.ToInt32(inputArrayList[0]);
            objUserRoleDAL.FunctionID = Convert.ToInt32(inputArrayList[1].ToString());
            objUserRoleDAL.AllowAdd = Convert.ToInt16(inputArrayList[2]);
            objUserRoleDAL.AllowEdit = Convert.ToInt16(inputArrayList[3]);
            objUserRoleDAL.AllowDelete = Convert.ToInt16(inputArrayList[4]);
            objUserRoleDAL.AllowView = Convert.ToInt16(inputArrayList[5]);
            objUserRoleDAL.Type = Convert.ToInt16(inputArrayList[6]);
            objUserRoleDAL.InsertRoleDetailRecord(objUserRoleDAL);
        }

        public void UpdateRoleDetailRecord(ArrayList inputArrayList)
        {
            objUserRoleDAL.GroupRightsID = Convert.ToInt32(inputArrayList[0]);
            objUserRoleDAL.GroupID = Convert.ToInt32(inputArrayList[1]);
            objUserRoleDAL.FunctionID = Convert.ToInt32(inputArrayList[2].ToString());
            objUserRoleDAL.AllowAdd = Convert.ToInt16(inputArrayList[3]);
            objUserRoleDAL.AllowEdit = Convert.ToInt16(inputArrayList[4]);
            objUserRoleDAL.AllowDelete = Convert.ToInt16(inputArrayList[5]);
            objUserRoleDAL.AllowView = Convert.ToInt16(inputArrayList[6]);
            objUserRoleDAL.Type = Convert.ToInt16(inputArrayList[7]);
            objUserRoleDAL.UpdateRoleDetailRecord(objUserRoleDAL);
        }

        public void DeleteRecord(int ID)
        {
            objUserRoleDAL.DeleteRecord(ID);
        }

        #endregion
    }
}

