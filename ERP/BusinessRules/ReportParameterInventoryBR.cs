using System;
using System.Data;
using System.Collections;
using ERP.DAL;
using ERP.Utilities;

namespace ERP.BusinessRules
{
    public class ReportParameterInventoryBR
    {
        #region Variables

        ReportParameterInventoryDAL objReportParameterInventoryDAL = new ReportParameterInventoryDAL();

        #endregion

        #region Methods
        
        public DataTable GetDeliveryPoint()
        {
            return objReportParameterInventoryDAL.GetDeliveryPoint();
        }

        public DataTable GetWareHouse(int BranchId)
        {
            return objReportParameterInventoryDAL.GetWareHouse(BranchId);
        }

        #endregion
    }
}
