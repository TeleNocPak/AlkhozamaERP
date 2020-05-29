using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class CreditNotesBR
    {
        #region Variables

        CreditNotesDAL objCreditNotesDAL = new CreditNotesDAL();

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int CNID)
        {
            return objCreditNotesDAL.GetMasterRecord(CNID);
        }
        public DataTable GetDetailRecords(int CNID)
        {
            return objCreditNotesDAL.GetDetailRecords(CNID);
        }
        public DataTable GetOrderExist(int OrderCode)
        {
            return objCreditNotesDAL.GetOrderExist(OrderCode);
        }
        public DataTable OrderExistinGRN(int CNID)
        {
            return objCreditNotesDAL.OrderExistinGRN(CNID);
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            return objCreditNotesDAL.GetAllMasterRecords(Search);
        }
        

        public DataTable GetVendors()
        {
            return objCreditNotesDAL.GetVendors();
        }

         public DataTable GetInsurer()
        {
            return objCreditNotesDAL.GetInsurer();
        }

        public DataTable GetOrderRequisition()
        {
            return objCreditNotesDAL.GetOrderRequisition();
        }

        public DataTable GetOrderRequisition(int ConfirmId)
        {
            return objCreditNotesDAL.GetOrderRequisition(ConfirmId);
        }

        public DataTable GetProformaNo(int ConfirmDetailId)
        {
            return objCreditNotesDAL.GetProformaNo(ConfirmDetailId);
        }

        public int InsertMasterRecord(ArrayList inputArrayList)
        {
            objCreditNotesDAL.CreditDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[0].ToString());
            objCreditNotesDAL.VendorId = Convert.ToInt32(inputArrayList[1]);
            objCreditNotesDAL.Remarks = inputArrayList[2].ToString();

            objCreditNotesDAL.InsurerId = Convert.ToInt32(inputArrayList[3]);
            objCreditNotesDAL.CoverNoteNo = inputArrayList[4].ToString();
            objCreditNotesDAL.Weeks = inputArrayList[5].ToString();

            return objCreditNotesDAL.InsertMasterRecord(objCreditNotesDAL);
        }

        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objCreditNotesDAL.CNID = Convert.ToInt32(inputArrayList[0]);
            objCreditNotesDAL.CreditDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objCreditNotesDAL.VendorId = Convert.ToInt32(inputArrayList[2]);
            objCreditNotesDAL.Remarks = inputArrayList[3].ToString();

            objCreditNotesDAL.InsurerId = Convert.ToInt32(inputArrayList[4]);
            objCreditNotesDAL.CoverNoteNo = inputArrayList[5].ToString();
            objCreditNotesDAL.Weeks = inputArrayList[6].ToString();

            objCreditNotesDAL.UpdateMasterRecord(objCreditNotesDAL);
        }

        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objCreditNotesDAL.CNID = Convert.ToInt32(inputArrayList[0]);
            objCreditNotesDAL.OrderId = Convert.ToInt32(inputArrayList[1]);
            objCreditNotesDAL.ConfirmDetailId = Convert.ToInt32(inputArrayList[2]);

            objCreditNotesDAL.ProductId = Convert.ToInt32(inputArrayList[3]);
            objCreditNotesDAL.ConfQty = Convert.ToInt32(inputArrayList[4]);
            objCreditNotesDAL.CostPrice = Convert.ToDecimal(inputArrayList[5]);

            objCreditNotesDAL.ConfirmAmount = Convert.ToDecimal(inputArrayList[6]);
            objCreditNotesDAL.ShipQuantity = Convert.ToInt32(inputArrayList[7]);

            objCreditNotesDAL.IncentiveRate = Convert.ToDecimal(inputArrayList[8].ToString());
            objCreditNotesDAL.DisctdRate = Convert.ToDecimal(inputArrayList[9].ToString());

            objCreditNotesDAL.InsertDetailRecord(objCreditNotesDAL);
        }

        public void DeleteMasterDetailRecord(int ID)
        {
            objCreditNotesDAL.DeleteMasterDetailRecord(ID);
        }

        public void DeleteDetailRecord(int ID)
        {
            objCreditNotesDAL.DeleteDetailRecord(ID);
        }

        #endregion
    }
}

