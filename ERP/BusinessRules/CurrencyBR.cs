using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class CurrencyBR
    {
        #region Variables

        CurrencyDAL objCurrencyDAL = new CurrencyDAL();

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            return objCurrencyDAL.GetRecord(ID);
        }
        public DataTable GetAllRecords(string Search)
        {
            return objCurrencyDAL.GetAllRecords(Search);
        }

        public DataTable LoginIDExist(int UserID, string LoginID)
        {
            return objCurrencyDAL.LoginIDExist(UserID, LoginID);
        }

       
        public void InsertRecord(string CurrencyName)
        {
            objCurrencyDAL.InsertRecord(CurrencyName);
        }
        public void UpdateRecord(int CurrencyID, string CurrencyName)
        {
           
            objCurrencyDAL.UpdateRecord(CurrencyID, CurrencyName);
        }
        public void DeleteRecord(int ID)
        {
            objCurrencyDAL.DeleteRecord(ID);
        }

        #endregion
    }
}
