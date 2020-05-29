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
    public partial class ChangePassword : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        LoginBR objLoginBR = new LoginBR();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Boolean isTrue = objLoginBR.PasswordConfirmation(Convert.ToInt32(Session["UserId"]), txtOldPassword.Text.Trim());

            if (!isTrue)
            {
                lblMessage.Text = "Old Password is incorrect";
                lblMessage.CssClass = "WarningText";
                return;
            }
          
            objLoginBR.UpdateLoginPassword(Convert.ToInt32(Session["UserId"]), txtNewPassword.Text.Trim());
            ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordUpdate);
            lblMessage.Text = ErrorMessage;
            lblMessage.CssClass = "ConfirmText";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetupsMenu.aspx");
        }
    }
}
