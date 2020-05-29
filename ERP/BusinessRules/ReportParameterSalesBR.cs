using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class ReportParameterSalesBR
    {
        #region Variables

        ReportParameterSalesDAL objReportParameterSalesDAL = new ReportParameterSalesDAL();

        #endregion

        #region Methods

        public DataTable GetDealerType()
        {
            return objReportParameterSalesDAL.GetDealerType();
        }

        public DataTable GetDealerSignBoard()
        {
            return objReportParameterSalesDAL.GetDealerSignBoard();
        }

        public DataTable GetSuplierIncentive()
        {
            return objReportParameterSalesDAL.GetSuplierIncentive();
        }

        public DataTable GetDealer()
        {
            return objReportParameterSalesDAL.GetDealer();
        }

        public DataTable GetSalesMan()
        {
            return objReportParameterSalesDAL.GetSalesMan();
        }

        public DataTable GetBank()
        {
            return objReportParameterSalesDAL.GetBank();
        }
       

        #endregion
    }
}
