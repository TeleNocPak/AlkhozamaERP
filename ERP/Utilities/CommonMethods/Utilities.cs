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

public class Utilities
{
    public Utilities()
    {

    }
    public static string ENGBtoENUS(string Date)
    {
        return Convert.ToDateTime(Date).ToShortDateString();
    }
    public static string GetCurrentDate
    {
        get
        {
            DateTime dt = Convert.ToDateTime(DateTime.Now, new CultureInfo("en-US", true));
            //return dt.ToString("dd/MM/yyyy", new CultureInfo("en-GB", true));
            return dt.ToString();
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
    public static String GetUserId()
    {
        String _UserId = String.Empty;
        if (HttpContext.Current.Session["UserId"] != null)
        {
            _UserId = HttpContext.Current.Session["UserId"].ToString();
        }
        else
        {
            _UserId = null;
        }
        return _UserId;
    }
}
