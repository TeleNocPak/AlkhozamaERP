using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class ReceiptVoucherBR
    {
        #region Variables

        ReceiptVoucherDAL objReceiptVoucherDAL = new ReceiptVoucherDAL();

        #endregion

        #region Methods


        public DataTable GetMasterRecord(int OrderId)
        {
            return objReceiptVoucherDAL.GetMasterRecord(OrderId);
        }
        public DataTable GetDetailRecords(int OrderId)
        {
            return objReceiptVoucherDAL.GetDetailRecords(OrderId);
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            return objReceiptVoucherDAL.GetAllMasterRecords(Search);
        }
       
        public string GetAccountCode(int AccountId)
        {
            return objReceiptVoucherDAL.GetAccountCode(AccountId);
        }

        public DataTable OrderExistinConfirm(int OrderID)
        {
            return objReceiptVoucherDAL.OrderExistinConfirm(OrderID);
        }

        public bool GetAccountNameIdExist(int AccountId, string AccountName)
        {
            return objReceiptVoucherDAL.GetAccountNameIdExist(AccountId, AccountName);
        }
          
        public int InsertMasterRecord(ArrayList inputArrayList)
        {            
            objReceiptVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[0].ToString());
            objReceiptVoucherDAL.BookType = inputArrayList[1].ToString();
            objReceiptVoucherDAL.NarrationMaster = inputArrayList[2].ToString();
            objReceiptVoucherDAL.ChequeNo = inputArrayList[3].ToString();
            objReceiptVoucherDAL.ChequeDate = inputArrayList[4].ToString();
            return objReceiptVoucherDAL.InsertMasterRecord(objReceiptVoucherDAL);
        }

        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objReceiptVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objReceiptVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objReceiptVoucherDAL.BookType = inputArrayList[2].ToString();
            objReceiptVoucherDAL.NarrationMaster = inputArrayList[3].ToString();
            objReceiptVoucherDAL.ChequeNo = inputArrayList[4].ToString();
            objReceiptVoucherDAL.ChequeDate = inputArrayList[5].ToString();
            objReceiptVoucherDAL.UpdateMasterRecord(objReceiptVoucherDAL);
        }

        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objReceiptVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objReceiptVoucherDAL.AccountId = Convert.ToInt32(inputArrayList[1]);
            objReceiptVoucherDAL.DebitAmount = Convert.ToDecimal(inputArrayList[2]);
            objReceiptVoucherDAL.CreditAmount = Convert.ToDecimal(inputArrayList[3]);
            objReceiptVoucherDAL.AccountType = inputArrayList[4].ToString();
            objReceiptVoucherDAL.InsertDetailRecord(objReceiptVoucherDAL);
        }
       
        public void DeleteMasterDetailRecord(int ID)
        {
            objReceiptVoucherDAL.DeleteMasterDetailRecord(ID);
        }

        public void DeleteDetailRecord(int ID)
        {
            objReceiptVoucherDAL.DeleteDetailRecord(ID);
        }

        #endregion
    }
}
