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
    public partial class Default : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        LoginBR objLoginBR = new LoginBR();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            DataTable LoginDataTable = objLoginBR.GetLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            if (LoginDataTable.Rows.Count > 0)
            {
                hfPassword.Value = txtPassword.Text.Trim();
                Session["RoleID"] = LoginDataTable.Rows[0]["GroupsID"].ToString();
                Session["UserId"] = LoginDataTable.Rows[0]["UserId"].ToString();
                Session["UserName"] = LoginDataTable.Rows[0]["UserId"].ToString();

                Session["CompanyName"] = LoginDataTable.Rows[0]["CompanyName"].ToString();
                Session["CompanyId"] = LoginDataTable.Rows[0]["CompanyId"].ToString();

                if (!string.IsNullOrEmpty(LoginDataTable.Rows[0]["LocationId"].ToString()))
                {
                    DataTable LocationDataTable = objLoginBR.GetLocations(LoginDataTable.Rows[0]["LocationId"].ToString());
                    if (LocationDataTable.Rows.Count > 0)
                    {
                        if (LocationDataTable.Rows.Count.Equals(1))
                        {
                            Session["LocationId"] = LocationDataTable.Rows[0]["LocationId"].ToString();
                            Session["LocationName"] = LocationDataTable.Rows[0]["LocationName"].ToString();

                            if (txtUserName.Text.Trim().Equals(txtPassword.Text.Trim()))
                                Response.Redirect(@"ChangeLoginPassword.aspx");
                            else
                                Response.Redirect(@"~\Modules\DashBoard\AdminDashBoard.aspx");
                        }
                        else
                        {
                            lstLocation.DataSource = LocationDataTable;
                            lstLocation.DataTextField = "LocationName";
                            lstLocation.DataValueField = "LocationId";
                            lstLocation.DataBind();
                            lstLocation.SelectedIndex = 0;
                            Location_ModalPopupExtender.Show();
                        }
                    }
                    else
                        lblMessage.Text = "Location cannot exist ....";
                }
                else
                    lblMessage.Text = "Location cannot exist ....";
            }
            else
            {
                Session["UserId"] = null;
                lblMessage.Text = "Invalid Login Name or Password ....";
            }
        }

        protected void btnLocationLogin_Click(object sender, EventArgs e)
        {
            Session["LocationId"] = lstLocation.SelectedValue;
            Session["LocationName"] = lstLocation.SelectedItem.Text;

            if (txtUserName.Text.Trim().Equals(hfPassword.Value))
                Response.Redirect(@"ChangeLoginPassword.aspx");
            else
                Response.Redirect(@"~\Modules\DashBoard\AdminDashBoard.aspx");
        }
    }
}
