using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ERP.Utilities;
using System.Data.SqlClient;

namespace ERP.DAL
{
    public partial class BrandsDAL
    {
        public BrandsDAL()
        { }

        # region Variables
        DataOperations objDataOperations = new DataOperations();
        # endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            DataTable UserDataTable = new DataTable();
            try
            {
                SqlCommand pObjCommand = new SqlCommand();
                string sql = "Select * from Brands where BrandID=@BrandID";

                pObjCommand.CommandText = sql;
                pObjCommand.Parameters.AddWithValue("@BrandID", ID);

                UserDataTable = objDataOperations.GetData(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Brand Records: {0}", exception.Message), exception);
            }
            return UserDataTable;
        }

        public DataTable GetAllRecords(string Search)
        {
            DataTable UserDataTable = new DataTable();
            string WhereClause = "BranchId=@BranchId";

            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                if (Search != null && Search.Trim().Length > 0)
                    WhereClause = Search + " and " + WhereClause;

                string sql = @"SELECT * FROM Brands where " + WhereClause + " order by BrandId desc";

                pObjCommand.CommandText = sql;
                pObjCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());

                UserDataTable = objDataOperations.GetData(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while getting Brand Records: {0}", exception.Message), exception);
            }
            return UserDataTable;
        }
        
        
        public void InsertRecord(string BrandName)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                string sql = @"INSERT INTO Brands(BrandName,BranchId,AddedBy,AddedOn,UpdatedBy,UpdatedOn)        
                            VALUES (@BrandName,@BranchId,@AddedBy,@AddedOn,@UpdatedBy,@UpdatedOn)";

                pObjCommand.CommandText = sql;
                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.Parameters.AddWithValue("@BrandName", BrandName);
                pObjCommand.Parameters.AddWithValue("@BranchId", CommonObjects.GetBranchId());
                pObjCommand.Parameters.AddWithValue("@AddedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@AddedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));
                pObjCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY("01/01/1900"));
                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while inserting Brand: {0}", exception.Message), exception);
            }
        }
        public void UpdateRecord(int BrandID, string BrandName)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();

                string sql = @"Update Brands SET BrandName = @BrandName,UpdatedBy=@UpdatedBy,
                               UpdatedOn=@UpdatedOn WHERE BrandID=@BrandID";

                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.CommandText = sql;

                pObjCommand.Parameters.AddWithValue("@BrandID", BrandID);
                pObjCommand.Parameters.AddWithValue("@BrandName", BrandName);
                pObjCommand.Parameters.AddWithValue("@UpdatedBy", CommonObjects.GetUserId());
                pObjCommand.Parameters.AddWithValue("@UpdatedOn", CommonObjects.ConvertMMDDYYYY(System.DateTime.Now.ToString("dd/MM/yyyy")));

                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Updating Brand Name: {0}", exception.Message), exception);
            }
        }
        public void DeleteRecord(int ID)
        {
            try
            {
                SqlCommand pObjCommand = new SqlCommand();
                string sql = @"Delete From Brands WHERE BrandID = @BrandID";

                pObjCommand.CommandType = System.Data.CommandType.Text;
                pObjCommand.CommandText = sql;

                pObjCommand.Parameters.AddWithValue("@BrandID", ID);
                objDataOperations.ExecuteStatement(pObjCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error occured while Deleting Brand Record: {0}", exception.Message), exception);
            }
        }

        #endregion
    }
}
