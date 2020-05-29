using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class ReportParameterSetupsBR
    {
        #region Variables

        ReportParameterSetupsDAL objReportParameterSetupsDAL = new ReportParameterSetupsDAL();

        #endregion

        #region Methods


        public DataTable GetUser()
        {
            return objReportParameterSetupsDAL.GetUser();
        }

        public DataTable GetCompany()
        {
            return objReportParameterSetupsDAL.GetCompany();
        }

        public DataTable GetLocation(int BranchId)
        {
            return objReportParameterSetupsDAL.GetLocation(BranchId);
        }

        public DataTable GetWarehouse()
        {
            return objReportParameterSetupsDAL.GetWarehouse();
        }

        public DataTable GetBranch()
        {
            return objReportParameterSetupsDAL.GetBranch();
        }

        public DataTable GetBranch(int CompanyId)
        {
            return objReportParameterSetupsDAL.GetBranch(CompanyId);
        }

        public DataTable GetLocation()
        {
            return objReportParameterSetupsDAL.GetLocation();
        }

        public DataTable GetCity()
        {
            return objReportParameterSetupsDAL.GetCity();
        }

        public DataTable GetBrand()
        {
            return objReportParameterSetupsDAL.GetBrand();
        }

        public DataTable GetModel()
        {
            return objReportParameterSetupsDAL.GetModel();
        }

        public DataTable GetColor()
        {
            return objReportParameterSetupsDAL.GetColor();
        }

        public DataTable GetCurrency()
        {
            return objReportParameterSetupsDAL.GetCurrency();
        }

        public DataTable GetInsurance()
        {
            return objReportParameterSetupsDAL.GetInsurance();
        }

        public DataTable GetBank()
        {
            return objReportParameterSetupsDAL.GetBank();
        }
        
        public DataTable GetVendors()
        {
            return objReportParameterSetupsDAL.GetVendors();
        }
        
        public DataTable GetPort()
        {
            return objReportParameterSetupsDAL.GetPort();
        }

        public DataTable GetBanks()
        {
            return objReportParameterSetupsDAL.GetBanks();
        }

        public DataTable GetProducts()
        {
            return objReportParameterSetupsDAL.GetProducts();
        }

        public DataTable GetCourierComapny()
        {
            return objReportParameterSetupsDAL.GetCourierComapny();

        }
        
        public DataTable GetZone()
        {
            return objReportParameterSetupsDAL.GetZone();
        }
        #endregion
    }
}
