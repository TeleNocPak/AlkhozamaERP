using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class GeneralVoucherBR
    {
        #region Variables

        GeneralVoucherDAL objGeneralVoucherDAL = new GeneralVoucherDAL();

        #endregion

        #region Methods


        public DataTable GetMasterRecord(int VoucherId)
        {
            return objGeneralVoucherDAL.GetMasterRecord(VoucherId);
        }
        public DataTable GetDetailRecords(int VoucherId)
        {
            return objGeneralVoucherDAL.GetDetailRecords(VoucherId);
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            return objGeneralVoucherDAL.GetAllMasterRecords(Search);
        }
       
        //public DataTable GetColors()
        //{
        //    return objGeneralVoucherDAL.GetColors();
        //}
        //public DataTable GetProducts(int VendorId)
        //{
        //    return objGeneralVoucherDAL.GetProducts(VendorId);
        //}
        //public DataTable GetDeliveryPoin()
        //{
        //    return objGeneralVoucherDAL.GetDeliveryPoint();
        //}
        //public DataTable GetCurrency()
        //{
        //    return objGeneralVoucherDAL.GetCurrency();
        //}
        //public DataTable GetVendors()
        //{
        //    return objGeneralVoucherDAL.GetVendors();
        //}

        //public DataTable GetModelNo()
        //{
        //    return objGeneralVoucherDAL.GetModelNo();
        //}

        //public DataTable GetProductsListPrice(int ProductId)
        //{
        //    return objGeneralVoucherDAL.GetProductsListPrice(ProductId);
        //}

        public string GetAccountCode(int AccountId)
        {
            return objGeneralVoucherDAL.GetAccountCode(AccountId);
        }

        public DataTable OrderExistinConfirm(int OrderID)
        {
            return objGeneralVoucherDAL.OrderExistinConfirm(OrderID);
        }

        public bool GetAccountNameIdExist(int AccountId, string AccountName)
        {
            return objGeneralVoucherDAL.GetAccountNameIdExist(AccountId, AccountName);
        }
          
        public int InsertMasterRecord(ArrayList inputArrayList)
        {
            objGeneralVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[0].ToString());
            objGeneralVoucherDAL.BookType = inputArrayList[1].ToString();
            objGeneralVoucherDAL.NarrationMaster = inputArrayList[2].ToString();
            return objGeneralVoucherDAL.InsertMasterRecord(objGeneralVoucherDAL);
        }
        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objGeneralVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objGeneralVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objGeneralVoucherDAL.BookType = inputArrayList[2].ToString();
            objGeneralVoucherDAL.NarrationMaster = inputArrayList[3].ToString();
            objGeneralVoucherDAL.UpdateMasterRecord(objGeneralVoucherDAL);
        }

        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objGeneralVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objGeneralVoucherDAL.AccountId = Convert.ToInt32(inputArrayList[1]);
            objGeneralVoucherDAL.DebitAmount = Convert.ToDecimal(inputArrayList[2]);
            objGeneralVoucherDAL.CreditAmount = Convert.ToDecimal(inputArrayList[3]);
            objGeneralVoucherDAL.NarrationDetail = inputArrayList[4].ToString();
            objGeneralVoucherDAL.InsertDetailRecord(objGeneralVoucherDAL);
        }
       
        public void DeleteMasterDetailRecord(int ID)
        {
            objGeneralVoucherDAL.DeleteMasterDetailRecord(ID);
        }

        public void DeleteDetailRecord(int ID)
        {
            objGeneralVoucherDAL.DeleteDetailRecord(ID);
        }

        #endregion
    }
}


