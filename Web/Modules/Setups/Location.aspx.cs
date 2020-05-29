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
    public partial class Location : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        LocationBR objLocationBR = new LocationBR();

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
                    GetSetupsData();
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
                        Response.Redirect("LocationList.aspx");
                }
                else
                    Response.Redirect("LocationList.aspx");
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
            Response.Redirect("LocationList.aspx");
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBranch(Convert.ToInt32(ddlCompany.SelectedValue));
        }

        #endregion

        #region Methods

        private void GetData(int ID)
        {
            DataTable dt = objLocationBR.GetRecord(ID);
            if (dt.Rows.Count > 0)
            {
                txtLocationName.Text = dt.Rows[0]["LocationName"].ToString();
                ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                GetBranch(Convert.ToInt32(ddlCompany.SelectedValue));
                ddlBranch.SelectedValue = dt.Rows[0]["BranchId"].ToString();
            }
            else
                Response.Redirect("LocationList.aspx");
        }

        private void GetSetupsData()
        {
            DataTable dataTable = objLocationBR.GetCompany();
            DataRow dr = dataTable.NewRow();

            dr["CompanyId"] = 0;
            dr["CompanyName"] = "Select a Company";
            dataTable.Rows.InsertAt(dr, 0);

            ddlCompany.DataSource = dataTable;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }

        private void ClearControl()
        {
            txtLocationName.Text = string.Empty;
            ddlCompany.SelectedValue = "0";
            ddlBranch.SelectedValue = "0";

            btnSave.Text = "Save";
            btnSaveNew.Text = "Save And New";
            HiddenFieldMode.Value = "New";
            HiddenFieldID.Value = "0";
        }

        private void GetBranch(int CompanyId)
        {
            DataTable dataTable = objLocationBR.GetBranch(CompanyId);
            DataRow dr = dataTable.NewRow();
            dr["BranchId"] = 0;
            dr["BranchName"] = "Select a Branch";
            dataTable.Rows.InsertAt(dr, 0);

            ddlBranch.DataSource = dataTable;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
        }

        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 29);
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
                bool isExist = CommonMethodsBR.RecordAlredayExist(Convert.ToInt32(HiddenFieldID.Value), txtLocationName.Text.Trim(), "LocationID", "LocationName", "Location");
                if (isExist)
                {
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordAlready);
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    return;
                }
                if (HiddenFieldMode.Value == "New")
                {
                    objLocationBR.InsertRecord(txtLocationName.Text.Trim().ToUpper(),Convert.ToInt32(ddlBranch.SelectedValue));                    
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    objLocationBR.UpdateRecord(Convert.ToInt32(HiddenFieldID.Value), txtLocationName.Text.Trim().ToUpper(), Convert.ToInt32(ddlBranch.SelectedValue));
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
                    Response.Redirect("LocationList.aspx");
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
