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
    public partial class UserRoles : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        UserRoleBR objUserRoleBR = new UserRoleBR();

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
                        HiddenFieldID.Value = "0";
                        GetFormReportData(Convert.ToInt32(HiddenFieldID.Value));
                        btnSave.Text = "Save";
                        HiddenFieldMode.Value = "New";
                    }
                    else if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" && int.TryParse(Request.QueryString["ID"].ToString(), out value))
                    {
                        HiddenFieldID.Value = Request.QueryString["ID"].ToString();
                        GetData(Convert.ToInt32(HiddenFieldID.Value));
                        GetFormReportData(Convert.ToInt32(HiddenFieldID.Value));
                        btnSave.Text = "Update";
                        HiddenFieldMode.Value = "Edit";
                        if (Request.QueryString["Mode"] == "View") btnSave.Visible = false;
                    }
                    else
                        Response.Redirect("UserRolesList.aspx");
                }
                else
                    Response.Redirect("UserRolesList.aspx");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserRolesList.aspx");
        }

        #endregion

        #region Methods

        private void GetData(int ID)
        {
            DataTable dt = objUserRoleBR.GetMasterRecord(ID);
            if (dt.Rows.Count > 0)
            {
                txtRoleName.Text = dt.Rows[0]["RoleName"].ToString();
                chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["Active"]);
            }
            else
                Response.Redirect("UserRolesList.aspx");
        }

        private void GetFormReportData(int RoleID)
        {
            FormGridView.DataSource = objUserRoleBR.GetDetailRecords(RoleID, 0);
            FormGridView.DataBind();
            ReportGridView.DataSource = objUserRoleBR.GetDetailRecords(RoleID, 1);
            ReportGridView.DataBind();
        }

        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 4);
                if (dt.Rows.Count > 0)
                {
                    btnSave.Visible = Convert.ToBoolean(dt.Rows[0]["AllowAdd"]);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        private void InsertData()
        {
            ArrayList inputArrList = new ArrayList();
            try
            {
                if (txtRoleName.Text.Trim() == string.Empty)
                {
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RoleName);
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    return;
                }

                bool isExist = CommonMethodsBR.RecordAlredayExist(Convert.ToInt32(HiddenFieldID.Value), txtRoleName.Text.Trim(), "ID", "RoleName", "rolemaster");
                if (isExist)
                {
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordAlready);
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    return;
                }

                if (HiddenFieldMode.Value == "New")
                {
                    int RoleID = 0;
                    DataTable MaxCodeDataTable = objUserRoleBR.GetMaxRecords();
                    if (Convert.ToInt32(MaxCodeDataTable.Rows[0]["RoleID"]) != 0)
                        RoleID = Convert.ToInt32(MaxCodeDataTable.Rows[0]["RoleID"]) + 1;
                    else
                        RoleID = 1;

                    objUserRoleBR.InsertRoleMasterRecord(RoleID, txtRoleName.Text.Trim().ToUpper(), Convert.ToInt16(chkActive.Checked));

                    foreach (GridViewRow gRow in FormGridView.Rows)
                    {
                        inputArrList.Clear();

                        bool chkFAllowAdd = ((CheckBox)gRow.Controls[0].FindControl("chkFAllowAdd")).Checked;
                        bool chkFAllowEdit = ((CheckBox)gRow.Controls[0].FindControl("chkFAllowEdit")).Checked;
                        bool chkFAllowDelete = ((CheckBox)gRow.Controls[0].FindControl("chkFAllowDelete")).Checked;
                        bool chkFAllowView = ((CheckBox)gRow.Controls[0].FindControl("chkFAllowView")).Checked;
                        string hfFormId = ((HiddenField)gRow.Controls[0].FindControl("hfFormId")).Value;

                        inputArrList.Add(RoleID);
                        inputArrList.Add(hfFormId);
                        inputArrList.Add(chkFAllowAdd);
                        inputArrList.Add(chkFAllowEdit);
                        inputArrList.Add(chkFAllowDelete);
                        inputArrList.Add(chkFAllowView);
                        inputArrList.Add(0);
                        objUserRoleBR.InsertRoleDetailRecord(inputArrList);
                    }

                    foreach (GridViewRow gRow in ReportGridView.Rows)
                    {
                        inputArrList.Clear();

                        bool chkRAllowAdd = ((CheckBox)gRow.Controls[0].FindControl("chkRAllowAdd")).Checked;
                        bool chkRAllowEdit = ((CheckBox)gRow.Controls[0].FindControl("chkRAllowEdit")).Checked;
                        bool chkRAllowDelete = ((CheckBox)gRow.Controls[0].FindControl("chkRAllowDelete")).Checked;
                        bool chkRAllowView = ((CheckBox)gRow.Controls[0].FindControl("chkRAllowView")).Checked;
                        string hfFormId = ((HiddenField)gRow.Controls[0].FindControl("hfFormId")).Value;

                        inputArrList.Add(RoleID);
                        inputArrList.Add(hfFormId);
                        inputArrList.Add(chkRAllowAdd);
                        inputArrList.Add(chkRAllowEdit);
                        inputArrList.Add(chkRAllowDelete);
                        inputArrList.Add(chkRAllowView);
                        inputArrList.Add(1);
                        objUserRoleBR.InsertRoleDetailRecord(inputArrList);
                    }
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    objUserRoleBR.UpdateRoleMasterRecord(Convert.ToInt32(HiddenFieldID.Value), txtRoleName.Text.Trim().ToUpper(), Convert.ToInt16(chkActive.Checked));
                    foreach (GridViewRow gRow in FormGridView.Rows)
                    {
                        inputArrList.Clear();

                        bool chkFAllowAdd = ((CheckBox)gRow.Controls[0].FindControl("chkFAllowAdd")).Checked;
                        bool chkFAllowEdit = ((CheckBox)gRow.Controls[0].FindControl("chkFAllowEdit")).Checked;
                        bool chkFAllowDelete = ((CheckBox)gRow.Controls[0].FindControl("chkFAllowDelete")).Checked;
                        bool chkFAllowView = ((CheckBox)gRow.Controls[0].FindControl("chkFAllowView")).Checked;
                        string hfFormId = ((HiddenField)gRow.Controls[0].FindControl("hfFormId")).Value;
                        string hfRoleDetailId = ((HiddenField)gRow.Controls[0].FindControl("hfRoleDetailId")).Value;

                        if (hfRoleDetailId.Equals("0"))
                        {
                            inputArrList.Add(HiddenFieldID.Value);
                            inputArrList.Add(hfFormId);
                            inputArrList.Add(chkFAllowAdd);
                            inputArrList.Add(chkFAllowEdit);
                            inputArrList.Add(chkFAllowDelete);
                            inputArrList.Add(chkFAllowView);
                            inputArrList.Add(0);
                            objUserRoleBR.InsertRoleDetailRecord(inputArrList);
                        }
                        else
                        {
                            inputArrList.Add(hfRoleDetailId);
                            inputArrList.Add(HiddenFieldID.Value);
                            inputArrList.Add(hfFormId);
                            inputArrList.Add(chkFAllowAdd);
                            inputArrList.Add(chkFAllowEdit);
                            inputArrList.Add(chkFAllowDelete);
                            inputArrList.Add(chkFAllowView);
                            inputArrList.Add(0);
                            objUserRoleBR.UpdateRoleDetailRecord(inputArrList);
                        }
                    }

                    foreach (GridViewRow gRow in ReportGridView.Rows)
                    {
                        inputArrList.Clear();

                        bool chkRAllowAdd = ((CheckBox)gRow.Controls[0].FindControl("chkRAllowAdd")).Checked;
                        bool chkRAllowEdit = ((CheckBox)gRow.Controls[0].FindControl("chkRAllowEdit")).Checked;
                        bool chkRAllowDelete = ((CheckBox)gRow.Controls[0].FindControl("chkRAllowDelete")).Checked;
                        bool chkRAllowView = ((CheckBox)gRow.Controls[0].FindControl("chkRAllowView")).Checked;
                        string hfFormId = ((HiddenField)gRow.Controls[0].FindControl("hfFormId")).Value;
                        string hfRoleDetailId = ((HiddenField)gRow.Controls[0].FindControl("hfRoleDetailId")).Value;

                        if (hfRoleDetailId.Equals("0"))
                        {
                            inputArrList.Add(HiddenFieldID.Value);
                            inputArrList.Add(hfFormId);
                            inputArrList.Add(chkRAllowAdd);
                            inputArrList.Add(chkRAllowEdit);
                            inputArrList.Add(chkRAllowDelete);
                            inputArrList.Add(chkRAllowView);
                            inputArrList.Add(1);
                            objUserRoleBR.InsertRoleDetailRecord(inputArrList);
                        }
                        else
                        {
                            inputArrList.Add(hfRoleDetailId);
                            inputArrList.Add(HiddenFieldID.Value);
                            inputArrList.Add(hfFormId);
                            inputArrList.Add(chkRAllowAdd);
                            inputArrList.Add(chkRAllowEdit);
                            inputArrList.Add(chkRAllowDelete);
                            inputArrList.Add(chkRAllowView);
                            inputArrList.Add(1);
                            objUserRoleBR.UpdateRoleDetailRecord(inputArrList);
                        }
                    }
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
                Response.Redirect("UserRolesList.aspx");
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
