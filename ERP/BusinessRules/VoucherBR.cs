using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class VoucherBR
    {
        #region Variables

        VoucherDAL objVoucherDAL = new VoucherDAL();

        #endregion

        #region Methods


        public DataTable GetMasterRecord(int OrderId)
        {
            return objVoucherDAL.GetMasterRecord(OrderId);
        }
        public DataTable GetDetailRecords(int OrderId)
        {
            return objVoucherDAL.GetDetailRecords(OrderId);
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            return objVoucherDAL.GetAllMasterRecords(Search);
        }
       
        public string GetAccountCode(int AccountId)
        {
            return objVoucherDAL.GetAccountCode(AccountId);
        }

        public DataTable OrderExistinConfirm(int OrderID)
        {
            return objVoucherDAL.OrderExistinConfirm(OrderID);
        }

        public bool GetAccountNameIdExist(int AccountId, string AccountName)
        {
            return objVoucherDAL.GetAccountNameIdExist(AccountId, AccountName);
        }
          
        public int InsertMasterRecord(ArrayList inputArrayList)
        {
            objVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[0].ToString());
            objVoucherDAL.BookType = inputArrayList[1].ToString();
            objVoucherDAL.NarrationMaster = inputArrayList[2].ToString();
            return objVoucherDAL.InsertMasterRecord(objVoucherDAL);
        }
        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objVoucherDAL.BookType = inputArrayList[2].ToString();
            objVoucherDAL.NarrationMaster = inputArrayList[3].ToString();
            objVoucherDAL.UpdateMasterRecord(objVoucherDAL);
        }

        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objVoucherDAL.AccountId = Convert.ToInt32(inputArrayList[1]);
            objVoucherDAL.DebitAmount = Convert.ToDecimal(inputArrayList[2]);
            objVoucherDAL.CreditAmount = Convert.ToDecimal(inputArrayList[3]);
            objVoucherDAL.NarrationDetail = inputArrayList[4].ToString();
            objVoucherDAL.InsertDetailRecord(objVoucherDAL);
        }
       
        public void DeleteMasterDetailRecord(int ID)
        {
            objVoucherDAL.DeleteMasterDetailRecord(ID);
        }

        public void DeleteDetailRecord(int ID)
        {
            objVoucherDAL.DeleteDetailRecord(ID);
        }

        #endregion
    }
}


