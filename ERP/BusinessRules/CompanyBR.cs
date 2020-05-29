using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class CompanyBR
    {
        #region Variables

        CompanyDAL objCompanyDAL = new CompanyDAL();

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            return objCompanyDAL.GetRecord(ID);
        }
        public DataTable GetAllRecords(string Search)
        {
            return objCompanyDAL.GetAllRecords(Search);
        }

        public void InsertRecord(ArrayList inputArrayList)
        {
            objCompanyDAL.CompanyName = inputArrayList[0].ToString();
            objCompanyDAL.ContactPerson = inputArrayList[1].ToString();
            objCompanyDAL.Address = inputArrayList[2].ToString();
            objCompanyDAL.POBox = inputArrayList[3].ToString();
            objCompanyDAL.PostalCode = inputArrayList[4].ToString();
            objCompanyDAL.ContactPhone = inputArrayList[5].ToString();
            objCompanyDAL.ContactMobile = inputArrayList[6].ToString();
            objCompanyDAL.ContactFaxNo = inputArrayList[7].ToString();
            objCompanyDAL.Email = inputArrayList[8].ToString();
            objCompanyDAL.WebSite = inputArrayList[9].ToString();
            objCompanyDAL.InsertRecord(objCompanyDAL);
        }
        public void UpdateRecord(ArrayList inputArrayList)
        {
            objCompanyDAL.ID = Convert.ToInt32(inputArrayList[0].ToString());
            objCompanyDAL.CompanyName = inputArrayList[1].ToString();
            objCompanyDAL.ContactPerson = inputArrayList[2].ToString();
            objCompanyDAL.Address = inputArrayList[3].ToString();
            objCompanyDAL.POBox = inputArrayList[4].ToString();
            objCompanyDAL.PostalCode = inputArrayList[5].ToString();
            objCompanyDAL.ContactPhone = inputArrayList[6].ToString();
            objCompanyDAL.ContactMobile = inputArrayList[7].ToString();
            objCompanyDAL.ContactFaxNo = inputArrayList[8].ToString();
            objCompanyDAL.Email = inputArrayList[9].ToString();
            objCompanyDAL.WebSite = inputArrayList[10].ToString();
            objCompanyDAL.UpdateRecord(objCompanyDAL);
        }
        public void DeleteRecord(int ID)
        {
            objCompanyDAL.DeleteRecord(ID);
        }

        #endregion
    }
}
