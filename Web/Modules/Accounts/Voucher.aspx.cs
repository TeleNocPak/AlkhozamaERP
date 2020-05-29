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
    public partial class Voucher : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        VoucherBR objVoucherBR = new VoucherBR();
        private DataTable OrderDetailDataTable;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            //txtVoucherDate.Attributes.Add("readonly", "true");
            GetOrderDetailSchema();

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
                        Response.Redirect("VoucherList.aspx");
                }
                else
                    Response.Redirect("VoucherList.aspx");
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
            Response.Redirect("VoucherList.aspx");
        }

        protected void txtAccountTitle_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            if (hfAccountAutoCompleted.Value != "0")
            {
                string AccountCode = objVoucherBR.GetAccountCode(Convert.ToInt32(hfAccountAutoCompleted.Value));
                if (AccountCode != "")
                {
                    txtAccountCode.Text = AccountCode;
                    ddlVoucherType.Focus();
                }
                else
                {
                    ErrorMessage = "Account does not exist in data base";
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    txtAccountCode.Focus();
                }
            }
            else
                ClearDetailControl();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            decimal DebitAmount = 0;
            decimal CreditAmount = 0;

            lblMessage.Text = string.Empty;

            if (!objVoucherBR.GetAccountNameIdExist(Convert.ToInt32(hfAccountAutoCompleted.Value), txtAccountTitle.Text.Trim()))
            {
                ErrorMessage = "Account Title does not exist in data base";
                lblMessage.Text = ErrorMessage;
                lblMessage.CssClass = "WarningText";
                ddlVoucherType.Focus();
                return;
            }            

            if (!txtDebitAmount.Text.Trim().Equals(""))
                DebitAmount = Convert.ToDecimal(txtDebitAmount.Text.Trim());

            if (!txtCreditAmount.Text.Trim().Equals(""))
                CreditAmount = Convert.ToDecimal(txtCreditAmount.Text.Trim());

            if ((txtDebitAmount.Text.Trim().Equals("")
                    && txtCreditAmount.Text.Trim().Equals("")) || (DebitAmount.Equals(0) && CreditAmount.Equals(0)))
            {
                ErrorMessage = "Debit or Credit Amount cannot be empty";
                lblMessage.Text = ErrorMessage;
                lblMessage.CssClass = "WarningText";
                ddlVoucherType.Focus();
                return;
            }

            if (DebitAmount > 0 && CreditAmount > 0)
            {
                ErrorMessage = "Only One Amount enter at a time";
                lblMessage.Text = ErrorMessage;
                lblMessage.CssClass = "WarningText";
                ddlVoucherType.Focus();
                return;
            }

            if (!DebitAmount.Equals(0) && DebitAmount < 0)
            {
                    ErrorMessage = "Debit Amount should be greater then zero";
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    ddlVoucherType.Focus();
                    return;
            }

            if (!CreditAmount.Equals(0) && CreditAmount < 0)
            {
                    ErrorMessage = "Credit Amount should be greater then zero";
                    lblMessage.Text = ErrorMessage;
                    lblMessage.CssClass = "WarningText";
                    ddlVoucherType.Focus();
                    return;
            }

            int MaxID = 0;
            if (OrderDetailDataTable.Rows.Count > 0)
                MaxID = Convert.ToInt32((object)OrderDetailDataTable.Compute("Max(RowNo)", string.Empty)) + 1;
            else
                MaxID = 1;

            DataRow dtRow = OrderDetailDataTable.NewRow();
            dtRow["RowNo"] = MaxID;
            dtRow["AccountId"] = hfAccountAutoCompleted.Value;
            dtRow["AccountCode"] = txtAccountCode.Text.Trim();
            dtRow["AccountName"] = txtAccountTitle.Text.Trim();
            dtRow["Narration"] = string.Empty;

            if (DebitAmount > 0)
            {
                dtRow["VoucherType"] = "D";
                dtRow["TotalDebit"] = txtDebitAmount.Text.Trim();
                dtRow["TotalCredit"] = "";
            }
            else
            {
                dtRow["VoucherType"] = "C";
                dtRow["TotalDebit"] = "";
                dtRow["TotalCredit"] = txtCreditAmount.Text.Trim();
            }

            OrderDetailDataTable.Rows.Add(dtRow);
            ViewState["OrderDetail"] = OrderDetailDataTable;

            GridView.DataSource = OrderDetailDataTable;
            GridView.DataBind();
            GrandTotal();
            ClearDetailControl();
            txtAccountTitle.Text = string.Empty;
            txtAccountTitle.Focus();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowId = 0;
            if (e.CommandName == "Del")
            {
                RowId = Int32.Parse(e.CommandArgument.ToString());

                OrderDetailDataTable.Rows.Find(RowId).Delete();
                OrderDetailDataTable.AcceptChanges();

                ViewState["OrderDetail"] = OrderDetailDataTable;
                GridView.DataSource = OrderDetailDataTable;
                GridView.DataBind();
                GrandTotal();
            }
        }

        #endregion

        #region Methods

        private void GetData(int ID)
        {
            DataTable dt = objVoucherBR.GetMasterRecord(ID);
            if (dt.Rows.Count > 0)
            {
                lblVoucherNo.Visible = true;
                lblHeadingVoucherNo.Visible = true;
                lblVoucherNo.Text = dt.Rows[0]["VoucherCode"].ToString();

                ddlVoucherType.SelectedValue = dt.Rows[0]["BookType"].ToString();
                txtVoucherDate.Text = Convert.ToDateTime(dt.Rows[0]["VoucherDate"]).ToString("dd/MM/yyyy");
                txtNarration.Text = dt.Rows[0]["NarrationMaster"].ToString();

                if (dt.Rows[0]["AutoPost"].ToString().Equals("0"))
                    txtStatus.Text = "UNPOST";
                else
                    txtStatus.Text = "POSTED";

                //Get Order Detail Records
                DataTable OrderDetailTable = objVoucherBR.GetDetailRecords(ID);
                int count = 1;
                foreach (DataRow drOrderDetail in OrderDetailTable.Rows)
                {
                    DataRow dtRow = OrderDetailDataTable.NewRow();

                    dtRow["RowNo"] = count;
                    dtRow["AccountId"] = drOrderDetail["AccountId"].ToString();
                    dtRow["AccountCode"] = drOrderDetail["AccountCode"].ToString();
                    dtRow["AccountName"] = drOrderDetail["AccountName"].ToString();
                    dtRow["Narration"] = drOrderDetail["Narration"].ToString();

                    if (!Convert.ToDecimal(drOrderDetail["DebitAmount"]).Equals(0))
                    {
                        dtRow["VoucherType"] = "D";
                        dtRow["TotalDebit"] = drOrderDetail["DebitAmount"].ToString();
                        dtRow["TotalCredit"] = "";
                    }
                    else
                    {
                        dtRow["VoucherType"] = "C";
                        dtRow["TotalDebit"] = "";
                        dtRow["TotalCredit"] = drOrderDetail["CreditAmount"].ToString();
                    }

                    OrderDetailDataTable.Rows.Add(dtRow);
                    count = count + 1;
                }

                ViewState["OrderDetail"] = OrderDetailDataTable;
                GridView.DataSource = OrderDetailDataTable;
                GridView.DataBind();
                GrandTotal();
            }
            else
                Response.Redirect("VoucherList.aspx");
        }

        private void GetOrderDetailSchema()
        {
            if (ViewState["OrderDetail"] == null)
            {
                OrderDetailDataTable = new DataTable();
                OrderDetailDataTable.Columns.Add("RowNo", System.Type.GetType("System.Int32"));
                OrderDetailDataTable.Columns.Add("AccountId");
                OrderDetailDataTable.Columns.Add("AccountCode");
                OrderDetailDataTable.Columns.Add("AccountName");
                OrderDetailDataTable.Columns.Add("Narration");
                OrderDetailDataTable.Columns.Add("TotalDebit");
                OrderDetailDataTable.Columns.Add("TotalCredit");
                OrderDetailDataTable.Columns.Add("VoucherType");
                OrderDetailDataTable.PrimaryKey = new DataColumn[] { OrderDetailDataTable.Columns["RowNo"] };
                ViewState["OrderDetail"] = OrderDetailDataTable;
                GridView.DataSource = OrderDetailDataTable;
                GridView.DataBind();
            }
            else
                OrderDetailDataTable = (DataTable)ViewState["OrderDetail"];
        }

        private void GetSetupsData()
        {

        }

        private void ClearControl()
        {
            ddlVoucherType.SelectedValue = "0";
            lblVoucherNo.Text = string.Empty;
            lblVoucherNo.Visible = true;
            lblHeadingVoucherNo.Visible = true;
            txtStatus.Text = string.Empty;

            btnSave.Text = "Save";
            btnSaveNew.Text = "Save And New";
            HiddenFieldMode.Value = "New";
            HiddenFieldID.Value = "0";

            ViewState["OrderDetail"] = null;
            GridView.DataSource = null;
            GridView.DataBind();

            txtNarration.Text = string.Empty;
            txtDifference.Text = string.Empty;
            txtDebitTotal.Text = string.Empty;
            txtCreditTotal.Text = string.Empty;
        }

        private void ClearDetailControl()
        {
            txtDebitAmount.Text = string.Empty;
            txtCreditAmount.Text = string.Empty;
            txtAccountCode.Text = string.Empty;
            txtAccountTitle.Text = string.Empty;
            hfAccountAutoCompleted.Value = "0";
        }

        private void GrandTotal()
        {
            decimal DebitTotal = 0;
            decimal CreditTotal = 0;
            foreach (DataRow dtRow in OrderDetailDataTable.Rows)
            {
                if (dtRow["TotalDebit"].ToString() != "") DebitTotal = DebitTotal + Convert.ToDecimal(dtRow["TotalDebit"]);
                if (dtRow["TotalCredit"].ToString() != "") CreditTotal = CreditTotal + Convert.ToDecimal(dtRow["TotalCredit"]);
            }

            txtDebitTotal.Text = String.Format("{0:#,###.00}", DebitTotal);
            txtCreditTotal.Text = String.Format("{0:#,###.00}", CreditTotal);
            txtDifference.Text = String.Format("{0:#,###.00}", DebitTotal - CreditTotal);
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
                if (OrderDetailDataTable.Rows.Count < 1)
                {
                    lblMessage.Text = "Before Saving, There should be at least one record in list";
                    lblMessage.CssClass = "WarningText";
                    ddlVoucherType.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtDifference.Text) != 0)
                {
                    ErrorMessage = "Total Debit must be equal to Total Credit";
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
                    int VoucherId = objVoucherBR.InsertMasterRecord(inputArrList);

                    foreach (DataRow dtRow in OrderDetailDataTable.Rows)
                    {
                        inputArrList.Clear();
                        inputArrList.Add(VoucherId);
                        inputArrList.Add(dtRow["AccountId"].ToString());

                        if (dtRow["TotalDebit"].ToString() != "")
                            inputArrList.Add(dtRow["TotalDebit"].ToString());
                        else
                            inputArrList.Add("0");

                        if (dtRow["TotalCredit"].ToString() != "")
                            inputArrList.Add(dtRow["TotalCredit"].ToString());
                        else
                            inputArrList.Add("0");

                        inputArrList.Add(dtRow["Narration"].ToString());
                        objVoucherBR.InsertDetailRecord(inputArrList);
                    }
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    inputArrList.Add(HiddenFieldID.Value);
                    inputArrList.Add(txtVoucherDate.Text.Trim());
                    inputArrList.Add(ddlVoucherType.SelectedValue);
                    inputArrList.Add(txtNarration.Text.Trim().ToUpper());
                    objVoucherBR.UpdateMasterRecord(inputArrList);

                    objVoucherBR.DeleteDetailRecord(Convert.ToInt32(HiddenFieldID.Value));

                    foreach (DataRow dtRow in OrderDetailDataTable.Rows)
                    {
                        inputArrList.Clear();
                        inputArrList.Add(HiddenFieldID.Value);
                        inputArrList.Add(dtRow["AccountId"].ToString());

                        if (dtRow["TotalDebit"].ToString() != "")
                            inputArrList.Add(dtRow["TotalDebit"].ToString());
                        else
                            inputArrList.Add("0");

                        if (dtRow["TotalCredit"].ToString() != "")
                            inputArrList.Add(dtRow["TotalCredit"].ToString());
                        else
                            inputArrList.Add("0");

                        inputArrList.Add(dtRow["Narration"].ToString());
                        objVoucherBR.InsertDetailRecord(inputArrList);
                    }
                }
                if (HiddenFieldMode.Value == "New")
                {
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordSave);
                    javaScriptCall(ErrorMessage);
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordUpdate);
                    javaScriptCall(ErrorMessage);
                }

                if (isSave == 0)
                    Response.Redirect("VoucherList.aspx");

                ClearControl();
            }
            catch (Exception exe)
            {
                javaScriptCall(exe.Message);
            }
        }

        private bool ProductAlreadyExistInList(int ProductId)
        {
            bool isCheck = false;
            string where = "ProductId= " + ProductId;
            DataRow[] dataRow = OrderDetailDataTable.Select(where);

            if (dataRow.Count() > 0)
                isCheck = true;

            return isCheck;
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
