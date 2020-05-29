using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class LetterCreditBR
    {
        #region Variables

        LetterCreditDAL objLetterCreditDAL = new LetterCreditDAL();

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int LCId)
        {
            return objLetterCreditDAL.GetMasterRecord(LCId);
        }
        public DataTable GetDetailRecords(int LCId)
        {
            return objLetterCreditDAL.GetDetailRecords(LCId);
        }
        public DataTable GetOrderExist(int OrderCode)
        {
            return objLetterCreditDAL.GetOrderExist(OrderCode);
        }
        public DataTable OrderExistinPerAlertShipment(int LCId)
        {
            return objLetterCreditDAL.OrderExistinPerAlertShipment(LCId);
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            return objLetterCreditDAL.GetAllMasterRecords(Search);
        }
        public DataTable GetMaxRecords()
        {
            return objLetterCreditDAL.GetMaxRecords();
        }

        public DataTable GetVendors()
        {
            return objLetterCreditDAL.GetVendors();
        }
        public DataTable GetBanks(string BankType)
        {
            return objLetterCreditDAL.GetBanks(BankType);
        }
        public DataTable GetCurrency()
        {
            return objLetterCreditDAL.GetCurrency();
        }
        public DataTable GetInsurer()
        {
            return objLetterCreditDAL.GetInsurer();
        }
        public DataTable GetCountry()
        {
            return objLetterCreditDAL.GetCountry();
        }

        public DataTable GetModelNo()
        {
            return objLetterCreditDAL.GetModelNo();
        }

        public DataTable GetOrderRequisition()
        {
            return objLetterCreditDAL.GetOrderRequisition();
        }
        public DataTable GetOrderRequisition(int ConfirmId)
        {
            return objLetterCreditDAL.GetOrderRequisition(ConfirmId);
        }

        public DataTable GetProformaNo(int ConfirmDetailId)
        {
            return objLetterCreditDAL.GetProformaNo(ConfirmDetailId);
        }

        public void InsertMasterRecord(ArrayList inputArrayList)
        {
            objLetterCreditDAL.LCID = Convert.ToInt32(inputArrayList[0]);

            objLetterCreditDAL.LCDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objLetterCreditDAL.LCExpiry = CommonObjects.ConvertMMDDYYYY(inputArrayList[2].ToString());
            objLetterCreditDAL.ShipmentDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[3].ToString());

            objLetterCreditDAL.VendorId = Convert.ToInt32(inputArrayList[4]);
            
            objLetterCreditDAL.BankId = Convert.ToInt32(inputArrayList[5]);
            objLetterCreditDAL.SupplierBank = Convert.ToInt32(inputArrayList[6]);  
          
            objLetterCreditDAL.InsurerId = Convert.ToInt32(inputArrayList[7]);
            objLetterCreditDAL.CoverNoteNo = inputArrayList[8].ToString();
            objLetterCreditDAL.Remarks = inputArrayList[9].ToString();
            objLetterCreditDAL.LCNumber = inputArrayList[10].ToString();
            objLetterCreditDAL.InsertMasterRecord(objLetterCreditDAL);
        }

        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objLetterCreditDAL.LCID = Convert.ToInt32(inputArrayList[0]);

            objLetterCreditDAL.LCDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objLetterCreditDAL.LCExpiry = CommonObjects.ConvertMMDDYYYY(inputArrayList[2].ToString());
            objLetterCreditDAL.ShipmentDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[3].ToString());

            objLetterCreditDAL.VendorId = Convert.ToInt32(inputArrayList[4]);

            objLetterCreditDAL.BankId = Convert.ToInt32(inputArrayList[5]);
            objLetterCreditDAL.SupplierBank = Convert.ToInt32(inputArrayList[6]);

            objLetterCreditDAL.InsurerId = Convert.ToInt32(inputArrayList[7]);
            objLetterCreditDAL.CoverNoteNo = inputArrayList[8].ToString();
            objLetterCreditDAL.Remarks = inputArrayList[9].ToString();
            objLetterCreditDAL.LCNumber = inputArrayList[10].ToString();

            objLetterCreditDAL.UpdateMasterRecord(objLetterCreditDAL);
        }

        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objLetterCreditDAL.LCID = Convert.ToInt32(inputArrayList[0]);
            objLetterCreditDAL.OrderId = Convert.ToInt32(inputArrayList[1]);
            objLetterCreditDAL.ConfirmDetailId = Convert.ToInt32(inputArrayList[2]);

            objLetterCreditDAL.ProductId = Convert.ToInt32(inputArrayList[3]);

            objLetterCreditDAL.ConfQty = Convert.ToInt32(inputArrayList[4]);
            objLetterCreditDAL.CostPrice = Convert.ToDecimal(inputArrayList[5]);

            objLetterCreditDAL.ConfirmAmount = Convert.ToDecimal(inputArrayList[6]);
            objLetterCreditDAL.ShipQuantity = Convert.ToInt32(inputArrayList[7]);

            objLetterCreditDAL.IncentiveRate = Convert.ToDecimal(inputArrayList[8].ToString());
            objLetterCreditDAL.DisctdRate = Convert.ToDecimal(inputArrayList[9].ToString());

            objLetterCreditDAL.InsertDetailRecord(objLetterCreditDAL);
        }

        public void DeleteMasterDetailRecord(int ID)
        {
            objLetterCreditDAL.DeleteMasterDetailRecord(ID);
        }
        public void DeleteDetailRecord(int ID)
        {
            objLetterCreditDAL.DeleteDetailRecord(ID);
        }

        #endregion
    }
}

