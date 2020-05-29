using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class PostDatedChequeBR
    {
        #region Variables

        PostDatedChequeDAL objPostDatedChequeDAL = new PostDatedChequeDAL();

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int PostDatedChequeId)
        {
            return objPostDatedChequeDAL.GetMasterRecord(PostDatedChequeId);
        }

        public DataTable GetRecord(int ID)
        {
            return objPostDatedChequeDAL.GetRecord(ID);
        }

        public DataTable GetDetailRecords(int PostDatedChequeId)
        {
            return objPostDatedChequeDAL.GetDetailRecords(PostDatedChequeId);
        }
        
        public DataTable GetAllRecords(string Search)
        {
            return objPostDatedChequeDAL.GetAllRecords(Search);
        }

        public DataTable GetDealer()
        {
            return objPostDatedChequeDAL.GetDealer();
        }

        public bool GetDealerNameExist(int DealerID, string DealerName)
        {
            return objPostDatedChequeDAL.GetDealerNameExist(DealerID, DealerName);
        }

        public DataTable GetMaxRecords()
        {
            return objPostDatedChequeDAL.GetMaxRecords();
        }

        public void InsertMasterRecord(ArrayList inputArrayList)
        {
            objPostDatedChequeDAL.ID = Convert.ToInt32(inputArrayList[0]);
            objPostDatedChequeDAL.Dealer = Convert.ToInt32(inputArrayList[1]);
            objPostDatedChequeDAL.SubmitDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[2].ToString());
            objPostDatedChequeDAL.Comments = inputArrayList[3].ToString();
            objPostDatedChequeDAL.InsertMasterRecord(objPostDatedChequeDAL);
        }

        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objPostDatedChequeDAL.ID = Convert.ToInt32(inputArrayList[0]);
            objPostDatedChequeDAL.Dealer = Convert.ToInt32(inputArrayList[1]);
            objPostDatedChequeDAL.SubmitDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[2].ToString());
            objPostDatedChequeDAL.Comments = inputArrayList[3].ToString();
            objPostDatedChequeDAL.UpdateMasterRecord(objPostDatedChequeDAL);
        }

        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objPostDatedChequeDAL.PostDatedChequeId = Convert.ToInt32(inputArrayList[0]);
            objPostDatedChequeDAL.ChequeDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objPostDatedChequeDAL.BankName = inputArrayList[2].ToString();
            objPostDatedChequeDAL.BranchName = inputArrayList[3].ToString();
            objPostDatedChequeDAL.ChequeNo = inputArrayList[4].ToString();
            objPostDatedChequeDAL.Amount = Convert.ToDecimal(inputArrayList[5]);
            objPostDatedChequeDAL.InsertDetailRecord(objPostDatedChequeDAL);
        }
        
        public void DeleteMasterDetailRecord(int ID)
        {
            objPostDatedChequeDAL.DeleteMasterDetailRecord(ID);
        }

        public void DeleteDetailRecord(int ID)
        {
            objPostDatedChequeDAL.DeleteDetailRecord(ID);
        }


        #endregion
    }
}
