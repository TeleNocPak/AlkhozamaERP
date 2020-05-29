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
    public partial class CompanyList : System.Web.UI.Page
    {
        #region Variables

        int index = 0;
        string ErrorMessage = string.Empty;
        CompanyBR objCompanyBR = new CompanyBR();
        clsVariables objclsVariables = new clsVariables();
        private static string SortOrder = "ASC";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyUserRoles();
            index = (CompanyGridView.PageIndex) * CompanyGridView.PageSize;
            if (!Page.IsPostBack)
            {
                hfSearchText.Value = string.Empty;
                GetGridData(hfSearchText.Value);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Company.aspx?Mode=New");
        }

        protected void CompanyGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (SortOrder == "ASC")
                SortOrder = "DESC";
            else
                SortOrder = "ASC";

            DataTable dataTable = objCompanyBR.GetAllRecords(hfSearchText.Value);
            dataTable.DefaultView.Sort = e.SortExpression + " " + SortOrder;
            CompanyGridView.DataSource = dataTable;
            CompanyGridView.DataBind();
        }

        protected void CompanyGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CompanyGridView.PageIndex = e.NewPageIndex;
            index = e.NewPageIndex * CompanyGridView.PageSize;
            GetGridData(hfSearchText.Value);
        }

        protected void CompanyGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = 0;
            if (e.CommandName == "View")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                Response.Redirect("Company.aspx?Mode=View&ID=" + ID);
            }
            else if (e.CommandName == "Edit")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                Response.Redirect("Company.aspx?Mode=Edit&ID=" + ID);
            }
            else if (e.CommandName == "Del")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                objCompanyBR.DeleteRecord(ID);
                ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordDelete);
                lblMessage.Text = ErrorMessage;
                lblMessage.CssClass = "ConfirmText";
                GetGridData(hfSearchText.Value);
            }
        }

        protected void CompanyGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int _counter = 0;
            if (e.Row != null)
            {
                if (((LinkButton)e.Row.FindControl("btnView")) != null)
                    ((LinkButton)e.Row.FindControl("btnView")).Visible = objclsVariables.btnView;

                if (((LinkButton)e.Row.FindControl("btnEdit")) != null)
                    ((LinkButton)e.Row.FindControl("btnEdit")).Visible = objclsVariables.btnEdit;

                if (((LinkButton)e.Row.FindControl("btnDelete")) != null)
                    ((LinkButton)e.Row.FindControl("btnDelete")).Visible = objclsVariables.btnDelete;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
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

            if (txtCompanyName.Text != "")
                SearchText = SearchText + " and CompanyName like '%" + txtCompanyName.Text.Trim().Replace("'", "`") + "%'";

            if (txtPhone.Text != "")
                SearchText = SearchText + " and ContactPerson like '%" + txtPhone.Text.Trim().Replace("'", "`") + "%'";

            if (txtMobile.Text != "")
                SearchText = SearchText + " and ContactMobile like '%" + txtMobile.Text.Trim().Replace("'", "`") + "%'";

            if (txtAddress.Text != "")
                SearchText = SearchText + " and Address like '%" + txtAddress.Text.Trim().Replace("'", "`") + "%'";

            hfSearchText.Value = SearchText.Trim();

            DataTable dtSearch = objCompanyBR.GetAllRecords(SearchText);
            CompanyGridView.DataSource = dtSearch;
            CompanyGridView.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCompanyName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAddress.Text = string.Empty;
            hfSearchText.Value = string.Empty;
            GetGridData(string.Empty);
        }

        #endregion

        #region Methods
        private void GetGridData(string SearchText)
        {
            CompanyGridView.DataSource = objCompanyBR.GetAllRecords(SearchText);
            CompanyGridView.DataBind();
        }

        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 25);
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

        #endregion
    }
}
