using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class CountryBR
    {
        #region Variables

        CountryDAL objCountryDAL = new CountryDAL();

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            return objCountryDAL.GetRecord(ID);
        }
        public DataTable GetAllRecords(string Search)
        {
            return objCountryDAL.GetAllRecords(Search);
        }
      
        public void InsertRecord(string Name)
        {
            objCountryDAL.InsertRecord(Name);
        }
        public void UpdateRecord(int CountryId, string Name)
        {
           
            objCountryDAL.UpdateRecord(CountryId, Name);
        }
        public void DeleteRecord(int ID)
        {
            objCountryDAL.DeleteRecord(ID);
        }

        #endregion
    }
}
