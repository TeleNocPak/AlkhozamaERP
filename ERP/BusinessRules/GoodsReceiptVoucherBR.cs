using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class GoodsReceiptVoucherBR
    {
        #region Variables

        GoodsReceiptVoucherDAL objGoodsReceiptVoucherDAL = new GoodsReceiptVoucherDAL();

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int EndorsementId)
        {
            return objGoodsReceiptVoucherDAL.GetMasterRecord(EndorsementId);
        }

        public DataTable GePreAlertShipmentMasterRecords(string InvoiceNo)
        {
            return objGoodsReceiptVoucherDAL.GePreAlertShipmentMasterRecords(InvoiceNo);
        }

        public DataTable GePreAlertShipmentDetailInvoiceWise(string InvoiceNo)
        {
            return objGoodsReceiptVoucherDAL.GePreAlertShipmentDetailInvoiceWise(InvoiceNo);
        }

        public DataTable GePreAlertShipmentDetailRecords(int VoucherId)
        {
            return objGoodsReceiptVoucherDAL.GePreAlertShipmentDetailRecords(VoucherId);
        }

        public bool ShipmentExistinGRN(int VoucherId, string InvoiceNo)
        {
            return objGoodsReceiptVoucherDAL.ShipmentExistinGRN(VoucherId, InvoiceNo);
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            return objGoodsReceiptVoucherDAL.GetAllMasterRecords(Search);
        }

        public DataTable GetBanks(string BankType)
        {
            return objGoodsReceiptVoucherDAL.GetBanks(BankType);
        }
        public DataTable GetModelNo()
        {
            return objGoodsReceiptVoucherDAL.GetModelNo();
        }

        public DataTable GetWarehouse(int BranchId)
        {
            return objGoodsReceiptVoucherDAL.GetWarehouse(BranchId);
        }

        public DataTable GetProformaNoShipmentWise(int LCDetailId)
        {
            return objGoodsReceiptVoucherDAL.GetProformaNoShipmentWise(LCDetailId);
        }

        public DataTable GetProformaNoShipmentWiseCreditNotes(int CNDetailId)
        {
            return objGoodsReceiptVoucherDAL.GetProformaNoShipmentWiseCreditNotes(CNDetailId);
        }

        public decimal GetTotalMarginHeldAmount()
        {
            return objGoodsReceiptVoucherDAL.GetTotalMarginHeldAmount();
        }

        public int InsertMasterRecord(ArrayList inputArrayList)
        {
            objGoodsReceiptVoucherDAL.ShipmentId = Convert.ToInt32(inputArrayList[0]);
            objGoodsReceiptVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objGoodsReceiptVoucherDAL.Remarks = inputArrayList[2].ToString();
            objGoodsReceiptVoucherDAL.WarehouseId = Convert.ToInt32(inputArrayList[3].ToString());
            return objGoodsReceiptVoucherDAL.InsertMasterRecord(objGoodsReceiptVoucherDAL);
        }

        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objGoodsReceiptVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objGoodsReceiptVoucherDAL.ShipmentIdDetailId = Convert.ToInt32(inputArrayList[1]);
            objGoodsReceiptVoucherDAL.InsertDetailRecord(objGoodsReceiptVoucherDAL);
        }

        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objGoodsReceiptVoucherDAL.VoucherId = Convert.ToInt32(inputArrayList[0]);
            objGoodsReceiptVoucherDAL.ShipmentId = Convert.ToInt32(inputArrayList[1]);
            objGoodsReceiptVoucherDAL.VoucherDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[2].ToString());
            objGoodsReceiptVoucherDAL.Remarks = inputArrayList[3].ToString();
            objGoodsReceiptVoucherDAL.WarehouseId = Convert.ToInt32(inputArrayList[4].ToString());
            objGoodsReceiptVoucherDAL.UpdateMasterRecord(objGoodsReceiptVoucherDAL);
        }

        public void DeleteMasterDetailRecord(int ID)
        {
            objGoodsReceiptVoucherDAL.DeleteMasterDetailRecord(ID);
        }

        public void DeleteDetailRecord(int ID)
        {
            objGoodsReceiptVoucherDAL.DeleteDetailRecord(ID);
        }

        #endregion
    }
}

