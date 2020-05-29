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
    public partial class GeneralVoucherList : System.Web.UI.Page
    {
        #region Variables

        int index = 0;
        string ErrorMessage = string.Empty;
        GeneralVoucherBR objGeneralVoucherBR = new GeneralVoucherBR();
        clsVariables objclsVariables = new clsVariables();
        private static string SortOrder = "ASC";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
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
            Response.Redirect("GeneralVoucher.aspx?Mode=New");
        }

        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (SortOrder == "ASC")
                SortOrder = "DESC";
            else
                SortOrder = "ASC";

            DataTable dataTable = objGeneralVoucherBR.GetAllMasterRecords(string.Empty);
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
                Response.Redirect("GeneralVoucher.aspx?Mode=View&ID=" + ID);
            }
            else if (e.CommandName == "Edt")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());

                //DataTable dt = objGeneralVoucherBR.OrderExistinConfirm(ID);
                //if (dt.Rows.Count > 0)
                //{
                //    lblMessage.Text = "Cannot edit this record, Order Requisition exist in Confirmation";
                //    lblMessage.CssClass = "WarningText";
                //    return;
                //}

                Response.Redirect("GeneralVoucher.aspx?Mode=Edit&ID=" + ID);
            }
            else if (e.CommandName == "Del")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());

                //DataTable dt = objGeneralVoucherBR.OrderExistinConfirm(ID);
                //if (dt.Rows.Count > 0)
                //{
                //    lblMessage.Text = "Cannot delete this record, Order Requisition exist in Confirmation";
                //    lblMessage.CssClass = "WarningText";
                //    return;
                //}

                objGeneralVoucherBR.DeleteMasterDetailRecord(ID);
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

            if (txtGeneralVoucherNo.Text.Trim() != "")
                SearchText = SearchText + " and VoucherCode like '%" + txtGeneralVoucherNo.Text.Trim().Replace("'", "`") + "%'";

            if (txtGeneralVoucherDate.Text.Trim() != "")
                SearchText = SearchText + " and VoucherDate = '" + Convert.ToDateTime(txtGeneralVoucherDate.Text.Trim()).ToString("yyyy/MM/dd") + "'";

            if (ddlStatus.SelectedValue.ToString() != "-1")
                SearchText = SearchText + " and AutoPost='" + ddlStatus.SelectedValue + "'";

            DataTable dtSearch = objGeneralVoucherBR.GetAllMasterRecords(SearchText);
            GridView.DataSource = dtSearch;
            GridView.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtGeneralVoucherNo.Text = string.Empty;
            txtGeneralVoucherDate.Text = string.Empty;           
            ddlStatus.SelectedValue = "0";            
            GetGridData();
        }

        #endregion

        #region Methods

        private void GetGridData()
        {
            GridView.DataSource = objGeneralVoucherBR.GetAllMasterRecords(string.Empty);
            GridView.DataBind();
        }

        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 59);
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
