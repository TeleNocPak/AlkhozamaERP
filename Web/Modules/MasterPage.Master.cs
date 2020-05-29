using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ERP.Utilities;

namespace ERPWeb
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private ERPApplicationHelper objAppLicationHelper = new ERPApplicationHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["UserId"] = null;
                Response.Redirect("~/Default.aspx");
            }

            ltrCompanyName.Text = Session["CompanyName"].ToString();
            ltrBranchName.Text = Session["LocationName"].ToString();

            ltrUserName.Text = Session["UserName"].ToString();
           // ltrLocationName.Text = Session["LocationName"].ToString();

        }
    }
}
