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
using System.Drawing;

namespace ERPWeb
{
    public partial class ChartofAccount : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty;
        COABR objCOABR = new COABR();
        private DataTable COADataTable;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyUserRoles();
            if (!IsPostBack)
            {
                HiddenFieldMode.Value = "New";
                HiddenFieldID.Value = "0";
                HiddenFieldAccountId.Value = "0";

                GetSetupsData();
                //GetLocations();
                GetCOA();
            }
        }

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {
            InsertData(1);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtParentAccountName.Text = string.Empty;
            txtParentAccountCode.Text = string.Empty;
            HiddenFieldAccountId.Value = "0";
            ddlBranch.SelectedValue = "0";
            ddlBranch.Enabled = true;
            ClearControl();
            GetLocations(0);
        }

        protected void tvCOA_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
                ddlBranch.Enabled = true;
                ddlBranch.SelectedValue = "0";
                txtParentAccountName.Text = tvCOA.SelectedNode.Text.Trim();
                txtParentAccountCode.Text = tvCOA.SelectedNode.ToolTip.Trim();
                HiddenFieldAccountId.Value = tvCOA.SelectedNode.Value.Trim();

                // Check Exist in Account Branch
                DataTable dt = objCOABR.GetRecord(Convert.ToInt32(HiddenFieldAccountId.Value));
                if (dt.Rows.Count > 0)
                {
                    if (!dt.Rows[0]["AccountBranchId"].ToString().Equals("0"))
                    {
                        ddlBranch.SelectedValue = dt.Rows[0]["AccountBranchId"].ToString();
                        ddlBranch.Enabled = false;
                    }
                }

                GetLocations(Convert.ToInt32(ddlBranch.SelectedValue));

                //string[] splitAccoutCode = tvCOA.SelectedNode.ToolTip.Trim().Split('-');
                //if (splitAccoutCode.Length > 1)
                //{
                //    ddlBranch.SelectedValue = splitAccoutCode[0];
                //    ddlBranch.Enabled = false;
                //}
                //else
                //    ddlBranch.Enabled = true;

                txtAccountName.Focus();
            }
            catch (Exception Exe)
            {
                javaScriptCall(Exe.Message);
            }
        }

        protected void btnEditCOA_Click(object sender, ImageClickEventArgs e)
        {
            if (HiddenFieldAccountId.Value == string.Empty || HiddenFieldAccountId.Value == "0")
            {
                ErrorMessage = "You need to select a node First";
                javaScriptCall(ErrorMessage);
                return;
            }

            if (txtParentAccountCode.Text.Trim().Length.Equals(2))
            {
                ErrorMessage = "First level of account code cannot be edit";
                javaScriptCall(ErrorMessage);
                return;
            }

            if (tvCOA.SelectedNode != null)
            {
                HiddenFieldID.Value = tvCOA.SelectedNode.Value;
                GetData(Convert.ToInt32(HiddenFieldID.Value));
            }
        }

        protected void LocationGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (HiddenFieldID.Value != "0" && HiddenFieldID.Value != string.Empty)
                    {
                        TextBox txtOpeningBalance = (TextBox)(e.Row.FindControl("txtOpeningBalance"));
                        HiddenField hfLocationId = (HiddenField)(e.Row.FindControl("hfLocationId"));
                        CheckBox chkAppeared = (CheckBox)(e.Row.FindControl("chkAppeared"));

                        DataTable dataTableCOA = objCOABR.GetLocationCOA(Convert.ToInt32(HiddenFieldID.Value), Convert.ToInt32(hfLocationId.Value));
                        if (dataTableCOA.Rows.Count > 0)
                        {
                            txtOpeningBalance.Text = dataTableCOA.Rows[0]["OpeningBalance"].ToString();
                            chkAppeared.Checked = Convert.ToBoolean(dataTableCOA.Rows[0]["Appeared"]);
                        }
                    }
                }
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLocations(Convert.ToInt32(ddlBranch.SelectedValue));
        }

        #endregion

        #region Methods

        private void GetSetupsData()
        {
            DataTable dataTable = objCOABR.GetBranch();
            DataRow dr = dataTable.NewRow();
            dr["BranchID"] = 0;
            dr["BranchName"] = "Account name without branch";
            dataTable.Rows.InsertAt(dr, 0);

            ddlBranch.DataSource = dataTable;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
        }

        private void LocationVisible(bool ischeck)
        {
            TRLoc1.Visible = ischeck;
            TRLoc2.Visible = ischeck;
        }
        private void GetLocations(int BranchId)
        {
            if (BranchId != 0)
            {
                LocationVisible(true);
                LocationGridView.DataSource = objCOABR.GetLocations(BranchId);
                LocationGridView.DataBind();
            }
            else
                LocationVisible(false);
        }

        private void GetCOA()
        {
            COADataTable = objCOABR.GetCOARecord();
            if (COADataTable.Rows.Count > 0)
            {
                string whereClause = "ParentAccountId = -1";
                DataRow[] TempDataTable;
                TempDataTable = COADataTable.Select(whereClause, "AccountCode");

                foreach (DataRow dr in TempDataTable)
                {
                    TreeNode node = new TreeNode();

                    node.Text = dr["AccountName"].ToString();
                    node.Value = dr["AccountId"].ToString();

                    //if (dr["AccountBranchId"].ToString().Equals("0"))
                    //    node.ToolTip = dr["AccountCode"].ToString();
                    //else
                    //    node.ToolTip = dr["AccountBranchId"].ToString() + "-" + dr["AccountCode"].ToString();

                    node.ToolTip = dr["AccountCode"].ToString();

                    node.ImageUrl = "../img/folder.png";
                    AddNodes(node);

                    tvCOA.Nodes.Add(node);
                    tvCOA.ExpandDepth = 1;
                }
            }
        }

        private void GetData(int ID)
        {
            DataTable dt = objCOABR.GetRecord(ID);

            if (dt.Rows.Count > 0)
            {
                txtAccountName.Text = dt.Rows[0]["AccountName"].ToString();
                //txtParentAccountCode.Text = dt.Rows[0]["AccountBranchId"].ToString() + "-" + dt.Rows[0]["AccountCode"].ToString();
                txtParentAccountCode.Text = dt.Rows[0]["AccountCode"].ToString();
                ddlBranch.SelectedValue = dt.Rows[0]["AccountBranchId"].ToString();

                GetLocations(Convert.ToInt32(ddlBranch.SelectedValue));
                btnSaveNew.Text = "Update & New";
                HiddenFieldMode.Value = "Edit";

                ddlBranch.Enabled = false;
                txtAccountName.Focus();
            }
            else
            {
                ErrorMessage = "Record does not exist in db";
                javaScriptCall(ErrorMessage);
            }
        }

        //private void GetLocations()
        //{
        //    DataTable dataTable = objCOABR.GetLocations();

        //    LocationGridView.DataSource = dataTable;
        //    LocationGridView.DataBind();
        //}

        private void ApplyUserRoles()
        {
            try
            {
                UserRoleBR objUserRoleBR = new UserRoleBR();
                DataTable dt = objUserRoleBR.GetRoleDetailByID(Convert.ToInt32(Session["RoleID"].ToString()), 44);
                if (dt.Rows.Count > 0)
                {
                    btnSaveNew.Visible = Convert.ToBoolean(dt.Rows[0]["AllowAdd"]);
                    btnEditCOA.Visible = Convert.ToBoolean(dt.Rows[0]["AllowEdit"]);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }

        private void ClearControl()
        {
            //lblDash.Visible = true;
            //txtAccountCode.Visible = true;
            //txtParentAccountCode.Width = 100;

            //txtAccountCode.Text = string.Empty;
            //txtOpeningBalance.Text = "0";
            //txtClosingBalance.Text = "0";
            //btnSave.Text = "Save";

            //txtParentAccountName.Text = string.Empty;
            //txtParentAccountCode.Text = string.Empty;

            txtAccountName.Text = string.Empty;
            btnSaveNew.Text = "Save And New";
            HiddenFieldMode.Value = "New";
            HiddenFieldID.Value = "0";
        }

        private void InsertData(int isSave)
        {
            int AccountId = 0;
            bool isExist = objCOABR.AccountNameAlredayExist(Convert.ToInt32(HiddenFieldID.Value), Convert.ToInt32(ddlBranch.SelectedValue), txtAccountName.Text.Trim());
            if (isExist)
            {
                ErrorMessage = "Account Name Alreday Exist";
                javaScriptCall(ErrorMessage);
                return;
            }

            if (HiddenFieldAccountId.Value == string.Empty || HiddenFieldAccountId.Value == "0")
            {
                ErrorMessage = "You need to select a node First";
                javaScriptCall(ErrorMessage);
                return;
            }            

            if (ddlBranch.SelectedValue != "0")
            {
                bool InvalidLocation = false;
                foreach (GridViewRow gRow in LocationGridView.Rows)
                {
                    bool chkAppeared = ((CheckBox)gRow.Controls[0].FindControl("chkAppeared")).Checked;
                    if (chkAppeared.Equals(true))
                    {
                        InvalidLocation = true;
                        break;
                    }
                }

                if (!InvalidLocation)
                {
                    javaScriptCall("Before Saving, There should be at least one record selected in Location List.");
                    txtAccountName.Focus();
                    return;
                }
            }

            if (txtParentAccountCode.Text.Trim().Length > 200)
            {
                ErrorMessage = "Account Code exceeds to 200-Character.";
                javaScriptCall(ErrorMessage);
                return;
            }

            string NewCOA = "", ParentAccountCode = "", ParentId = "";

            //string[] splitAccoutCode = txtParentAccountCode.Text.Trim().Split('-');
            //if (splitAccoutCode.Length > 1)
            //{
            //    for (int Count = 1; Count < splitAccoutCode.Length; Count++)
            //    {
            //        if (ParentAccountCode.Length > 0) ParentAccountCode += "-";
            //        ParentAccountCode += splitAccoutCode[Count];
            //    }
            //}
            //else
            //    ParentAccountCode = txtParentAccountCode.Text.Trim();

            ParentAccountCode = txtParentAccountCode.Text.Trim();
            ParentId = HiddenFieldAccountId.Value.ToString();

            ArrayList inputArrList = new ArrayList();
            try
            {
                if (HiddenFieldMode.Value == "New")
                {
                    //if (txtParentAccountCode.Text.Trim().Length > 8)
                    //{
                    //    ErrorMessage = "Only 4th level account create";
                    //    javaScriptCall(ErrorMessage);
                    //    return;
                    //}

                    inputArrList.Add(ParentAccountCode);
                    inputArrList.Add(txtAccountName.Text.Trim().ToUpper());
                    inputArrList.Add(ParentId);
                    inputArrList.Add(0);        // Old Opening Balance
                    inputArrList.Add(ddlBranch.SelectedValue);
                    AccountId = objCOABR.InsertRecord(inputArrList, out NewCOA);

                    foreach (GridViewRow gRow in LocationGridView.Rows)
                    {
                        inputArrList.Clear();
                        string txtOpeningBalance = ((TextBox)gRow.Controls[0].FindControl("txtOpeningBalance")).Text;
                        string hfLocationId = ((HiddenField)gRow.Controls[0].FindControl("hfLocationId")).Value;
                        bool chkAppeared = ((CheckBox)gRow.Controls[0].FindControl("chkAppeared")).Checked;

                        txtOpeningBalance = txtOpeningBalance.Equals("") ? "0" : txtOpeningBalance;

                        if (chkAppeared.Equals(true))
                        {
                            inputArrList.Add(AccountId);
                            inputArrList.Add(hfLocationId);
                            inputArrList.Add(txtOpeningBalance);
                            inputArrList.Add(chkAppeared);
                            objCOABR.InsertCOALocationDetailRecord(inputArrList);
                        }
                    }
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    AccountId = Convert.ToInt32(HiddenFieldID.Value);
                    inputArrList.Add(AccountId);
                    inputArrList.Add(txtAccountName.Text.Trim().ToUpper());
                    objCOABR.UpdateRecord(inputArrList);

                    foreach (GridViewRow gRow in LocationGridView.Rows)
                    {
                        inputArrList.Clear();
                        string txtOpeningBalance = ((TextBox)gRow.Controls[0].FindControl("txtOpeningBalance")).Text;
                        string hfLocationId = ((HiddenField)gRow.Controls[0].FindControl("hfLocationId")).Value;
                        bool chkAppeared = ((CheckBox)gRow.Controls[0].FindControl("chkAppeared")).Checked;

                        txtOpeningBalance = txtOpeningBalance.Equals("") ? "0" : txtOpeningBalance;

                        inputArrList.Add(AccountId);
                        inputArrList.Add(hfLocationId);
                        inputArrList.Add(txtOpeningBalance);
                        inputArrList.Add(chkAppeared);

                        DataTable dataTableCOA = objCOABR.GetLocationCOA(AccountId, Convert.ToInt32(hfLocationId));
                        if (dataTableCOA.Rows.Count > 0)
                            objCOABR.UpdateCOALocationDetailRecord(inputArrList);
                        else
                        {
                            if (chkAppeared.Equals(true))
                                objCOABR.InsertCOALocationDetailRecord(inputArrList);
                        }
                    }
                }

                string AccountName = txtAccountName.Text.Trim().ToUpper();

                if (ddlBranch.SelectedValue != "0")
                    AccountName += " (" + objCOABR.GetBranchName(Convert.ToInt32(ddlBranch.SelectedValue)) + ")";

                if (HiddenFieldMode.Value == "New")
                {
                    AddSingleNodeList(AccountId, NewCOA, AccountName);

                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordSave);
                    javaScriptCall(ErrorMessage);
                }
                else if (HiddenFieldMode.Value == "Edit")
                {
                    tvCOA.SelectedNode.Text = AccountName;
                    ErrorMessage = ErrorMessageBuilder.BuildMessage(MessageKeys.RecordUpdate);
                    javaScriptCall(ErrorMessage);
                }

                ClearControl();
                GetLocations(Convert.ToInt32(ddlBranch.SelectedValue));
                txtAccountName.Focus();
            }
            catch (Exception exe)
            {
                lblMessage.Text = exe.Message;
                lblMessage.CssClass = "WarningText";
            }
        }

        private void AddNodes(TreeNode node)
        {
            string whereClause = "ParentAccountId = " + node.Value;
            DataRow[] TempDataTable;
            TempDataTable = COADataTable.Select(whereClause, "AccountName");

            foreach (DataRow dr in TempDataTable)
            {
                TreeNode nNode = new TreeNode();

                nNode.Text = dr["AccountName"].ToString();
                nNode.Value = dr["AccountId"].ToString();

                //if (dr["AccountBranchId"].ToString().Equals("0"))
                //    nNode.ToolTip = dr["AccountCode"].ToString();
                //else
                //    nNode.ToolTip = dr["AccountBranchId"].ToString() + "-" + dr["AccountCode"].ToString();

                nNode.ToolTip = dr["AccountCode"].ToString();

                if (dr["AccountCode"].ToString().Length.Equals(5))
                {
                    tvCOA.ForeColor = System.Drawing.Color.Red;
                    nNode.ImageUrl = "../img/squareblue.gif";
                }
                else if (dr["AccountCode"].ToString().Length.Equals(8))
                {
                    nNode.ImageUrl = "../img/squaregreen.gif";
                }
                AddNodes(nNode);
                node.ChildNodes.Add(nNode);
            }
        }

        private void AddNodeList(DataTable pDataTble)
        {
            TreeNode nodeLoc;
            if (tvCOA.SelectedNode != null)
            {
                tvCOA.SelectedNode.ChildNodes.Clear();
                //tvCOA.SelectedNode.ChildNodes.Remove(tvCOA.SelectedNode.Value);
            }

            foreach (DataRow dr in pDataTble.Rows)
            {
                nodeLoc = new TreeNode();
                nodeLoc.Text = dr["AccountName"].ToString();
                nodeLoc.Value = dr["AccountId"].ToString();
                nodeLoc.ToolTip = dr["AccountCode"].ToString();

                if (tvCOA.SelectedNode == null)
                {
                    tvCOA.Nodes.Add(nodeLoc);
                }
                else
                {
                    tvCOA.SelectedNode.ChildNodes.Add(nodeLoc);
                }
            }
        }

        private void AddSingleNodeList(int AccountId, string AccountCode, string AccountName)
        {
            TreeNode nodeList = new TreeNode();
            if (tvCOA.SelectedNode == null)
            {
                tvCOA.Nodes.Add(nodeList);
            }
            else
            {
                nodeList.Text = AccountName;
                nodeList.Value = AccountId.ToString();
                nodeList.ToolTip = AccountCode;

                if (txtParentAccountCode.Text.Length.Equals(2))
                {
                    //nodeList.Text = "<font color = #5d88bf><strong>" + nodeList.Text + "</strong></font>";
                    nodeList.ImageUrl = "../img/squareblue.gif";
                }
                else if (txtParentAccountCode.Text.Length.Equals(5))
                    nodeList.ImageUrl = "../img/squaregreen.gif";

                tvCOA.SelectedNode.ChildNodes.Add(nodeList);
            }
        }

        private void javaScriptCall(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('" + message + "');", true);
        }

        #endregion
    }
}
