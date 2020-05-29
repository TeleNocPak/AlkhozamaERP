using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;

namespace ERP.Utilities
{
    public class CommonObjects
    {        
        public CommonObjects()
        {}

        public static string ENGBtoENUS(string Date)
        {
            return Convert.ToDateTime(Date).ToShortDateString();
        }
        public static DateTime GetCurrentDate
        {
            get
            {
                //DateTime dt = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US", true));
                //return dt.ToString("dd/MM/yyyy", new CultureInfo("en-GB", true)); 
                DateTime dt = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                //return Convert.ToDateTime(dt.ToString("dd/MM/yyyy");
                return dt;
            }
        }
        public static bool isDate(string date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetUserId()
        {
            string UserId = String.Empty;
            if (HttpContext.Current.Session["UserId"] != null)
                UserId = HttpContext.Current.Session["UserId"].ToString();
            else
                UserId = null;
            return UserId;
        }

        public static string GetCompanyId()
        {
            string CompanyId = String.Empty;
            if (HttpContext.Current.Session["CompanyId"] != null)
                CompanyId = HttpContext.Current.Session["CompanyId"].ToString();
            else
                CompanyId = null;
            return CompanyId;
        }

        public static string GetBranchId()
        {
            string BranchId = String.Empty;
            if (HttpContext.Current.Session["BranchId"] != null)
                BranchId = HttpContext.Current.Session["BranchId"].ToString();
            else
                BranchId = null;
            return BranchId;
        }

        public static string GetLocationId()
        {
            string LocationId = String.Empty;
            if (HttpContext.Current.Session["LocationId"] != null)
                LocationId = HttpContext.Current.Session["LocationId"].ToString();
            else
                LocationId = null;
            return LocationId;
        }

        public static string GetLocationName()
        {
            string LocationName = String.Empty;
            if (HttpContext.Current.Session["LocationName"] != null)
                LocationName = HttpContext.Current.Session["LocationName"].ToString();
            else
                LocationName = null;
            return LocationName;
        }
        
        public static DateTime ConvertMMDDYYYY(string InDate)
        {
            DateTime OutDate = DateTime.Now;
            try
            {
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                if (InDate != "")
                    OutDate = DateTime.ParseExact(InDate, "dd/MM/yyyy", format);

                return OutDate;
            }
            catch
            {
                throw;
            }
        }

        // set currency format
        public static string parseValueIntoCurrency(string number)
        {
            return string.IsNullOrWhiteSpace(number) ? "" : String.Format("{0:#,##0.00}", Convert.ToDouble(number));
        }

        public static string FormatMaxCode(int Code)
        {
            string NewCode = string.Empty;

            if (Code.ToString().Length.Equals(1))
                NewCode = "000000" + Code.ToString();
            else if (Code.ToString().Length.Equals(2))
                NewCode = "00000" + Code.ToString();
            else if (Code.ToString().Length.Equals(3))
                NewCode = "0000" + Code.ToString();
            else if (Code.ToString().Length.Equals(4))
                NewCode = "000" + Code.ToString();
            else if (Code.ToString().Length.Equals(5))
                NewCode = "00" + Code.ToString();
            else if (Code.ToString().Length.Equals(6))
                NewCode = "0" + Code.ToString();
            else
                NewCode = Code.ToString();

            return NewCode;
        }
    }
}
