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
    public partial class UserMasterList : System.Web.UI.Page
    {
        #region Variables

        int index = 0;
        string ErrorMessage = string.Empty;
        UserMasterBR objUserMasterBR = new UserMasterBR();
        clsVariables objclsVariables = new clsVariables();
        private static string SortOrder = "ASC";

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyUserRoles();
            index = (UserGridView.PageIndex) * UserGridView.PageSize;
            if (!Page.IsPostBack)
            {
                GetSetupsData();
                hfSearchText.Value = string.Empty;
                GetGridData(hfSearchText.Value);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserMaster.aspx?Mode=New");
        }

        protected void UserGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (SortOrder == "ASC")
                SortOrder = "DESC";
            else
                SortOrder = "ASC";

            DataTable dataTable = objUserMasterBR.GetAllRecords(hfSearchText.Value);
            dataTable.DefaultView.Sort = e.SortExpression + " " + SortOrder;
            UserGridView.DataSource = dataTable;
            UserGridView.DataBind();
        }

        protected void UserGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UserGridView.PageIndex = e.NewPageIndex;
            index = e.NewPageIndex * UserGridView.PageSize;
            GetGridData(hfSearchText.Value);
        }

        protected void UserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = 0;
            if (e.CommandName == "View")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                Response.Redirect("UserMaster.aspx?Mode=View&ID=" + ID);
            }
            else if (e.CommandName == "Edit")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                Response.Redirect("UserMaster.aspx?Mode=Edit&ID=" + ID);
            }
            else if (e.CommandName == "Del")
            {
                ID = Int32.Parse(e.CommandArgument.ToString());
                objUserMasterBR.DeleteRecord(ID);
                ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordDelete);
                lblMessage.Text = ErrorMessage;
                lblMessage.CssClass = "ConfirmText";
                GetGridData(hfSearchText.Value);
            }
        }

        protected void UserGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (txtLoginId.Text != "")
                SearchText = SearchText + " and LoginID like '%" + txtLoginId.Text.Trim().Replace("'", "`") + "%'";

            if (txtName.Text != "")
                SearchText = SearchText + " and FullName like '%" + txtName.Text.Trim().Replace("'", "`") + "%'";

            if (txtPhone.Text != "")
                SearchText = SearchText + " and Phone like '%" + txtPhone.Text.Trim().Replace("'", "`") + "%'";

            if (ddlRole.SelectedValue.ToString() != "0")
                SearchText = SearchText + " and a.RoleID ='" + ddlRole.SelectedValue + "'";

            hfSearchText.Value = SearchText.Trim();

            DataTable dtSearch = objUserMasterBR.GetAllRecords(SearchText);
            UserGridView.DataSource = dtSearch;
            UserGridView.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtLoginId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;

            ddlRole.SelectedValue = "0";
            hfSearchText.Value = string.Empty;
            GetGridData(string.Empty);
        }

        #endregion

        #region Methods
        private void GetGridData(string SearchText)
        {
            UserGridView.DataSource = objUserMasterBR.GetAllRecords(SearchText);
            UserGridView.DataBind();
        }
        private void GetSetupsData()
        {
            DataTable dtTemp = objUserMasterBR.GetRoles();

            DataRow dr = dtTemp.NewRow();
            dr["ID"] = 0;
            dr["RoleName"] = "ALL";
            dtTemp.Rows.InsertAt(dr, 0);

            ddlRole.DataSource = dtTemp;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();
        }
        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 2);
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
