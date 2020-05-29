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
using System.Collections.Generic;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace ERPWeb
{
    public partial class ReceiptVoucherSingle : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        ReceiptVoucherBR objReceiptVoucherBR = new ReceiptVoucherBR();
        
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ApplyUserRoles();
                if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"] != "")
                {
                    int value;
                    GetSetupsData();
                    if (Request.QueryString["Mode"] == "New")
                    {
                        btnSave.Text = "Save";
                        HiddenFieldMode.Value = "New";
                        HiddenFieldID.Value = "0";
                        txtVoucherDate.Text = Convert.ToDateTime(DateTime.Today).ToString("dd/MM/yyyy");
                    }
                    else if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "" && int.TryParse(Request.QueryString["ID"].ToString(), out value))
                    {
                        HiddenFieldID.Value = Request.QueryString["ID"].ToString();
                        GetData(Convert.ToInt32(HiddenFieldID.Value));
                        btnPrint.Visible = true;
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
                        Response.Redirect("ReceiptVoucherSingleList.aspx");
                }
                else
                    Response.Redirect("ReceiptVoucherSingleList.aspx");
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
            Response.Redirect("ReceiptVoucherSingleList.aspx");
        }

        protected void txtAccountTitle_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            if (hfAccountAutoCompleted.Value != "0")
            {
                string AccountCode = objReceiptVoucherBR.GetAccountCode(Convert.ToInt32(hfAccountAutoCompleted.Value));
                if (AccountCode != "")
                {
                    txtAccountCode.Text = AccountCode;
                    txtPartyAccountTitle.Focus();
                }
                else
                {
                    ErrorMessage = "Cash / Bank Account does not exist in data base";
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    txtAccountTitle.Focus();
                }
            }
            else
            {
                txtAccountCode.Text = string.Empty;
                txtAccountTitle.Text = string.Empty;
                hfAccountAutoCompleted.Value = "0";
            }
        }

        protected void txtPartyAccountTitle_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            if (hfPartyAccountAutoCompleted.Value != "0")
            {
                string AccountCode = objReceiptVoucherBR.GetAccountCode(Convert.ToInt32(hfPartyAccountAutoCompleted.Value));
                if (AccountCode != "")
                {
                    txtPartyAccountCode.Text = AccountCode;
                    txtAmount.Focus();
                }
                else
                {
                    ErrorMessage = "Party Account does not exist in data base";
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    txtPartyAccountTitle.Focus();
                }
            }
            else
            {
                txtPartyAccountCode.Text = string.Empty;
                txtPartyAccountTitle.Text = string.Empty;
                hfPartyAccountAutoCompleted.Value = "0";
            }
        }

        protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideChequeNoDate(ddlVoucherType.SelectedValue);
        }

        #endregion

        #region Methods

        private void GetData(int ID)
        {
            DataTable dt = objReceiptVoucherBR.GetMasterRecord(ID);
            if (dt.Rows.Count > 0)
            {
                lblVoucherNo.Visible = true;
                lblHeadingVoucherNo.Visible = true;
                lblVoucherNo.Text = dt.Rows[0]["VoucherCode"].ToString();

                ddlVoucherType.SelectedValue = dt.Rows[0]["BookType"].ToString();
                txtVoucherDate.Text = Convert.ToDateTime(dt.Rows[0]["VoucherDate"]).ToString("dd/MM/yyyy");
                txtNarration.Text = dt.Rows[0]["NarrationMaster"].ToString();

                if (ddlVoucherType.SelectedValue.Equals("BR"))
                {
                    HideChequeNoDate(ddlVoucherType.SelectedValue);
                    txtChequeNo.Text = dt.Rows[0]["ChequeNo"].ToString();
                    txtChequeDate.Text = Convert.ToDateTime(dt.Rows[0]["ChequeDate"]).ToString("dd/MM/yyyy");
                }

                //Get Order Detail Records
                DataTable VoucherDetailTable = objReceiptVoucherBR.GetDetailRecords(ID);
                foreach (DataRow drOrderDetail in VoucherDetailTable.Rows)
                {
                    if (drOrderDetail["AccountType"].Equals("D"))                                       // Debit Bank & Cash Account Code
                    {
                        hfAccountAutoCompleted.Value = drOrderDetail["AccountId"].ToString();
                        txtAccountTitle.Text = drOrderDetail["AccountName"].ToString();
                        txtAccountCode.Text = drOrderDetail["AccountCode"].ToString();
                        txtAmount.Text = drOrderDetail["DebitAmount"].ToString();
                    }
                    else
                    {
                        // Credit Party Account Code
                        hfPartyAccountAutoCompleted.Value = drOrderDetail["AccountId"].ToString();
                        txtPartyAccountTitle.Text = drOrderDetail["AccountName"].ToString();
                        txtPartyAccountCode.Text = drOrderDetail["AccountCode"].ToString();
                        
                    }
                }
            }
            else
                Response.Redirect("ReceiptVoucherSingleList.aspx");
        }

        private void GetSetupsData()
        {

        }

        private void ClearControl()
        {
            //ddlVoucherType.SelectedValue = "0";
            lblVoucherNo.Text = string.Empty;
            lblVoucherNo.Visible = true;
            lblHeadingVoucherNo.Visible = true;

            txtNarration.Text = string.Empty;
            txtAccountTitle.Text = string.Empty;
            txtAccountCode.Text = string.Empty;
            hfAccountAutoCompleted.Value = "0";

            txtPartyAccountTitle.Text = string.Empty;
            txtPartyAccountCode.Text = string.Empty;
            hfPartyAccountAutoCompleted.Value = "0";

            txtChequeNo.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            btnPrint.Visible = false;

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
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 45);
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
            ArrayList inputArrList = new ArrayList();
            try
            {
                lblMessage.Text = string.Empty;

                if (!objReceiptVoucherBR.GetAccountNameIdExist(Convert.ToInt32(hfAccountAutoCompleted.Value), txtAccountTitle.Text.Trim()))
                {
                    ErrorMessage = "Cash / Bank Account Title / Code does not exist in data base";
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    ddlVoucherType.Focus();
                    return;
                }

                if (!objReceiptVoucherBR.GetAccountNameIdExist(Convert.ToInt32(hfPartyAccountAutoCompleted.Value), txtPartyAccountTitle.Text.Trim()))
                {
                    ErrorMessage = "Party Account Title / Code does not exist in data base";
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    ddlVoucherType.Focus();
                    return;
                }    

                if (Convert.ToDecimal(txtAmount.Text) <= 0)
                {
                    ErrorMessage = "Amount should be greater then zero";
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    ddlVoucherType.Focus();
                    return;
                }

                if (HiddenFieldMode.Value == "New")
                {
                    inputArrList.Add(txtVoucherDate.Text.Trim());
                    inputArrList.Add(ddlVoucherType.SelectedValue);
                    inputArrList.Add(txtNarration.Text.Trim().ToUpper());
                    inputArrList.Add(txtChequeNo.Text.Trim().ToUpper());
                    inputArrList.Add(txtChequeDate.Text.Trim());
                    int VoucherId = objReceiptVoucherBR.InsertMasterRecord(inputArrList);

                    // Save Debit (Bank & Cash Account Code)
                    inputArrList.Clear();
                    inputArrList.Add(VoucherId);
                    inputArrList.Add(hfAccountAutoCompleted.Value);
                    inputArrList.Add(txtAmount.Text);
                    inputArrList.Add(0);
                    inputArrList.Add("D");
                    objReceiptVoucherBR.InsertDetailRecord(inputArrList);

                    // Save Credit (Party Account Code)
                    inputArrList.Clear();
                    inputArrList.Add(VoucherId);
                    inputArrList.Add(hfPartyAccountAutoCompleted.Value);
                    inputArrList.Add(0);
                    inputArrList.Add(txtAmount.Text);
                    inputArrList.Add("C");
                    objReceiptVoucherBR.InsertDetailRecord(inputArrList);
                    
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordSave);
                    javaScriptCall(ErrorMessage);
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    inputArrList.Add(HiddenFieldID.Value);
                    inputArrList.Add(txtVoucherDate.Text.Trim());
                    inputArrList.Add(ddlVoucherType.SelectedValue);
                    inputArrList.Add(txtNarration.Text.Trim().ToUpper());
                    inputArrList.Add(txtChequeNo.Text.Trim().ToUpper());
                    inputArrList.Add(txtChequeDate.Text.Trim());
                    objReceiptVoucherBR.UpdateMasterRecord(inputArrList);

                    objReceiptVoucherBR.DeleteDetailRecord(Convert.ToInt32(HiddenFieldID.Value));

                    // Save Debit (Bank & Cash Account Code)
                    inputArrList.Clear();
                    inputArrList.Add(HiddenFieldID.Value);
                    inputArrList.Add(hfAccountAutoCompleted.Value);
                    inputArrList.Add(txtAmount.Text);
                    inputArrList.Add(0);
                    inputArrList.Add("D");
                    objReceiptVoucherBR.InsertDetailRecord(inputArrList);

                    // Save Credit (Party Account Code)
                    inputArrList.Clear();
                    inputArrList.Add(HiddenFieldID.Value);
                    inputArrList.Add(hfPartyAccountAutoCompleted.Value);
                    inputArrList.Add(0);
                    inputArrList.Add(txtAmount.Text);
                    inputArrList.Add("C");
                    objReceiptVoucherBR.InsertDetailRecord(inputArrList);

                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordUpdate);
                    javaScriptCall(ErrorMessage);
                }

                if (isSave == 0)
                    Response.Redirect("ReceiptVoucherSingleList.aspx");

                ClearControl();
            }
            catch (Exception exe)
            {
                javaScriptCall(exe.Message);
            }
        }

        private void HideChequeNoDate(string VoucherType)
        {
            if (VoucherType.Equals("CR"))
            {
                trChequeDateHide.Visible = false;
                lblChequeNo.Visible = false;
                txtChequeNo.Visible = false;
                txtChequeDate.Text = string.Empty;
                txtChequeNo.Text = string.Empty;
            }
            else
            {
                trChequeDateHide.Visible = true;
                lblChequeNo.Visible = true;
                txtChequeNo.Visible = true;
            }
        }

        private void javaScriptCall(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('" + message + "');", true);
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCOAList(string prefixText, int count, string contextKey)
        {
            string queryString = @"SELECT Top 20 AccountId,AccountName FROM COA WHERE 
                                   AccountName LIKE '%" + prefixText.Trim().ToUpper() + "%' and AccountBranchId=" + CommonObjects.GetBranchId() + " and AccountId in (select AccountId from COALocation where Locationid=" + CommonObjects.GetLocationId() + ") ";

            List<string> items;
            string m_connectionString = ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString;

            SqlConnection conn = new SqlConnection();
            try
            {
                if (conn != null)
                {
                    conn.Dispose();
                    conn = null;
                }

                conn = new SqlConnection(m_connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, 0, 30, "AccountId");
                items = new List<string>();
                string returnString = String.Empty;

                foreach (DataRow dr in ds.Tables[0].Rows)
                    items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["AccountName"].ToString(), dr["AccountId"].ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return items.ToArray();
        }

        #endregion
    }
}
