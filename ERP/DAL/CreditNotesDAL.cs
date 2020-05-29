using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class CreditNotesDAL
    {
        public CreditNotesDAL()
        { }

        # region Variables

        DataOperations objDataOperations = new DataOperations();

        # endregion

        #region properties

        public int CNID { get; set; }
        public DateTime CreditDate { get; set; }

        public int VendorId { get; set; }
        public string Remarks { get; set; }

        public int CreditNotesDetailId { get; set; }
        public int OrderId { get; set; }
        public int ConfirmDetailId { get; set; }

        public decimal ConfirmAmount { get; set; }
        public int ShipQuantity { get; set; }

        public int ProductId { get; set; }
        public int ColorId { get; set; }

        public int ConfQty { get; set; }
        public decimal CostPrice { get; set; }

        public int InsurerId { get; set; }
        public string CoverNoteNo { get; set; }
        public string Weeks { get; set; }

        public decimal IncentiveRate { get; set; }
        public decimal DisctdRate { get; set; }

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int CNID)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = "SELECT * FROM CreditNotesMaster where CNID=@CNID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CNID", CNID);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Credit Notes Master Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable GetDetailRecords(int CNID)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"SELECT b.*,c.ProductName,c.ProductCode FROM CreditNotesMaster a
                              inner join CreditNotesDetail b on a.CNID=b.CNID
                              inner join Products c on b.ProductId=c.ProductId
                              where a.CNID=@CNID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CNID", CNID);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Credit Notes detail Records: {0}", exception.Message), exception);
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
                              inner join CreditNotesDetail b on a.OrderId=b.OrderId
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
                throw new Exception(string.Format("Error occured while getting Credit Notes detail Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return OrderDataTable;
        }

        public DataTable OrderExistinGRN(int CNID)
        {
            DataTable OrderDataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"SELECT * FROM GRNDetail where CNID=@CNID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CNID", CNID);
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(OrderDataTable);
                    sqlAdapter.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Credit Notes Records: {0}", exception.Message), exception);
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

                string sql = @"SELECT CNID,CNCode,CreditDate,Week,b.CompanyName VendorName,c.Name InsurerName,
                              dbo.fnTotalValues(CNID,4) as TotalAmounts
                              FROM CreditNotesMaster a
                              inner join  vendors b on a.VendorID=b.VendorID 
                              inner join  Insurance c on a.InsurerId=c.InsuranceID  
                              where " + WhereClause + " order by CNID desc";

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
                throw new Exception(string.Format("Error occured while getting Credit Notes Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return UserDataTable;
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

        public DataTable GetInsurer()
        {
            DataTable getData = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = "Select InsuranceID,Name from Insurance order by Name";
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
                              INNER JOIN CreditNotesDetail c ON b.ConfirmDetailId = c.ConfirmDetailId)
                              and confirmId NOT IN (SELECT DISTINCT a.confirmId
                              FROM OrderConfirmationMaster a
                              INNER JOIN OrderConfirmationDetail b ON a.ConfirmId = b.ConfirmId
                              INNER JOIN LCDetail c ON b.ConfirmDetailId = c.ConfirmDetailId)
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
                throw new Exception(string.Format("Error occured while getting Credit Notes Max Records: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }

        public int InsertMasterRecord(CreditNotesDAL objCreditNotesDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            int MaxCode = 0;
            try
            {
                MaxCode = GetAutoCodeGenerate(FormsCode.CreditNotes);
                //string CNCode = FormsCode.CreditNotes + System.DateTime.Now.ToString("MM") + System.DateTime.Now.ToString("yy") + FormatMaxCode(MaxCode);
                string[] splitWeek = objCreditNotesDAL.Weeks.Split('-');
                string Week="", CNCode, sql;

                if (splitWeek.Length > 1)
                    Week = splitWeek[0] + splitWeek[1].Substring(2, 2);

                CNCode = FormsCode.CreditNotes + Week + FormatMaxCode(MaxCode);

                sql = @"INSERT INTO CreditNotesMaster (CNID,CNCode,CreditDate,
                        VendorId,InsurerId,CoverNoteNo,Remarks,Week,BranchId,LocationId,
                        AddedBy,AddedOn,UpdatedBy,UpdatedOn)
                        VALUES (@CNID,@CNCode,@CreditDate,@VendorId,@InsurerId,@CoverNoteNo,@Remarks,@Week,
                        @BranchId,@LocationId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CNID", MaxCode);
                    sqlCommand.Parameters.AddWithValue("@CNCode", CNCode);

                    sqlCommand.Parameters.AddWithValue("@CreditDate", objCreditNotesDAL.CreditDate);
                    sqlCommand.Parameters.AddWithValue("@VendorId", objCreditNotesDAL.VendorId);

                    sqlCommand.Parameters.AddWithValue("@InsurerId", objCreditNotesDAL.InsurerId);
                    sqlCommand.Parameters.AddWithValue("@CoverNoteNo", objCreditNotesDAL.CoverNoteNo);

                    sqlCommand.Parameters.AddWithValue("@Remarks", objCreditNotesDAL.Remarks);
                    sqlCommand.Parameters.AddWithValue("@Week", objCreditNotesDAL.Weeks);

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
                    sqlCommand.Parameters.AddWithValue("@CodeType", FormsCode.CreditNotes);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Credit Notes Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return MaxCode;
        }

        public void UpdateMasterRecord(CreditNotesDAL objCreditNotesDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);

            try
            {
                string sql = @"Update CreditNotesMaster SET CreditDate = @CreditDate,
                               VendorId = @VendorId,InsurerId=@InsurerId,CoverNoteNo=@CoverNoteNo,
                               Remarks = @Remarks,Week = @Week,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn
                               WHERE CNID = @CNID";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CNID", objCreditNotesDAL.CNID);
                    sqlCommand.Parameters.AddWithValue("@CreditDate", objCreditNotesDAL.CreditDate);
                    sqlCommand.Parameters.AddWithValue("@VendorId", objCreditNotesDAL.VendorId);

                    sqlCommand.Parameters.AddWithValue("@InsurerId", objCreditNotesDAL.InsurerId);
                    sqlCommand.Parameters.AddWithValue("@CoverNoteNo", objCreditNotesDAL.CoverNoteNo);

                    sqlCommand.Parameters.AddWithValue("@Remarks", objCreditNotesDAL.Remarks);
                    sqlCommand.Parameters.AddWithValue("@Week", objCreditNotesDAL.Weeks);

                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                    sqlCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Credit Notes Master: {0}", exception.Message), exception);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public void InsertDetailRecord(CreditNotesDAL objCreditNotesDAL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            try
            {
                string sql = @"INSERT INTO CreditNotesDetail(CNID,OrderId,ConfirmDetailId,ProductId,
                              ConfQty,CostPrice, ConfirmAmount,ShipQuantity,IncentiveRate,DisctdRate)
                              VALUES (@CNID,@OrderId,@ConfirmDetailId,@ProductId,@ConfQty,@CostPrice,
                              @ConfirmAmount,@ShipQuantity,@IncentiveRate,@DisctdRate)";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CNID", objCreditNotesDAL.CNID);
                    sqlCommand.Parameters.AddWithValue("@OrderId", objCreditNotesDAL.OrderId);
                    sqlCommand.Parameters.AddWithValue("@ConfirmDetailId", objCreditNotesDAL.ConfirmDetailId);

                    sqlCommand.Parameters.AddWithValue("@ProductId", objCreditNotesDAL.ProductId);

                    sqlCommand.Parameters.AddWithValue("@ConfQty", objCreditNotesDAL.ConfQty);
                    sqlCommand.Parameters.AddWithValue("@CostPrice", objCreditNotesDAL.CostPrice);

                    sqlCommand.Parameters.AddWithValue("@ConfirmAmount", objCreditNotesDAL.ConfirmAmount);
                    sqlCommand.Parameters.AddWithValue("@ShipQuantity", objCreditNotesDAL.ShipQuantity);

                    sqlCommand.Parameters.AddWithValue("@IncentiveRate", objCreditNotesDAL.IncentiveRate);
                    sqlCommand.Parameters.AddWithValue("@DisctdRate", objCreditNotesDAL.DisctdRate);

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Credit Notes Detail: {0}", exception.Message), exception);
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
                string sql = @"Delete From CreditNotesMaster WHERE CNID = @CNID";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CNID", ID);
                    sqlCommand.ExecuteNonQuery();
                }
                sql = @"Delete From CreditNotesDetail WHERE CNID = @CNID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CNID", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Credit Notes Mster & Detail : {0}", exception.Message), exception);
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
                string sql = @"Delete From CreditNotesDetail WHERE CNID = @CNID";
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@CNID", ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Detail Credit Notes  : {0}", exception.Message), exception);
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
