using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class SubDepartmentsBR
    {
        #region Variables

        SubDepartmentsDAL objSubDepartmentsDAL = new SubDepartmentsDAL();

        #endregion

        #region Methods

        public DataTable GetRecord(int ID)
        {
            return objSubDepartmentsDAL.GetRecord(ID);
        }

        public DataTable GetDepartmentName()
        {
            return objSubDepartmentsDAL.GetDepartmentName();
        }

        public DataTable GetAllRecords(string Search)
        {
            return objSubDepartmentsDAL.GetAllRecords(Search);
        }

        public void InsertRecord(int DepartmentParentId, string DepartmentName)
        {
            objSubDepartmentsDAL.InsertRecord(DepartmentParentId, DepartmentName);
        }

        public void UpdateRecord(int DepartmentId, int DepartmentParentId, string DepartmentName)
        {

            objSubDepartmentsDAL.UpdateRecord(DepartmentId, DepartmentParentId, DepartmentName);
        }

        public void DeleteRecord(int ID)
        {
            objSubDepartmentsDAL.DeleteRecord(ID);
        }

        #endregion
    }
}
