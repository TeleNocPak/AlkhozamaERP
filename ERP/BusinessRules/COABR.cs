using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class COABR
    {
        #region Variables

        COADAL objCOADAL = new COADAL();

        #endregion

        #region Methods

        public DataTable GetCOARecord()
        {
            return objCOADAL.GetCOARecord();
        }

        public DataTable GetRecord(int ID)
        {
            return objCOADAL.GetRecord(ID);
        }

        public DataTable GetAllRecords(string Search)
        {
            return objCOADAL.GetAllRecords(Search);
        }

        public DataTable GetParentLevelCOA(int ParentAccountId, int CompanyId)
        {
            return objCOADAL.GetParentLevelCOA(ParentAccountId, CompanyId);
        }

        public DataTable GetLocations(int BranchId)
        {
            return objCOADAL.GetLocations(BranchId);
        }


        public DataTable GetBranch()
        {
            return objCOADAL.GetBranch();
        }

        public DataTable GetLocationCOA(int AccountId, int LocationId)
        {
            return objCOADAL.GetLocationCOA(AccountId, LocationId);
        }

         public string GetBranchName(int BranchId)
        {
            return objCOADAL.GetBranchName(BranchId);
        }

         public bool AccountNameAlredayExist(int AccountId, int AccountBranchId, string AccountName)
         {
             return objCOADAL.AccountNameAlredayExist(AccountId, AccountBranchId, AccountName);
         }

        public int InsertRecord(ArrayList inputArrayList, out string COA)
        {
            objCOADAL.AccountCode = inputArrayList[0].ToString();
            objCOADAL.AccountName = inputArrayList[1].ToString();
            objCOADAL.ParentAccountId = Convert.ToInt32(inputArrayList[2]);
            objCOADAL.OpeningBalance = Convert.ToInt64(inputArrayList[3]);
            objCOADAL.AccountBranchId = Convert.ToInt32(inputArrayList[4]);
            return objCOADAL.InsertRecord(objCOADAL, out COA);
        }

        public void InsertCOALocationDetailRecord(ArrayList inputArrayList)
        {
            objCOADAL.AccountId = Convert.ToInt32(inputArrayList[0]);
            objCOADAL.LocationId = Convert.ToInt32(inputArrayList[1]);
            objCOADAL.OpeningBalance = Convert.ToInt64(inputArrayList[2]);
            objCOADAL.Appeared = Convert.ToInt16(inputArrayList[3]);
            objCOADAL.InsertCOALocationDetailRecord(objCOADAL);
        }

        public void UpdateRecord(ArrayList inputArrayList)
        {
            objCOADAL.AccountId = Convert.ToInt32(inputArrayList[0].ToString());
            objCOADAL.AccountName = inputArrayList[1].ToString();
            objCOADAL.UpdateRecord(objCOADAL);
        }

        public void UpdateCOALocationDetailRecord(ArrayList inputArrayList)
        {
            objCOADAL.AccountId = Convert.ToInt32(inputArrayList[0]);
            objCOADAL.LocationId = Convert.ToInt32(inputArrayList[1]);
            objCOADAL.OpeningBalance = Convert.ToInt64(inputArrayList[2]);
            objCOADAL.Appeared = Convert.ToInt16(inputArrayList[3]);
            objCOADAL.UpdateCOALocationDetailRecord(objCOADAL);
        }

        public void DeleteRecord(int ID)
        {
            objCOADAL.DeleteRecord(ID);
        }

        #endregion
    }
}
