using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class LocationBR
    {
        #region Variables

        LocationDAL objLocationDAL = new LocationDAL();

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            return objLocationDAL.GetRecord(ID);
        }
        public DataTable GetAllRecords(string Search)
        {
            return objLocationDAL.GetAllRecords(Search);
        }

        public DataTable LoginIDExist(int UserID, string LoginID)
        {
            return objLocationDAL.LoginIDExist(UserID, LoginID);
        }

        public DataTable GetCompany()
        {
            return objLocationDAL.GetCompany();
        }

        public DataTable GetBranch(int CompanyId)
        {
            return objLocationDAL.GetBranch(CompanyId);
        }

        public void InsertRecord(string LocationName, int BranchId)
        {
            objLocationDAL.InsertRecord(LocationName, BranchId);
        }
        public void UpdateRecord(int LocationID, string LocationName, int BranchId)
        {
            objLocationDAL.UpdateRecord(LocationID, LocationName, BranchId);
        }
        public void DeleteRecord(int ID)
        {
            objLocationDAL.DeleteRecord(ID);
        }

        #endregion
    }
}
