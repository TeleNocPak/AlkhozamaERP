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
    public partial class Company : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        CompanyBR objCompanyBR = new CompanyBR();

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
                    else if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" && int.TryParse(Request.QueryString["ID"].ToString(), out value))
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
                        Response.Redirect("CompanyList.aspx");
                }
                else
                    Response.Redirect("CompanyList.aspx");
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
            Response.Redirect("CompanyList.aspx");
        }

        #endregion

        #region Methods

        private void GetData(int ID)
        {
            DataTable dt = objCompanyBR.GetRecord(ID);
            if (dt.Rows.Count > 0)
            {
                txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                txtContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtPOBox.Text = dt.Rows[0]["POBox"].ToString();
                txtPostalCode.Text = dt.Rows[0]["PostalCode"].ToString();
                txtContactPhone.Text = dt.Rows[0]["ContactPhone"].ToString();
                txtContactMobile.Text = dt.Rows[0]["ContactMobile"].ToString();
                txtContactFaxNo.Text = dt.Rows[0]["ContactFaxNo"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtWebSite.Text = dt.Rows[0]["WebSite"].ToString();
            }
            else
                Response.Redirect("CompanyList.aspx");
        }
       
        private void ClearControl()
        {
            txtCompanyName.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPOBox.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtContactPhone.Text = string.Empty;
            txtContactMobile.Text = string.Empty;
            txtContactFaxNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtWebSite.Text = string.Empty;

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
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 25);
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
            bool isExist = CommonMethodsBR.RecordAlredayExist(Convert.ToInt32(HiddenFieldID.Value), txtCompanyName.Text.Trim(), "CompanyID", "CompanyName", "Company");
            if (isExist)
            {
                ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordAlready);
                lblMessage.Text = ErrorMessage;
                lblMessage.CssClass = "WarningText";
                return;
            }

            ArrayList inputArrList = new ArrayList();
            try
            {
                if (HiddenFieldMode.Value == "New")
                {
                    inputArrList.Add(txtCompanyName.Text.Trim().ToUpper());
                    inputArrList.Add(txtContactPerson.Text.Trim().ToUpper());
                    inputArrList.Add(txtAddress.Text.Trim().ToUpper());
                    inputArrList.Add(txtPOBox.Text.Trim().ToUpper());
                    inputArrList.Add(txtPostalCode.Text.Trim().ToUpper());
                    inputArrList.Add(txtContactPhone.Text.Trim().ToUpper());
                    inputArrList.Add(txtContactMobile.Text.Trim().ToUpper());
                    inputArrList.Add(txtContactFaxNo.Text.Trim().ToUpper());
                    inputArrList.Add(txtEmail.Text.Trim().ToUpper());
                    inputArrList.Add(txtWebSite.Text.Trim().ToUpper());
                    objCompanyBR.InsertRecord(inputArrList);
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    inputArrList.Add(HiddenFieldID.Value);
                    inputArrList.Add(txtCompanyName.Text.Trim().ToUpper());
                    inputArrList.Add(txtContactPerson.Text.Trim().ToUpper());
                    inputArrList.Add(txtAddress.Text.Trim().ToUpper());                    
                    inputArrList.Add(txtPOBox.Text.Trim().ToUpper());
                    inputArrList.Add(txtPostalCode.Text.Trim().ToUpper());
                    inputArrList.Add(txtContactPhone.Text.Trim().ToUpper());
                    inputArrList.Add(txtContactMobile.Text.Trim().ToUpper());
                    inputArrList.Add(txtContactFaxNo.Text.Trim().ToUpper());
                    inputArrList.Add(txtEmail.Text.Trim().ToUpper());
                    inputArrList.Add(txtWebSite.Text.Trim().ToUpper());
                    objCompanyBR.UpdateRecord(inputArrList);
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
                    Response.Redirect("CompanyList.aspx");
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
