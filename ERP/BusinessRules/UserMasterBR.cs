using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class UserMasterBR
    {
        #region Variables

        UserMasterDAL objUserMasterDAL = new UserMasterDAL();

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            return objUserMasterDAL.GetRecord(ID);
        }
        public DataTable GetAllRecords(string Search)
        {
            return objUserMasterDAL.GetAllRecords(Search);
        }

        public DataTable LoginIDExist(int UserID, string LoginID)
        {
            return objUserMasterDAL.LoginIDExist(UserID, LoginID);
        }

        public DataTable GetRoles()
        {
            return objUserMasterDAL.GetRoles();
        }

        public DataTable GetBranch()
        {
            return objUserMasterDAL.GetBranch();
        }

        public DataTable GetLocation(int BranchId)
        {
            return objUserMasterDAL.GetLocation(BranchId);
        }

        public void InsertRecord(ArrayList inputArrayList)
        {
            objUserMasterDAL.LoginID = inputArrayList[0].ToString();
            objUserMasterDAL.Pwd = inputArrayList[1].ToString();
            objUserMasterDAL.FullName = inputArrayList[2].ToString();
            objUserMasterDAL.Phone = inputArrayList[3].ToString();
            objUserMasterDAL.Mobile = inputArrayList[4].ToString();
            objUserMasterDAL.Email = inputArrayList[5].ToString();
            objUserMasterDAL.Address = inputArrayList[6].ToString();
            objUserMasterDAL.Admin = Convert.ToInt16(inputArrayList[7]);
            objUserMasterDAL.RoleID = Convert.ToInt32(inputArrayList[8].ToString());
            objUserMasterDAL.Active = Convert.ToInt16(inputArrayList[9]);

            objUserMasterDAL.Qualification = inputArrayList[10].ToString();
            objUserMasterDAL.References = inputArrayList[11].ToString();
            objUserMasterDAL.EmergencyNo = inputArrayList[12].ToString();
            objUserMasterDAL.AppointmentDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[13].ToString());
            objUserMasterDAL.Others = inputArrayList[14].ToString();
            objUserMasterDAL.BranchId = Convert.ToInt32(inputArrayList[15].ToString());
            objUserMasterDAL.LocationId = inputArrayList[16].ToString();
            objUserMasterDAL.InsertRecord(objUserMasterDAL);
        }
        public void UpdateRecord(ArrayList inputArrayList)
        {
            objUserMasterDAL.ID = Convert.ToInt32(inputArrayList[0].ToString());
            objUserMasterDAL.LoginID = inputArrayList[1].ToString();
            objUserMasterDAL.FullName = inputArrayList[2].ToString();
            objUserMasterDAL.Phone = inputArrayList[3].ToString();
            objUserMasterDAL.Mobile = inputArrayList[4].ToString();
            objUserMasterDAL.Email = inputArrayList[5].ToString();
            objUserMasterDAL.Address = inputArrayList[6].ToString();
            objUserMasterDAL.Admin = Convert.ToInt16(inputArrayList[7]);
            objUserMasterDAL.RoleID = Convert.ToInt32(inputArrayList[8].ToString());
            objUserMasterDAL.Active = Convert.ToInt16(inputArrayList[9]);

            objUserMasterDAL.Qualification = inputArrayList[10].ToString();
            objUserMasterDAL.References = inputArrayList[11].ToString();
            objUserMasterDAL.EmergencyNo = inputArrayList[12].ToString();
            objUserMasterDAL.AppointmentDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[13].ToString());
            objUserMasterDAL.Others = inputArrayList[14].ToString();

            objUserMasterDAL.BranchId = Convert.ToInt32(inputArrayList[15].ToString());
            objUserMasterDAL.LocationId = inputArrayList[16].ToString();

            objUserMasterDAL.UpdateRecord(objUserMasterDAL);
        }
        public void DeleteRecord(int ID)
        {
            objUserMasterDAL.DeleteRecord(ID);
        }

        #endregion
    }
}
