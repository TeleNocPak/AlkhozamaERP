using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class PostDatedChequeReceivedBR
    {
        #region Variables

        PostDatedChequeReceivedDAL objPostDatedChequeReceivedDAL = new PostDatedChequeReceivedDAL();

        #endregion

        #region Methods

        public DataTable GetMasterRecord(int PostDatedChequeId)
        {
            return objPostDatedChequeReceivedDAL.GetMasterRecord(PostDatedChequeId);
        }
        public DataTable GetRecord(int ID)
        {
            return objPostDatedChequeReceivedDAL.GetRecord(ID);
        }
        public DataTable GetDetailRecords(int PostDatedChequeId)
        {
            return objPostDatedChequeReceivedDAL.GetDetailRecords(PostDatedChequeId);
        }
        public DataTable GetAllRecords(string Search)
        {
            return objPostDatedChequeReceivedDAL.GetAllRecords(Search);
        }
        public DataTable GetDealer()
        {
            return objPostDatedChequeReceivedDAL.GetDealer();
        }
        public bool GetDealerNameExist(int DealerID, string DealerName)
        {
            return objPostDatedChequeReceivedDAL.GetDealerNameExist(DealerID, DealerName);
        }
        public DataTable GetMaxRecords()
        {
            return objPostDatedChequeReceivedDAL.GetMaxRecords();
        }
        public void InsertMasterRecord(ArrayList inputArrayList)
        {
            objPostDatedChequeReceivedDAL.ID = Convert.ToInt32(inputArrayList[0]);
            objPostDatedChequeReceivedDAL.Dealer = Convert.ToInt32(inputArrayList[1]);
            objPostDatedChequeReceivedDAL.SubmitDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[2].ToString());
            objPostDatedChequeReceivedDAL.Comments = inputArrayList[3].ToString();
            objPostDatedChequeReceivedDAL.InsertMasterRecord(objPostDatedChequeReceivedDAL);
        }
        public void UpdateMasterRecord(ArrayList inputArrayList)
        {
            objPostDatedChequeReceivedDAL.ID = Convert.ToInt32(inputArrayList[0]);
            objPostDatedChequeReceivedDAL.Dealer = Convert.ToInt32(inputArrayList[1]);
            objPostDatedChequeReceivedDAL.SubmitDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[2].ToString());
            objPostDatedChequeReceivedDAL.Comments = inputArrayList[3].ToString();
            objPostDatedChequeReceivedDAL.UpdateMasterRecord(objPostDatedChequeReceivedDAL);
        }
        public void InsertDetailRecord(ArrayList inputArrayList)
        {
            objPostDatedChequeReceivedDAL.PostDatedChequeId = Convert.ToInt32(inputArrayList[0]);
            objPostDatedChequeReceivedDAL.ChequeDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objPostDatedChequeReceivedDAL.BankName = inputArrayList[2].ToString();
            objPostDatedChequeReceivedDAL.BranchName = inputArrayList[3].ToString();
            objPostDatedChequeReceivedDAL.ChequeNo = inputArrayList[4].ToString();
            objPostDatedChequeReceivedDAL.Amount = Convert.ToDecimal(inputArrayList[5]);
            objPostDatedChequeReceivedDAL.InsertDetailRecord(objPostDatedChequeReceivedDAL);
        }
        public void DeleteMasterDetailRecord(int ID)
        {
            objPostDatedChequeReceivedDAL.DeleteMasterDetailRecord(ID);
        }
        public void DeleteDetailRecord(int ID)
        {
            objPostDatedChequeReceivedDAL.DeleteDetailRecord(ID);
        }
        public void DeleteRecord(int ID)
        {
            objPostDatedChequeReceivedDAL.DeleteRecord(ID);
        }
        public void UpdateAccountStatus(int ID, bool ChkAgnt)
        {
            objPostDatedChequeReceivedDAL.UpdateAccountStatus(ID, ChkAgnt);
        }
        public void UpdateAccontPaid(ArrayList inputArrayList)
        {
            objPostDatedChequeReceivedDAL.PostDatedChequeId = Convert.ToInt32(inputArrayList[0]);
            objPostDatedChequeReceivedDAL.PaidDate = CommonObjects.ConvertMMDDYYYY(inputArrayList[1].ToString());
            objPostDatedChequeReceivedDAL.PaidBankId = Convert.ToInt32(inputArrayList[2]);            
            objPostDatedChequeReceivedDAL.DepositSlipNo = inputArrayList[3].ToString();
            objPostDatedChequeReceivedDAL.Narration = inputArrayList[4].ToString();

            objPostDatedChequeReceivedDAL.UpdateAccontPaid(objPostDatedChequeReceivedDAL);
        }
        public DataTable GetBankName()
        {
            return objPostDatedChequeReceivedDAL.GetBankName();
        }
        #endregion
    }
}
