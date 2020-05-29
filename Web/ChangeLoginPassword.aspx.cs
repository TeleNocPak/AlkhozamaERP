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
using ERP.BusinessRules;

namespace ERPWeb
{
    public partial class ChangeLoginPassword : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        LoginBR objLoginBR = new LoginBR();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["UserId"] = null;
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {          
            objLoginBR.UpdateLoginPassword(Convert.ToInt32(Session["UserId"]), txtNewPassword.Text.Trim());
            Response.Redirect(@"~\Modules\DashBoard\AdminDashBoard.aspx");
        }
    }
}
