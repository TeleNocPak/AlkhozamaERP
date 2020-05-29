using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class LetterCreditDAL
    {
        public LetterCreditDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int LCID { get; set; }
        public DateTime LCDate { get; set; }
        public DateTime LCExpiry { get; set; }

        public string LCNumber { get; set; }

        public int CountryId { get; set; }
        public int VendorId { get; set; }
        public int BankId { get; set; }
        public int SupplierBank { get; set; }

        public DateTime ShipmentDate { get; set; }
        public int CurrencyId { get; set; }
        public int InsurerId { get; set; }
        public string CoverNoteNo { get; set; }
        public string Remarks { get; set; }

        public int LCDetailId { get; set; }
        public int OrderId { get; set; }
        public int ConfirmDetailId { get; set; }

        public decimal ConfirmAmount { get; set; }
        public int ShipQuantity { get; set; }

        public int ProductId { get; set; }
        public int ColorId { get; set; }

        public int ConfQty { get; set; }
        public decimal CostPrice { get; set; }

        public decimal IncentiveRate { get; set; }
        public decimal DisctdRate { get; set; }

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int LCId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = "SELECT * FROM LCMaster where LCId=@LCId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@LCId", LCId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting LC master Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetDetailRecords(int LCId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT b.*,c.ProductName,c.ProductCode FROM LCMaster a
                              inner join LCDetail b on a.LCId=b.LCId
                              inner join Products c on b.ProductId=c.ProductId
                              where a.LCId=@LCId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@LCId", LCId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting LC detail Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetOrderExist(int OrderCode)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"SELECT * FROM OrderRequisitionMaster a 
                              inner join LCDetail b on a.OrderId=b.OrderId
                              where a.OrderCode=@OrderCode";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@OrderCode", OrderCode);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting LC detail Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }
        public DataTable OrderExistinPerAlertShipment(int LCId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"SELECT LCId FROM PreAlertShipmentMaster where LCId=@LCId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@LCId", LCId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting LC Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
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

                string sql = @"SELECT distinct a.LCID,b.BankName,a.LCNumber,a.LCDate,
                               a.LCExpiry,c.Name InsurerName,f.CompanyName,dbo.fnTotalValues(a.LCID,3) as TotalAmounts
                               FROM LCMaster a
                               inner join  Bank b on a.BankId=b.BankId 
                               inner join  Insurance c on a.InsurerId=c.InsuranceID 
                               inner join LCDetail d on a.LCID=d.LCID
                               inner join Products e on d.ProductID=e.ProductID
                               inner join vendors f on a.VendorID=f.VendorID  
                               inner join OrderConfirmationDetail g on d.ConfirmDetailId=g.ConfirmDetailId
                               inner join OrderConfirmationMaster h on g.ConfirmId=h.ConfirmId
                               where " + WhereClause + " order by a.LCId desc";

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
                throw new Exception(string.Format("Error occured while getting Letter Credit Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
        }
        public DataTable GetMaxRecords()
        {
            DataTable RoleDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "SELECT  isNull(Max(LCID),0) as LCID FROM LCMaster";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(RoleDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Letter Credit Max Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return RoleDataTable;
        }

        public DataTable GetVendors()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from Vendors order by CompanyName";
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
                throw new Exception(string.Format("Error occured while getting Vendor Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
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

        public DataTable GetCurrency()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from currency order by CurrencyName";
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
                throw new Exception(string.Format("Error occured while getting Currency Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }
        public DataTable GetInsurer()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select * from Insurance order by Name";
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
                throw new Exception(string.Format("Error occured while getting Insurer Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return getData;
        }
        public DataTable GetCountry()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select InsuranceID,Name from Country order by CountryName";
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
                throw new Exception(string.Format("Error occured while getting Country Records: {0}", exception.Message), exception);
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


        public DataTable GetOrderRequisition()
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT ConfirmId, ProformaNo FROM OrderConfirmationMaster
                               WHERE confirmId NOT IN (SELECT DISTINCT a.confirmId
                               FROM OrderConfirmationMaster a
                               INNER JOIN OrderConfirmationDetail b ON a.ConfirmId = b.ConfirmId
                               INNER JOIN LCDetail c ON b.ConfirmDetailId = c.ConfirmDetailId)
                               and confirmId NOT IN (SELECT DISTINCT a.confirmId
                               FROM OrderConfirmationMaster a
                               INNER JOIN OrderConfirmationDetail b ON a.ConfirmId = b.ConfirmId
                               INNER JOIN CreditNotesDetail c ON b.ConfirmDetailId = c.ConfirmDetailId)
                               and confirmId NOT IN (SELECT DISTINCT a.confirmId
                               FROM OrderConfirmationMaster a
                               INNER JOIN OrderConfirmationDetail b ON a.ConfirmId = b.ConfirmId
                               INNER JOIN PurchaseLocalDetail c ON b.ConfirmDetailId = c.ConfirmDetailId)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Order Requ Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetOrderRequisition(int ConfirmId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT a.ProformaNo,a.ConfirmId,a.OrderId,b.ConfirmDetailId,
                              b.ConfirmQuantity,b.ConfirmAmount,b.CostPrice,IncentiveRate,DisctdRate, 
                              c.ProductName,c.ProductCode,c.ProductId From OrderConfirmationMaster a
                              inner join OrderConfirmationDetail b on a.ConfirmId=b.ConfirmId  
                              inner join Products c on b.ProductID=c.ProductID
                              where a.ConfirmId=@ConfirmId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@ConfirmId", ConfirmId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Order Requ Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetProformaNo(int ConfirmDetailId)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT a.ConfirmId,a.ProformaNo FROM OrderConfirmationMaster a
                              inner join OrderConfirmationDetail b on a.ConfirmId=b.ConfirmId
                              where b.ConfirmDetailId=@ConfirmDetailId";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@ConfirmDetailId", ConfirmDetailId);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Proforma No Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
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

        public void InsertMasterRecord(LetterCreditDAL objLetterCreditDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                int MaxCode = GetAutoCodeGenerate(FormsCode.LetterCredit);
                string LCCode = FormsCode.LetterCredit + System.DateTime.Now.ToString("MM") + System.DateTime.Now.ToString("yy") + FormatMaxCode(MaxCode);

                string sql = @"INSERT INTO LCMaster (LCID,LCCode,LCNumber,LCDate,LCExpiry,VendorId,BankId,
                              SupplierBankId,ShipmentDate,InsurerId,CoverNoteNo,Remarks,BranchId,LocationId,AddedBy,
                              AddedOn,UpdatedBy,UpdatedOn)
                              VALUES (@LCID,@LCCode,@LCNumber,@LCDate,@LCExpiry,@VendorId,@BankId,@SupplierBankId,
                              @ShipmentDate,@InsurerId,@CoverNoteNo,@Remarks,
                              @BranchId,@LocationId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LCID", objLetterCreditDAL.LCID);
                    sqlCommand.Parameters.AddWithValue("@LCCode", LCCode);
                    sqlCommand.Parameters.AddWithValue("@LCNumber", objLetterCreditDAL.LCNumber);

                    sqlCommand.Parameters.AddWithValue("@LCDate", objLetterCreditDAL.LCDate);
                    sqlCommand.Parameters.AddWithValue("@LCExpiry", objLetterCreditDAL.LCExpiry);

                    sqlCommand.Parameters.AddWithValue("@VendorId", objLetterCreditDAL.VendorId);
                    sqlCommand.Parameters.AddWithValue("@BankId", objLetterCreditDAL.BankId);
                    sqlCommand.Parameters.AddWithValue("@SupplierBankId", objLetterCreditDAL.SupplierBank);
                    sqlCommand.Parameters.AddWithValue("@ShipmentDate", objLetterCreditDAL.ShipmentDate);
                    sqlCommand.Parameters.AddWithValue("@InsurerId", objLetterCreditDAL.InsurerId);
                    sqlCommand.Parameters.AddWithValue("@CoverNoteNo", objLetterCreditDAL.CoverNoteNo);
                    sqlCommand.Parameters.AddWithValue("@Remarks", objLetterCreditDAL.Remarks);

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
                    sqlCommand.Parameters.AddWithValue("@CodeType", FormsCode.LetterCredit);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Letter Credit Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void UpdateMasterRecord(LetterCreditDAL objLetterCreditDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update LCMaster SET LCDate = @LCDate, LCNumber = @LCNumber,LCExpiry=@LCExpiry,
                               VendorId = @VendorId, BankId = @BankId, SupplierBankId=@SupplierBankId,
                               ShipmentDate = @ShipmentDate, InsurerId = @InsurerId, CoverNoteNo = @CoverNoteNo,
                               Remarks = @Remarks,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn 
                               WHERE LCID = @LCID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LCID", objLetterCreditDAL.LCID);
                    sqlCommand.Parameters.AddWithValue("@LCDate", objLetterCreditDAL.LCDate);
                    sqlCommand.Parameters.AddWithValue("@LCNumber", objLetterCreditDAL.LCNumber);
                    sqlCommand.Parameters.AddWithValue("@LCExpiry", objLetterCreditDAL.LCExpiry);

                    sqlCommand.Parameters.AddWithValue("@VendorId", objLetterCreditDAL.VendorId);
                    sqlCommand.Parameters.AddWithValue("@BankId", objLetterCreditDAL.BankId);
                    sqlCommand.Parameters.AddWithValue("@SupplierBankId", objLetterCreditDAL.SupplierBank);

                    sqlCommand.Parameters.AddWithValue("@ShipmentDate", objLetterCreditDAL.ShipmentDate);

                    sqlCommand.Parameters.AddWithValue("@InsurerId", objLetterCreditDAL.InsurerId);
                    sqlCommand.Parameters.AddWithValue("@CoverNoteNo", objLetterCreditDAL.CoverNoteNo);
                    sqlCommand.Parameters.AddWithValue("@Remarks", objLetterCreditDAL.Remarks);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Letter Credit Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void InsertDetailRecord(LetterCreditDAL objLetterCreditDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO LCDetail(LCID,OrderId,ConfirmDetailId,ProductId,
                              ConfQty,CostPrice, ConfirmAmount,ShipQuantity,IncentiveRate,DisctdRate)
                              VALUES (@LCID,@OrderId,@ConfirmDetailId,@ProductId,@ConfQty,@CostPrice,
                              @ConfirmAmount,@ShipQuantity,@IncentiveRate,@DisctdRate)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LCID", objLetterCreditDAL.LCID);
                    sqlCommand.Parameters.AddWithValue("@OrderId", objLetterCreditDAL.OrderId);
                    sqlCommand.Parameters.AddWithValue("@ConfirmDetailId", objLetterCreditDAL.ConfirmDetailId);

                    sqlCommand.Parameters.AddWithValue("@ProductId", objLetterCreditDAL.ProductId);

                    sqlCommand.Parameters.AddWithValue("@ConfQty", objLetterCreditDAL.ConfQty);
                    sqlCommand.Parameters.AddWithValue("@CostPrice", objLetterCreditDAL.CostPrice);

                    sqlCommand.Parameters.AddWithValue("@ConfirmAmount", objLetterCreditDAL.ConfirmAmount);
                    sqlCommand.Parameters.AddWithValue("@ShipQuantity", objLetterCreditDAL.ShipQuantity);

                    sqlCommand.Parameters.AddWithValue("@IncentiveRate", objLetterCreditDAL.IncentiveRate);
                    sqlCommand.Parameters.AddWithValue("@DisctdRate", objLetterCreditDAL.DisctdRate);

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Letter Credit Detail: {0}", exception.Message), exception);
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
                string sql = @"Delete From LCMaster WHERE LCId = @LCId";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LCId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
                sql = @"Delete From LCDetail WHERE LCId = @LCId";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LCId", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting LC Mster & Detail : {0}", exception.Message), exception);
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
                string sql = @"Delete From LCDetail WHERE LCID = @LCID";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@LCID", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Detail LC : {0}", exception.Message), exception);
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
