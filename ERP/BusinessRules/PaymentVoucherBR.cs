using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class PaymentVoucherBR
    {
        #region Variables

        PaymentVoucherDAL objPaymentVoucherDAL = new PaymentVoucherDAL();

        #endregion

        #region Methods


        public DataTable GetMasterRecord(int OrderId)
        {
            return objPaymentVoucherDAL.GetMasterRecord(OrderId);
        }
        public DataTable GetDetailRecords(int OrderId)
        {
            return objPaymentVoucherDAL.GetDetailRecords(OrderId);
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            return objPaymentVoucherDAL.GetAllMasterRecords(Search);
        }
       
        public string GetAccountCode(int AccountId)
        {
            return objPaymentVoucherDAL.GetAccountCode(AccountId);
        }

        public DataTable OrderExistinConfirm(int OrderID)
        {
            return objPaymentVoucherDAL.OrderExistinConfirm(OrderID);
        }

        public bool GetAccountNameIdExist(int AccountId, string AccountName)
        {
            return objPaymentVoucherDAL.GetAccountNameIdExist(AccountId, AccountName);
        }
          
        public int InsertMasterRecord(ArrayList inputArrayList)
        {            
            objPaymentVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[0].ToString());
            objPaymentVoucherDAL.BookType = inputArrayList[1].ToString();
            objPaymentVoucherDAL.NarrationMaster = inputArrayList[2].ToString();
            objPaymentVoucherDAL.ChequeNo = inputArrayList[3].ToString();
            objPaymentVoucherDAL.ChequeDate = inputArrayList[4].ToString();

            return objPaymentVoucherDAL.InsertMasterRecord(objPaymentVoucherDAL);
        }

        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objPaymentVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objPaymentVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objPaymentVoucherDAL.BookType = inputArrayList[2].ToString();
            objPaymentVoucherDAL.NarrationMaster = inputArrayList[3].ToString();
            objPaymentVoucherDAL.ChequeNo = inputArrayList[4].ToString();
            objPaymentVoucherDAL.ChequeDate = inputArrayList[5].ToString();
            objPaymentVoucherDAL.UpdateMasterRecord(objPaymentVoucherDAL);
        }

        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objPaymentVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objPaymentVoucherDAL.AccountId = Convert.ToInt32(inputArrayList[1]);
            objPaymentVoucherDAL.DebitAmount = Convert.ToDecimal(inputArrayList[2]);
            objPaymentVoucherDAL.CreditAmount = Convert.ToDecimal(inputArrayList[3]);
            objPaymentVoucherDAL.AccountType = inputArrayList[4].ToString();
            objPaymentVoucherDAL.InsertDetailRecord(objPaymentVoucherDAL);
        }
       
        public void DeleteMasterDetailRecord(int ID)
        {
            objPaymentVoucherDAL.DeleteMasterDetailRecord(ID);
        }

        public void DeleteDetailRecord(int ID)
        {
            objPaymentVoucherDAL.DeleteDetailRecord(ID);
        }

        #endregion
    }
}
