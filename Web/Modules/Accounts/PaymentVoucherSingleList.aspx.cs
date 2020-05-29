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
    public partial class PaymentVoucherSingleList : System.Web.UI.Page
    {
        #region Variables

        int index = 0;
        string ErrorMessage = string.Empty;
        PaymentVoucherBR objPaymentVoucherBR = new PaymentVoucherBR();
        clsVariables objclsVariables = new clsVariables();
        private static string SortOrder = "ASC";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            //txtVoucherDate.Attributes.Add("readonly", "true");

            ApplyUserRoles();
            index = (GridView.PageIndex) * GridView.PageSize;
            if (!Page.IsPostBack)
            {
                GetGridData();
                GetSetupsData();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentVoucherSingle.aspx?Mode=New");
        }

        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (SortOrder == "ASC")
                SortOrder = "DESC";
            else
                SortOrder = "ASC";

            DataTable dataTable = objPaymentVoucherBR.GetAllMasterRecords(string.Empty);
            dataTable.DefaultView.Sort = e.SortExpression + " " + SortOrder;
            GridView.DataSource = dataTable;
            GridView.DataBind();
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView.PageIndex = e.NewPageIndex;
            index = e.NewPageIndex * GridView.PageSize;
            GetGridData();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = 0;
            if (e.CommandName == "View")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                Response.Redirect("PaymentVoucherSingle.aspx?Mode=View&ID=" + ID);
            }
            else if (e.CommandName == "Edt")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                Response.Redirect("PaymentVoucherSingle.aspx?Mode=Edit&ID=" + ID);
            }
            else if (e.CommandName == "Del")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                objPaymentVoucherBR.DeleteMasterDetailRecord(ID);
                ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordDelete);
                lblMessage.Text = ErrorMessage;
                lblMessage.CssClass = "ConfirmText";
                GetGridData();
            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int _counter = 0;
            if (e.Row != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (((LinkButton)e.Row.FindControl("btnView")) != null)
                        ((LinkButton)e.Row.FindControl("btnView")).Visible = objclsVariables.btnView;

                    if (((LinkButton)e.Row.FindControl("btnEdit")) != null)
                        ((LinkButton)e.Row.FindControl("btnEdit")).Visible = objclsVariables.btnEdit;

                    if (((LinkButton)e.Row.FindControl("btnDelete")) != null)
                        ((LinkButton)e.Row.FindControl("btnDelete")).Visible = objclsVariables.btnDelete;

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

            if (txtVoucherNo.Text.Trim() != "")
                SearchText = SearchText + " and VoucherCode like '%" + txtVoucherNo.Text.Trim().Replace("'", "`") + "%'";

            if (txtNarration.Text.Trim() != "")
                SearchText = SearchText + " and NarrationMaster like '%" + txtNarration.Text.Trim().Replace("'", "`") + "%'";
            
            if (txtVoucherDate.Text.Trim() != "")
                SearchText = SearchText + " and VoucherDate ='" + CommonObjects.ConvertMMDDYYYY(txtVoucherDate.Text.Trim()) + "'";
                
            if (ddlStatus.SelectedValue.ToString() != "-1")
                SearchText = SearchText + " and AutoPost='" + ddlStatus.SelectedValue + "'";

            if (ddlVoucherType.SelectedValue.ToString() != "0")
                SearchText = SearchText + " and BookType='" + ddlVoucherType.SelectedValue + "'";

            DataTable dtSearch = objPaymentVoucherBR.GetAllMasterRecords(SearchText);
            GridView.DataSource = dtSearch;
            GridView.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtVoucherNo.Text = string.Empty;
            txtVoucherDate.Text = string.Empty;
            ddlStatus.SelectedValue = "-1";
            GetGridData();
        }

        #endregion

        #region Methods

        private void GetGridData()
        {
            GridView.DataSource = objPaymentVoucherBR.GetAllMasterRecords(string.Empty);
            GridView.DataBind();
        }

        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 45);
                if (dt.Rows.Count > 0)
                {
                    btnAdd.Visible = Convert.ToBoolean(dt.Rows[0]["AllowAdd"]);
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

        private void GetSetupsData()
        {
           
        }

        #endregion

    }
}
