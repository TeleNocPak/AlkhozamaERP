using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class GoodsReceiptVoucherDAL
    {
        public GoodsReceiptVoucherDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int VoucherId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int ShipmentId { get; set; }
        public string Remarks { get; set; }
        public int WarehouseId { get; set; }

        public int ShipmentIdDetailId { get; set; }

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int VoucherId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                //                string sql = @"SELECT a.*,d.VoucherId,d.VoucherCode,d.VoucherDate,d.ShipmentId,d.Remarks GRNRemarks,
                //                               b.CoverNoteNo,b.LCNumber,c.BankName
                //                               FROM PreAlertShipmentMaster a
                //                               inner join LCMaster b on a.LCId=b.LCId
                //                               inner join Bank c on b.BankId=c.BankId
                //                               inner join GRNMaster d on a.ShipmentId=d.ShipmentId
                //                               where d.VoucherId=@VoucherId";

                string sql = @"SELECT distinct a.VoucherId,a.VoucherCode,a.VoucherDate,
                               a.ShipmentId,a.Remarks GRNRemarks,a.WarehouseId,d.InvoiceNo,c.LCId,c.ShipmentDate,c.HAWB,c.MAWB, 
                               isnull(e.LCNumber,'') LCNumber,isnull(f.BankName,'') BankName,isnull(g.Name,'') InsuranceName,
                               isnull(i.Name,'') InsuranceNameCN,
                               isnull(h.CoverNoteNo,'') CoverNoteNoCN,h.CNId from GRNMaster a
                               inner join GRNDetail b on a.VoucherId=b.VoucherId
                               inner join PreAlertShipmentMaster c on a.ShipmentId=c.ShipmentId
                               inner join PreAlertShipmentDetail d on b.ShipmentIdDetailId=d.ShipmentIdDetailId
                               left join LCMaster e on c.LCId=e.LCId
                               left join Bank f on e.BankId=f.BankId
                               left join insurance g on e.InsurerId =g.InsuranceID
                               left join CreditNotesMaster h on c.CNId=h.CNId
                               left join Insurance i on h.InsurerId=i.InsuranceID
                               where a.VoucherId=@VoucherId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@VoucherId", VoucherId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Goods Receipt Voucher Master Records : {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GePreAlertShipmentMasterRecords(string InvoiceNo)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"SELECT distinct a.*,isnull(c.CoverNoteNo,'') CoverNoteNo,isnull(c.LCNumber,'') LCNumber,
                                isnull(d.BankName,'') BankName,isnull(e.Name,'') InsuranceName,
                                isnull(f.CoverNoteNo,'') CoverNoteNoCN,isnull(i.Name,'') InsurerNameCN
                                FROM PreAlertShipmentMaster a
                                inner join PreAlertShipmentDetail b on a.ShipmentId=b.ShipmentId
                                left join LCMaster c on a.LCId=c.LCId
                                left join Bank d on c.BankId=d.BankId
                                left join insurance e on c.InsurerId =e.InsuranceID
                                left join CreditNotesMaster f on a.CNId=f.CNId
                                left join  Insurance i on f.InsurerId=i.InsuranceID
                                where b.InvoiceNo=@InvoiceNo";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Pre Alert Shipment Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GePreAlertShipmentDetailInvoiceWise(string InvoiceNo)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {

                string sql = @"SELECT a.LCId,a.CNId,b.*,c.ProductName,c.ProductCode FROM PreAlertShipmentMaster a
                              inner join PreAlertShipmentDetail b on a.ShipmentId=b.ShipmentId
                              inner join Products c on b.ProductId=c.ProductId
                              where b.InvoiceNo=@InvoiceNo";


                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Shipment detail Records Invoice wise: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GePreAlertShipmentDetailRecords(int VoucherId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT d.LCId,d.CNId,c.*,e.ProductName,e.ProductCode
                               from  GRNMaster a
                               inner join GRNDetail b on a.VoucherId=b.VoucherId
                               inner join PreAlertShipmentDetail c on b.ShipmentIdDetailId=c.ShipmentIdDetailId
                               inner join PreAlertShipmentMaster d on c.ShipmentId=d.ShipmentId
                               inner join Products e on c.ProductId=e.ProductId
                               where a.VoucherId=@VoucherId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@VoucherId", VoucherId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Shipment detail Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public bool ShipmentExistinGRN(int VoucherId, string InvoiceNo)
        {
            DataTable OrderDataTable = new DataTable();
            bool RecordExist = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"SELECT c.InvoiceNo FROM GRNMaster a
                               inner join GRNDetail b on a.VoucherId=b.VoucherId     
                               inner join PreAlertShipmentDetail c on b.ShipmentIdDetailId=c.ShipmentIdDetailId
                               where c.InvoiceNo=@InvoiceNo and a.VoucherId!=@VoucherId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                    sqlCommand.Parameters.AddWithValue("@VoucherId", VoucherId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    if (OrderDataTable.Rows.Count > 0)
                        RecordExist = true;
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Shipment Record Exist in Goods Receipt Voucher: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return RecordExist;
        }

        public DataTable GetAllMasterRecords(string Search)
        {
            DataTable UserDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            string WhereClause = "1=1";

            try
            {
                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                //                string sql = @"SELECT distinct a.VoucherId,a.VoucherCode,a.VoucherDate,
                //                               a.ShipmentId,b.HAWB,b.ShipmentCode,b.ShipmentDate,c.InvoiceNo,d.LCNumber
                //                               from GRNMaster a
                //                               inner join PreAlertShipmentMaster b on a.ShipmentId=b.ShipmentId      
                //                               inner join PreAlertShipmentDetail c on b.ShipmentId=c.ShipmentId      
                //                               inner join LCMaster d on b.LCId=d.LCId
                //                               inner join Products e on c.ProductID=e.ProductID
                //                               inner join GRNDetail f on c.ShipmentIdDetailId=f.ShipmentIdDetailId 
                //                               where " + WhereClause + " order by a.VoucherId desc";


                string sql = @"SELECT distinct a.VoucherId,a.VoucherCode,a.VoucherDate,dbo.fnTotalValues(a.VoucherId,10) as TotalAmounts
                               from GRNMaster a
                               inner join GRNDetail b on a.VoucherId=b.VoucherId
                               inner join PreAlertShipmentDetail c on b.ShipmentIdDetailId=c.ShipmentIdDetailId
                               inner join PreAlertShipmentMaster d on c.ShipmentId=d.ShipmentId 
                               left join LCMaster e on d.LCId=e.LCId
                               inner join Products f on c.ProductID=f.ProductID
                               where " + WhereClause + " order by a.VoucherId desc";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(UserDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Pre Goods Receipt Voucher Master Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }

        public DataTable GetBanks(string BankType)
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from Bank where BankType=@BankType order by BankName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Parameters.AddWithValue("@BankType", BankType);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Bank Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }

        public DataTable GetModelNo()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select ModelId,ModelName from ModelNo order by ModelName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Model Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }

        public DataTable GetWarehouse(int BranchId)
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Select WarehouseId,WarehouseName from Warehouse where BranchId=@BranchId order by WarehouseName";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Parameters.AddWithValue("@BranchId", BranchId);
                    sqlAdapter.Fill(getData);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Warehouse Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }


        public DataTable GetProformaNoShipmentWise(int LCDetailId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT ProformaNo FROM OrderConfirmationMaster
                                WHERE confirmId IN (SELECT DISTINCT a.confirmId
                                FROM OrderConfirmationMaster a
                                INNER JOIN OrderConfirmationDetail b ON a.ConfirmId = b.ConfirmId
                                INNER JOIN LCDetail c ON b.ConfirmDetailId = c.ConfirmDetailId
                                INNER JOIN PreAlertShipmentDetail d ON c.LCDetailId = d.LCDetailId
                                AND d.LCDetailId=@LCDetailId)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@LCDetailId", LCDetailId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Proforma No. Records in pre Alert Shipment: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetProformaNoShipmentWiseCreditNotes(int CNDetailId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT ProformaNo FROM OrderConfirmationMaster
                                WHERE confirmId IN (SELECT DISTINCT a.confirmId
                                FROM OrderConfirmationMaster a
                                INNER JOIN OrderConfirmationDetail b ON a.ConfirmId = b.ConfirmId
                                INNER JOIN CreditNotesDetail c ON b.ConfirmDetailId = c.ConfirmDetailId
                                INNER JOIN PreAlertShipmentDetail d ON c.CNDetailId = d.CNDetailId
                                AND d.CNDetailId=@CNDetailId)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CNDetailId", CNDetailId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Proforma No. Credit Notes Records in pre Alert Shipment: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public decimal GetTotalMarginHeldAmount()
        {
            DataTable OrderDataTable = new DataTable();
            decimal TotalMarginHeldAmount = 0;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT isNull(sum(MarginHeld),0) as MarginHeldAmount from bankendorsementmaster";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    TotalMarginHeldAmount = Convert.ToDecimal(OrderDataTable.Rows[0]["MarginHeldAmount"]);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Proforma No. Records in pre Alert Shipment: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return TotalMarginHeldAmount;
        }

        public int GetAutoCodeGenerate(string CodeType)
        {
            int MaxCode = 0;
            DataTable DataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT  isNull(Max(Code),0) as MaxCode FROM uniquecode where CodeType=@CodeType";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CodeType", CodeType);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(DataTable);

                    if (Convert.ToInt32(DataTable.Rows[0]["MaxCode"]) != 0)
                        MaxCode = Convert.ToInt32(DataTable.Rows[0]["MaxCode"]) + 1;
                    else
                        MaxCode = 1;

                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Order Requisition Max Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }

        public int InsertMasterRecord(GoodsReceiptVoucherDAL objGoodsReceiptVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            int MaxCode = 0;
            try
            {
                MaxCode = GetAutoCodeGenerate(FormsCode.GoodsReceiptVoucher);
                string VoucherCode = FormsCode.GoodsReceiptVoucher + System.DateTime.Now.ToString("MM") + System.DateTime.Now.ToString("yy") + FormatMaxCode(MaxCode);

                string sql = @"INSERT INTO GRNMaster(VoucherId,VoucherCode,ShipmentId,VoucherDate,
                              Remarks,WarehouseId,BranchId,LocationId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)
                              VALUES (@VoucherId,@VoucherCode,@ShipmentId,@VoucherDate,@Remarks,@WarehouseId,
                              @BranchId,@LocationId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", MaxCode);
                    sqlCommand.Parameters.AddWithValue("@VoucherCode", VoucherCode);
                    sqlCommand.Parameters.AddWithValue("@ShipmentId", objGoodsReceiptVoucherDAL.ShipmentId);
                    sqlCommand.Parameters.AddWithValue("@VoucherDate", objGoodsReceiptVoucherDAL.VoucherDate);
                    sqlCommand.Parameters.AddWithValue("@Remarks", objGoodsReceiptVoucherDAL.Remarks);
                    sqlCommand.Parameters.AddWithValue("@WarehouseId", objGoodsReceiptVoucherDAL.WarehouseId);

                    sqlCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                    sqlCommand.Parameters.AddWithValue("@LocationId", CommonObjects.GetLocationId());

                    sqlCommand.Parameters.AddWithValue("@AddedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@AddedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY("01/01/1900"));
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }

                sql = @"Update uniquecode SET Code = @Code where CodeType=@CodeType";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Code", MaxCode);
                    sqlCommand.Parameters.AddWithValue("@CodeType", FormsCode.GoodsReceiptVoucher);
                    sqlCommand.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Goods Receipt Voucher Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }
        public void UpdateMasterRecord(GoodsReceiptVoucherDAL objGoodsReceiptVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update GRNMaster SET ShipmentId = @ShipmentId,
                               VoucherDate=@VoucherDate,Remarks=@Remarks,WarehouseId=@WarehouseId,
                               UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn 
                               WHERE VoucherId=@VoucherId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", objGoodsReceiptVoucherDAL.VoucherId);
                    sqlCommand.Parameters.AddWithValue("@ShipmentId", objGoodsReceiptVoucherDAL.ShipmentId);
                    sqlCommand.Parameters.AddWithValue("@VoucherDate", objGoodsReceiptVoucherDAL.VoucherDate);
                    sqlCommand.Parameters.AddWithValue("@Remarks", objGoodsReceiptVoucherDAL.Remarks);
                    sqlCommand.Parameters.AddWithValue("@WarehouseId", objGoodsReceiptVoucherDAL.WarehouseId);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Goods Receipt Voucher Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void InsertDetailRecord(GoodsReceiptVoucherDAL objGoodsReceiptVoucherDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO GRNDetail(VoucherId,ShipmentIdDetailId)
                              VALUES (@VoucherId,@ShipmentIdDetailId)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", objGoodsReceiptVoucherDAL.VoucherId);
                    sqlCommand.Parameters.AddWithValue("@ShipmentIdDetailId", objGoodsReceiptVoucherDAL.ShipmentIdDetailId);

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Goods Receipt Voucher Detail: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void DeleteMasterDetailRecord(int ID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Delete From GRNMaster WHERE VoucherId = @VoucherId";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
                sql = @"Delete From GRNDetail WHERE VoucherId = @VoucherId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Goods Receipt Voucher Mster & Detail : {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void DeleteDetailRecord(int ID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"Delete From GRNDetail WHERE VoucherId = @VoucherId";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@VoucherId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Goods Receipt Voucher Detail: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        private string FormatMaxCode(int Code)
        {
            string NewCode = string.Empty;
            if (Code.ToString().Length.Equals(1))
                NewCode = "0000" + Code.ToString();
            else if (Code.ToString().Length.Equals(2))
                NewCode = "000" + Code.ToString();
            else if (Code.ToString().Length.Equals(3))
                NewCode = "00" + Code.ToString();
            else if (Code.ToString().Length.Equals(4))
                NewCode = "0" + Code.ToString();
            else
                NewCode = Code.ToString();

            return NewCode;
        }

        #endregion
    }
}

