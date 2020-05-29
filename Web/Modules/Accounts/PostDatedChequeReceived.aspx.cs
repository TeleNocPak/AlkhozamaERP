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
    public partial class PostDatedChequeReceived : System.Web.UI.Page
    {
        #region Variables

        int index = 0;
        string ErrorMessage = string.Empty;
        PostDatedChequeReceivedBR objPostDatedChequeReceivedBR = new PostDatedChequeReceivedBR();
        clsVariables objclsVariables = new clsVariables();
        private static string SortOrder = "DESC";
        private static string SortExpression = "PostDatedChequeId";
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

            ApplyUserRoles();
            index = (GridView.PageIndex) * GridView.PageSize;

            if (!Page.IsPostBack)
            {
                if (Session["GridPageIndex"] != null)
                {
                    GridView.PageIndex = Convert.ToInt32(Session["GridPageIndex"]);
                    index = (GridView.PageIndex) * GridView.PageSize;
                }

                if (Session["SearchText"] != null)
                    hfSearchText.Value = Session["SearchText"].ToString();
                else
                    hfSearchText.Value = string.Empty;

                hfSearchText.Value = string.Empty;
                GetBankName();
                GetGridData();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderRequisition.aspx?Mode=New");
        }
        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortOrder = SortOrder.Equals("ASC") ? "DESC" : "ASC";
            SortExpression = e.SortExpression;
            GetGridData();
            DataTable dataTable = objPostDatedChequeReceivedBR.GetAllRecords(string.Empty);
            dataTable.DefaultView.Sort = e.SortExpression + " " + SortOrder;
            GridView.DataSource = dataTable;
            GridView.DataBind();
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Session["GridPageIndex"] = e.NewPageIndex;

            GridView.PageIndex = e.NewPageIndex;
            index = e.NewPageIndex * GridView.PageSize;
            GetGridData();
        }
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = 0;
            if (e.CommandName == "Paid")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                hfPDCDetailId.Value = ID.ToString();
                Paid_ModalPopupExtender.Show();                
            }

        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int _counter = 0;
            if (e.Row != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if(((LinkButton)e.Row.FindControl("btnView")) != null)
                        ((LinkButton)e.Row.FindControl("btnView")).Visible = objclsVariables.btnView;

                    _counter = index + 1;
                    Label lblSerialNo = (Label)(e.Row.FindControl("lblSerialNo"));

                    lblSerialNo.Text = _counter.ToString();
                    lblSerialNo.DataBind();
                    index++;
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string SearchText = "1=1";

            if (hfDealerNameAutoCompleted.Value != "0")
                SearchText = SearchText + " and a.DealerId like '%" + hfDealerNameAutoCompleted.Value.Replace("'", "`") + "%'";

            if (txtSubmitDate.Text.Trim() != "")
                SearchText = SearchText + " and a.SubmitDate ='" + CommonObjects.ConvertMMDDYYYY(txtSubmitDate.Text.Trim()) + "'";

            if (txtChequeDate.Text.Trim() != "")
                SearchText = SearchText + " and b.ChequeDate ='" + CommonObjects.ConvertMMDDYYYY(txtChequeDate.Text.Trim()) + "'";

            if (txtChequeNo.Text.Trim() != "")
                SearchText = SearchText + " and b.ChequeNo like '%" + txtChequeNo.Text.Trim().Replace("'", "`") + "%'";

            if (txtBankName.Text.Trim() != "")
                SearchText = SearchText + " and b.BankName like '%" + txtBankName.Text.Trim().Replace("'", "`") + "%'";

            if (txtBranchName.Text.Trim() != "")
                SearchText = SearchText + " and b.BranchName like '%" + txtBranchName.Text.Trim().Replace("'", "`") + "%'";

            if (ddlPaidStatus.SelectedValue.ToString() != "-1")
                SearchText = SearchText + " and b.PaidStatus='" + ddlPaidStatus.SelectedValue + "'";

            if (txtAmount.Text.Trim() != "")
                SearchText = SearchText + " and b.Amount " + ddlAmount.SelectedItem.Text + " " + txtAmount.Text.Trim() + "";

            hfSearchText.Value = SearchText.Trim();
            Session["SearchText"] = SearchText.Trim();
            GetGridData();

            DataTable dtSearch = objPostDatedChequeReceivedBR.GetAllRecords(SearchText);
            GridView.DataSource = dtSearch;
            GridView.DataBind();
        }
        protected void chkAccountStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gridRow = (sender as CheckBox).NamingContainer as GridViewRow;
                HiddenField hfPDCDetailId = (HiddenField)gridRow.FindControl("hfPDCDetailId");
                CheckBox chkAccountStatus = (CheckBox)gridRow.FindControl("chkAccountStatus");
                objPostDatedChequeReceivedBR.UpdateAccountStatus(Convert.ToInt32(hfPDCDetailId.Value), Convert.ToBoolean(chkAccountStatus.Checked));
                javaScriptCall("Cheque Account Received Updated successfully.");
            }
            catch (Exception Exc)
            {
            }
        }

        protected void btnAccontPaid_Click(object sender, EventArgs e)
        {
            ArrayList inputArrList = new ArrayList();
            inputArrList.Add(hfPDCDetailId.Value);
            inputArrList.Add(txtPaid.Text.Trim());
            inputArrList.Add(ddlBank.SelectedValue);
            inputArrList.Add(txtDSNo.Text.Trim());
            inputArrList.Add(txtNarration.Text.Trim());
            
            objPostDatedChequeReceivedBR.UpdateAccontPaid(inputArrList);
            Paid_ModalPopupExtender.Hide();
            ClearPaidControls();
        }

        protected void btnAccountCancel_Click(object sender, EventArgs e)
        {
            Paid_ModalPopupExtender.Hide();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        #endregion

        #region Methods

        private void GetGridData()
        {
            DataTable dataTable = objPostDatedChequeReceivedBR.GetAllRecords(string.Empty);
            dataTable.DefaultView.Sort = SortExpression + " " + SortOrder;
            GridView.DataSource = dataTable;
            GridView.DataBind();
        }
        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 53);
                if (dt.Rows.Count > 0)
                {
                    //btnAdd.Visible = Convert.ToBoolean(dt.Rows[0]["AllowAdd"]);
                    objclsVariables.btnView = Convert.ToBoolean(dt.Rows[0]["AllowView"]);
                    objclsVariables.btnEdit = Convert.ToBoolean(dt.Rows[0]["AllowEdit"]);
                    objclsVariables.btnDelete = Convert.ToBoolean(dt.Rows[0]["AllowDelete"]);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected string GetSubmitDate(object date)
        {
            return (Convert.ToDateTime(date).ToString("dd MMM yyyy").Equals("01 Jan 1900") || string.IsNullOrWhiteSpace(date.ToString()) ? "" : Convert.ToDateTime(date).ToString("dd MMM yyyy"));
        }
        protected string GetChequeDate(object date)
        {
            return (Convert.ToDateTime(date).ToString("dd MMM yyyy").Equals("01 Jan 1900") || string.IsNullOrWhiteSpace(date.ToString()) ? "" : Convert.ToDateTime(date).ToString("dd MMM yyyy"));
        }
        private void GetBankName()
        {
            DataTable dataTable = objPostDatedChequeReceivedBR.GetBankName();
            DataRow dr = dataTable.NewRow();
            dr["BankID"] = 0;
            dr["BankName"] = "Select a BANK";
            dataTable.Rows.InsertAt(dr, 0);

            ddlBank.DataSource = dataTable;
            ddlBank.DataTextField = "BankName";
            ddlBank.DataValueField = "BankID";
            ddlBank.DataBind();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetDealerList(string prefixText, int count, string contextKey)
        {
            string queryString = @"SELECT Top 20 DealerID, DealerName FROM Dealers WHERE DealerName LIKE '%" + prefixText.Trim().ToUpper() + "%'";

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
                adapter.Fill(ds, 0, 30, "Dealers");
                items = new List<string>();
                string returnString = String.Empty;

                foreach (DataRow dr in ds.Tables[0].Rows)
                    items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["DealerName"].ToString(), dr["DealerID"].ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return items.ToArray();
        }
        private void javaScriptCall(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('" + message + "');", true);
        }
        private void ClearControls()
        {
            txtDealerName.Text = string.Empty;
            txtSubmitDate.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtChequeNo.Text = string.Empty;
            txtBranchName.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtAmount.Text = string.Empty;
            ddlPaidStatus.SelectedIndex = 0;
            ddlAmount.SelectedIndex = 0;
            hfDealerNameAutoCompleted.Value = "0";

            hfSearchText.Value = string.Empty;
            GetGridData();
        }

        private void ClearPaidControls()
        {
            txtPaid.Text = string.Empty;
            ddlBank.SelectedValue = "0";
            txtDSNo.Text = string.Empty;
            txtNarration.Text = string.Empty;
        }


        #endregion
    }
}
