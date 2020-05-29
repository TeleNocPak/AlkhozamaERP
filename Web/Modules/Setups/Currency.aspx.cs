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
    public partial class Currency : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        CurrencyBR objCurrencyBR = new CurrencyBR();

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyUserRoles();
            if (!IsPostBack)
            {
                if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"] != "")
                {
                    int value;
                    if (Request.QueryString["Mode"] == "New")
                    {
                        btnSave.Text = "Save";
                        HiddenFieldMode.Value = "New";
                        HiddenFieldID.Value = "0";
                    }
                    else if(Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" && int.TryParse(Request.QueryString["ID"].ToString(), out value))
                    {
                        HiddenFieldID.Value = Request.QueryString["ID"].ToString();
                        GetData(Convert.ToInt32(HiddenFieldID.Value));
                        btnSave.Text = "Update";
                        btnSaveNew.Text = "Update & New";
                        HiddenFieldMode.Value = "Edit";
                        if (Request.QueryString["Mode"] == "View")
                        {
                            btnSave.Visible = false;
                            btnSaveNew.Visible = false;
                        }
                    }
                    else
                        Response.Redirect("CurrencyList.aspx");
                }
                else
                    Response.Redirect("CurrencyList.aspx");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertData(0);
        }
        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            InsertData(1);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CurrencyList.aspx");
        }

        #endregion

        #region Methods

        private void GetData(int ID)
        {
            DataTable dt = objCurrencyBR.GetRecord(ID);
            if (dt.Rows.Count > 0)
            {
                txtCurrencyName.Text = dt.Rows[0]["CurrencyName"].ToString();                
            }
            else
                Response.Redirect("CurrencyList.aspx");
        }
        
        private void ClearControl()
        {
            txtCurrencyName.Text = string.Empty;
            btnSave.Text = "Save";
            btnSaveNew.Text = "Save And New";
            HiddenFieldMode.Value = "New";
            HiddenFieldID.Value = "0";
        }

        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 8);
                if (dt.Rows.Count > 0)
                {
                    btnSaveNew.Visible = Convert.ToBoolean(dt.Rows[0]["AllowAdd"]);
                    if (Convert.ToBoolean(dt.Rows[0]["AllowAdd"].Equals(true)) &&
                        Convert.ToBoolean(dt.Rows[0]["AllowEdit"].Equals(true)))
                    {
                        btnSave.Visible = Convert.ToBoolean(dt.Rows[0]["AllowAdd"]);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        private void InsertData(int isSave)
        {
            try
            {
                bool isExist = CommonMethodsBR.RecordAlredayExist(Convert.ToInt32(HiddenFieldID.Value), txtCurrencyName.Text.Trim(), "CurrencyId", "CurrencyName", "currency");
                if (isExist)
                {
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordAlready);
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    return;
                }

                if (HiddenFieldMode.Value == "New")
                {
                    objCurrencyBR.InsertRecord(txtCurrencyName.Text.Trim().ToUpper());
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    objCurrencyBR.UpdateRecord(Convert.ToInt32(HiddenFieldID.Value), txtCurrencyName.Text.Trim().ToUpper());   
                }
                if (HiddenFieldMode.Value == "New")
                {
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordSave);
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "ConfirmText";
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordUpdate);
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "ConfirmText";
                }
                ClearControl();
                if (isSave == 0)
                    Response.Redirect("CurrencyList.aspx");
            }
            catch (Exception exe)
            {
                lblMessage.Text = exe.Message;
                lblMessage.CssClass = "WarningText";
            }
        }
        #endregion
    }
}
