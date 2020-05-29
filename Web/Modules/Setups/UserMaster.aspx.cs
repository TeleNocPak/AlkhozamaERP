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
    public partial class UserMaster : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        UserMasterBR objUserMasterBR = new UserMasterBR();

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyUserRoles();
            //txtAppointmentDate.Attributes.Add("readonly", "true");
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
                        Response.Redirect("UserMasterList.aspx");
                }
                else
                    Response.Redirect("UserMasterList.aspx");
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
            Response.Redirect("UserMasterList.aspx");
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLocation(Convert.ToInt32(ddlBranch.SelectedValue));
        }

        #endregion

        #region Methods

        private void GetData(int ID)
        {
            DataTable dt = objUserMasterBR.GetRecord(ID);
            if (dt.Rows.Count > 0)
            {
                txtLoginID.Text = dt.Rows[0]["LoginID"].ToString();
                txtName.Text = dt.Rows[0]["FullName"].ToString();
                ddlRole.SelectedValue = dt.Rows[0]["RoleID"].ToString();
                chkAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["Admin"]);
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["Active"]);

                txtQualification.Text = dt.Rows[0]["Qualification"].ToString();
                txtReference.Text = dt.Rows[0]["Reference"].ToString();
                txtEmergency.Text = dt.Rows[0]["EmergencyNo"].ToString();
                if (dt.Rows[0]["AppointmentDate"] != null && Convert.ToDateTime(dt.Rows[0]["AppointmentDate"]).ToString("dd-MM-yyyy") != "01-01-1900")
                    txtAppointmentDate.Text = Convert.ToDateTime(dt.Rows[0]["AppointmentDate"]).ToString("dd/MM/yyyy");
                txtOther.Text = dt.Rows[0]["Others"].ToString();
                ddlBranch.SelectedValue = dt.Rows[0]["BranchId"].ToString();

                GetLocation(Convert.ToInt32(ddlBranch.SelectedValue));

                string[] LocationId = dt.Rows[0]["LocationId"].ToString().Trim().Split(',');

                for (int count = 0; count < LocationId.Length; count++)
                {
                    foreach (ListItem li in lstLocation.Items)
                    {
                        if (li.Value.Equals(LocationId[count]))
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                    //if(lstLocation.Items[count].Value.Equals(LocationId[count]));
                    //    lstLocation.Items[count].Selected = true;
                }
            }
            else
                Response.Redirect("UserMasterList.aspx");
        }
        private void GetSetupsData()
        {
            DataTable dataTable = objUserMasterBR.GetRoles();
            DataRow dr = dataTable.NewRow();
            dr["ID"] = 0;
            dr["RoleName"] = "Select a Role";
            dataTable.Rows.InsertAt(dr, 0);

            ddlRole.DataSource = dataTable;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();

            dataTable = objUserMasterBR.GetBranch();
            dr = dataTable.NewRow();
            dr["BranchID"] = 0;
            dr["BranchName"] = "Select a Branch";
            dataTable.Rows.InsertAt(dr, 0);

            ddlBranch.DataSource = dataTable;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
        }

        private void GetLocation(int BranchId)
        {
            lstLocation.DataSource = objUserMasterBR.GetLocation(BranchId);
            lstLocation.DataTextField = "LocationName";
            lstLocation.DataValueField = "LocationId";
            lstLocation.DataBind();
        }

        private void ClearControl()
        {
            txtLoginID.Text = string.Empty;
            txtName.Text = string.Empty;
            ddlRole.SelectedValue = "0";
            ddlBranch.SelectedValue = "0";
            chkAdmin.Checked = false;
            txtPhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            chkActive.Checked = false;
            txtAddress.Text = string.Empty;

            txtQualification.Text = string.Empty;
            txtReference.Text = string.Empty;
            txtEmergency.Text = string.Empty;
            txtAppointmentDate.Text = string.Empty;
            txtOther.Text = string.Empty;

            btnSave.Text = "Save";
            btnSaveNew.Text = "Save And New";
            HiddenFieldMode.Value = "New";
            HiddenFieldID.Value = "0";

            lstLocation.ClearSelection();
        }

        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 2);
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
            if (Page.IsValid)
            {

                ArrayList inputArrList = new ArrayList();
                try
                {
                    bool InvalidLst = false;
                    string LocationId = string.Empty;

                    if (txtLoginID.Text.Trim() == string.Empty)
                    {
                        ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.LoginID);
                        lblMessage.Text = ErrorMessage;
                        lblMessage.CssClass = "WarningText";
                        txtName.Focus();
                        return;
                    }

                    DataTable ExistDataTable = objUserMasterBR.LoginIDExist(Convert.ToInt32(HiddenFieldID.Value), txtLoginID.Text.Trim());
                    if (ExistDataTable.Rows.Count > 0)
                    {
                        ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.LoginIDAlready);
                        lblMessage.Text = ErrorMessage;
                        lblMessage.CssClass = "WarningText";
                        txtName.Focus();
                        return;
                    }

                    foreach (ListItem li in lstLocation.Items)
                    {
                        if (li.Selected)
                        {
                            InvalidLst = true;
                            break;
                        }
                    }

                    if (!InvalidLst)
                    {
                        lblMessage.Text = "Before Saving, There should be at least one record selected in Location List.";
                        lblMessage.CssClass = "WarningText";
                        txtName.Focus();
                        return;
                    }

                    foreach (ListItem li in lstLocation.Items)
                    {
                        if (li.Selected)
                        {
                            if (LocationId.Length > 0)
                                LocationId += ",";
                            LocationId += li.Value;
                        }
                    }

                    string AppointmentDate = txtAppointmentDate.Text.Trim() != "" ? txtAppointmentDate.Text.Trim() : "01/01/1900";

                    if (HiddenFieldMode.Value == "New")
                    {
                        inputArrList.Add(txtLoginID.Text.Trim().ToUpper());
                        inputArrList.Add(txtLoginID.Text.Trim().ToUpper());
                        inputArrList.Add(txtName.Text.Trim().ToUpper());

                        if (!txtPhone.Text.Trim().Equals("___-________"))
                            inputArrList.Add(txtPhone.Text.Trim().ToUpper());
                        else
                            inputArrList.Add(string.Empty);

                        if (!txtMobile.Text.Trim().Equals("___-________"))
                            inputArrList.Add(txtMobile.Text.Trim().ToUpper());
                        else
                            inputArrList.Add(string.Empty);

                        inputArrList.Add(txtEmail.Text.Trim().ToUpper());
                        inputArrList.Add(txtAddress.Text.Trim().ToUpper());
                        inputArrList.Add(chkAdmin.Checked);
                        inputArrList.Add(ddlRole.SelectedValue);
                        inputArrList.Add(chkActive.Checked);

                        inputArrList.Add(txtQualification.Text.Trim().ToUpper());
                        inputArrList.Add(txtReference.Text.Trim().ToUpper());
                        inputArrList.Add(txtEmergency.Text.Trim().ToUpper());
                        inputArrList.Add(AppointmentDate);

                        inputArrList.Add(txtOther.Text.Trim().ToUpper());
                        inputArrList.Add(ddlBranch.SelectedValue);
                        inputArrList.Add(LocationId);

                        objUserMasterBR.InsertRecord(inputArrList);
                    }
                    else if (HiddenFieldMode.Value == "Edit")
                    {
                        inputArrList.Add(HiddenFieldID.Value);
                        inputArrList.Add(txtLoginID.Text.Trim().ToUpper());

                        inputArrList.Add(txtName.Text.Trim().ToUpper());

                        if (!txtPhone.Text.Trim().Equals("___-________"))
                            inputArrList.Add(txtPhone.Text.Trim().ToUpper());
                        else
                            inputArrList.Add(string.Empty);

                        if (!txtMobile.Text.Trim().Equals("___-________"))
                            inputArrList.Add(txtMobile.Text.Trim().ToUpper());
                        else
                            inputArrList.Add(string.Empty);

                        inputArrList.Add(txtEmail.Text.Trim().ToUpper());
                        inputArrList.Add(txtAddress.Text.Trim().ToUpper());
                        inputArrList.Add(chkAdmin.Checked);
                        inputArrList.Add(ddlRole.SelectedValue);
                        inputArrList.Add(chkActive.Checked);

                        inputArrList.Add(txtQualification.Text.Trim().ToUpper());
                        inputArrList.Add(txtReference.Text.Trim().ToUpper());
                        inputArrList.Add(txtEmergency.Text.Trim().ToUpper());

                        inputArrList.Add(AppointmentDate);

                        inputArrList.Add(txtOther.Text.Trim().ToUpper());
                        inputArrList.Add(ddlBranch.SelectedValue);
                        inputArrList.Add(LocationId);

                        objUserMasterBR.UpdateRecord(inputArrList);
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

                    if (isSave == 0)
                        Response.Redirect("UserMasterList.aspx");

                    ClearControl();
                }
                catch (Exception exe)
                {
                    lblMessage.Text = exe.Message;
                    lblMessage.CssClass = "WarningText";
                }
            }
        }
        #endregion
    }
}
