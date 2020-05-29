using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class CityBR
    {
        #region Variables

        CityDAL objCityDAL = new CityDAL();

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            return objCityDAL.GetRecord(ID);
        }

        public DataTable GetDepartmentName()
        {
            return objCityDAL.GetCountryName();
        }

        public DataTable GetAllRecords(string Search)
        {
            return objCityDAL.GetAllRecords(Search);
        }

        public void InsertRecord(int CountryParentId, string Name)
        {
            objCityDAL.InsertRecord(CountryParentId, Name);
        }

        public void UpdateRecord(int CountryId, int CountryParentId, string Name)
        {

            objCityDAL.UpdateRecord(CountryId, CountryParentId, Name);
        }

        public void DeleteRecord(int ID)
        {
            objCityDAL.DeleteRecord(ID);
        }

        #endregion
    }
}
