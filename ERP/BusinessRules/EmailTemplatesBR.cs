using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class EmailTemplatesBR
    {
        #region Variables

        EmailTemplatesDAL objEmailTemplatesDAL = new EmailTemplatesDAL();

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            return objEmailTemplatesDAL.GetRecord(ID);
        }
        public DataTable GetAllRecords(string Search)
        {
            return objEmailTemplatesDAL.GetAllRecords(Search);
        }

        public void InsertRecord(ArrayList inputArrayList)
        {
            objEmailTemplatesDAL.TemplateName = inputArrayList[0].ToString();
            objEmailTemplatesDAL.Subject = inputArrayList[1].ToString();
            objEmailTemplatesDAL.EmailBody = inputArrayList[2].ToString();
            objEmailTemplatesDAL.Active =Convert.ToInt16(inputArrayList[3]);
            
            objEmailTemplatesDAL.InsertRecord(objEmailTemplatesDAL);
        }
        public void UpdateRecord(ArrayList inputArrayList)
        {
            objEmailTemplatesDAL.TemplateId = Convert.ToInt32(inputArrayList[0].ToString());
            objEmailTemplatesDAL.TemplateName = inputArrayList[1].ToString();
            objEmailTemplatesDAL.Subject = inputArrayList[2].ToString();
            objEmailTemplatesDAL.EmailBody = inputArrayList[3].ToString();
            objEmailTemplatesDAL.Active = Convert.ToInt16(inputArrayList[4]);
            
            objEmailTemplatesDAL.UpdateRecord(objEmailTemplatesDAL);
        }
        public void DeleteRecord(int ID)
        {
            objEmailTemplatesDAL.DeleteRecord(ID);
        }

        #endregion
    }
}
