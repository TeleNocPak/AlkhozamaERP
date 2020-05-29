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
    public partial class ReportParameterSetups : System.Web.UI.Page
    {
        #region Variables

        string ErrorMessage = string.Empty, qString = string.Empty, strScript = string.Empty;
        ReportParameterSetupsBR objReportParameterSetupsBR = new ReportParameterSetupsBR();
        LetterCreditBR objLetterCreditBR = new LetterCreditBR();
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int value;
                if (Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"] != "" && int.TryParse(Request.QueryString["ReportId"].ToString(), out value))
                {                 
                    hfReportId.Value = Request.QueryString["ReportId"].ToString();
                    if (hfReportId.Value.Equals(SetupsReportParameter.UserInformation))
                    {
                        mvReports.ActiveViewIndex = 0;
                    }
                    else if (hfReportId.Value.Equals(SetupsReportParameter.ProductInformation))
                        mvReports.ActiveViewIndex = 1;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.ProductRatesInformation))
                        mvReports.ActiveViewIndex = 2;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.ColorInformation))
                        mvReports.ActiveViewIndex = 3;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.CurrencyInformation))
                        mvReports.ActiveViewIndex = 4;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.ModelNoInformation))
                        mvReports.ActiveViewIndex = 5;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.InsuranceInformation))
                        mvReports.ActiveViewIndex = 6;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.BankInformation))
                        mvReports.ActiveViewIndex = 7;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.VendorInformation))
                        mvReports.ActiveViewIndex = 8;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.PortInformation))
                        mvReports.ActiveViewIndex = 9;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.CompanyInformation))
                        mvReports.ActiveViewIndex = 10;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.BranchInformation))
                        mvReports.ActiveViewIndex = 11;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.WarehouseInformation))
                        mvReports.ActiveViewIndex = 12;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.LocationInformation))
                        mvReports.ActiveViewIndex = 13;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.CourierInformation))
                        mvReports.ActiveViewIndex = 14;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.ZoneInformation))
                        mvReports.ActiveViewIndex = 15;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.BrandInformation))
                        mvReports.ActiveViewIndex = 16;
                    else if (hfReportId.Value.Equals(SetupsReportParameter.CityInformation))
                        mvReports.ActiveViewIndex = 17;

                    GetSetupsData(hfReportId.Value);
                }
                else
                    Response.Redirect("SetupsReportMenu.aspx");
            }
        }

        protected void btnPreviewBank_Click(object sender, EventArgs e)
        {
            if (hfReportId.Value.Equals(SetupsReportParameter.UserInformation))
            {
                string SearchText = "1=1";
                if (ddlUserName.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and UserId='" + ddlUserName.SelectedValue + "'";

                if (ddlCompanyUser.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and CompanyID='" + ddlCompanyUser.SelectedValue + "'";

                if (ddlBranchUser.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and BranchID='" + ddlBranchUser.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&UserName=" + ddlUserName.SelectedItem.Text;
                qString += "&CompanyName=" + ddlCompanyUser.SelectedItem.Text;
                qString += "&BranchName=" + ddlBranchUser.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.ProductInformation))
            {
                if (txtProduct.Text == string.Empty)
                {
                    hfProductIDAutoCompleted.Value = "0";                    
                }

                string SearchText = "1=1";
                if (hfProductIDAutoCompleted.Value.ToString() != "0")
                    SearchText = SearchText + " and ProductID='" + hfProductIDAutoCompleted.Value + "'";                    

                if (ddlBrandProduct.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and VendorId='" + ddlBrandProduct.SelectedValue + "'";

                if (ddlModelProduct.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and ModelId='" + ddlModelProduct.SelectedValue + "'";

                if (ddlColorProduct.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and ColorId='" + ddlColorProduct.SelectedValue + "'";

                if (chkFTA.Checked == true)
                    SearchText = SearchText + " and FTA='" + Convert.ToInt16(chkFTA.Checked) + "'";

                Session["SelectionFormula"] = SearchText;
                
                qString += "&ProductName=" + txtProduct.Text;                
                qString += "&VendorName=" + ddlBrandProduct.SelectedItem.Text;
                qString += "&ModelNo=" + ddlModelProduct.SelectedItem.Text;
                qString += "&ColorName=" + ddlColorProduct.SelectedItem.Text;
                qString += chkFTA.Checked.Equals(true) ? "&FTA=Yes" : "&FTA=No";                
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.ProductRatesInformation))
            {
                if (txtProduct2.Text == string.Empty)
                {
                    hfProductIDAutoCompleted.Value = "0";                    
                }
                string SearchText = "1=1";
                if (hfProductIDAutoCompleted.Value.ToString() != "0")
                    SearchText = SearchText + " and ProductID='" + hfProductIDAutoCompleted.Value + "'";                

                if (ddlBrandRates.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and VendorId='" + ddlBrandRates.SelectedValue + "'";

                if (ddlModelRates.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and ModelId='" + ddlModelRates.SelectedValue + "'";

                if (ddlColorRates.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and ColorId='" + ddlColorRates.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&ProductName=" + txtProduct2.Text;
                qString += "&BrandRates=" + ddlBrandRates.SelectedItem.Text;
                qString += "&ModelRates=" + ddlModelRates.SelectedItem.Text;
                qString += "&ColorRates=" + ddlColorRates.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.ColorInformation))
            {
                string SearchText = "1=1";
                if (ddlColorInfo.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and ColorID='" + ddlColorInfo.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&ColorName=" + ddlColorInfo.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.CurrencyInformation))
            {
                string SearchText = "1=1";
                if (ddlCurrencyInfo.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and CurrencyID='" + ddlCurrencyInfo.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&CurrencyName=" + ddlCurrencyInfo.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.ModelNoInformation))
            {
                string SearchText = "1=1";
                if (ddlModelNo.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and ModelID='" + ddlModelNo.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&ModelNo=" + ddlModelNo.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.InsuranceInformation))
            {
                string SearchText = "1=1";
                if (ddlInsuranceName.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and InsuranceID='" + ddlInsuranceName.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&InsuranceName=" + ddlInsuranceName.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.BankInformation))
            {
                string SearchText = "1=1";
                if (ddlBank.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and BankID='" + ddlBank.SelectedValue + "'";

                if (ddlBankType.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and BankType='" + ddlBankType.SelectedItem.Text + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&BankName=" + ddlBank.SelectedItem.Text;
                qString += "&BankType=" + ddlBankType.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.VendorInformation))
            {
                string SearchText = "1=1";
                if (ddlVendorName.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and VendorID='" + ddlVendorName.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&VendorName=" + ddlVendorName.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.PortInformation))
            {
                string SearchText = "1=1";
                if (ddlPortName.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and PortID='" + ddlPortName.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&PortName=" + ddlPortName.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.CompanyInformation))
            {
                string SearchText = "1=1";
                if (ddlCompanyInfo.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and CompanyID='" + ddlCompanyInfo.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&CompanyName=" + ddlCompanyInfo.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.BranchInformation))
            {
                string SearchText = "1=1";
                if (ddlBranchInfo.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and BranchID='" + ddlBranchInfo.SelectedValue + "'";

                if (ddlComapny_Branch.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and CompanyId='" + ddlComapny_Branch.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&BranchName=" + ddlBranchInfo.SelectedItem.Text;
                qString += "&ComapnyName=" + ddlComapny_Branch.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.WarehouseInformation))
            {
                string SearchText = "1=1";
                if (ddlWarehouse.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and WarehouseID='" + ddlWarehouse.SelectedValue + "'";

                if (ddlBranch_WareHouse.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and BranchID='" + ddlBranch_WareHouse.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&WarehouseName=" + ddlWarehouse.SelectedItem.Text;
                qString += "&Branch_Warehouse=" + ddlBranch_WareHouse.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.LocationInformation))
            {
                string SearchText = "1=1";
                if (ddlLocationName.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and LocationId='" + ddlLocationName.SelectedValue + "'";

                if (ddlBranch_Location.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and BranchId='" + ddlBranch_Location.SelectedValue + "'";

                if (ddlCompany_Location.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and LocationId='" + ddlCompany_Location.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&LocationName=" + ddlLocationName.SelectedItem.Text;
                qString += "&Branch_Location=" + ddlBranch_Location.SelectedItem.Text;
                qString += "&Company_Location=" + ddlCompany_Location.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.CourierInformation))
            {
                string SearchText = "1=1";
                if (ddlCourierName.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and CourierID='" + ddlCourierName.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&CourierName=" + ddlCourierName.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.ZoneInformation))
            {
                string SearchText = "1=1";
                if (ddlZoneInfo.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and ZoneID='" + ddlZoneInfo.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&ZoneInfo=" + ddlZoneInfo.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.BrandInformation))
            {
                string SearchText = "1=1";
                if (ddlBrandName.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and BrandID='" + ddlBrandName.SelectedValue + "'";

                Session["SelectionFormula"] = SearchText;

                qString += "&BrandName=" + ddlBrandName.SelectedItem.Text;
            }
            else if (hfReportId.Value.Equals(SetupsReportParameter.CityInformation))
            {
                string SearchText = "1=1";

                if (ddlCity.SelectedValue.ToString() != "0")
                    SearchText = SearchText + " and CountryID=" + ddlCity.SelectedValue + "";

                Session["SelectionFormula"] = SearchText;

                qString += "&City=" + ddlCity.SelectedItem.Text;
            }

            strScript = "window.open('ReportViewerSetups.aspx?ReportId=" + hfReportId.Value + qString + "','Report','width=940,height=900,scrollbars=1')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ReportPopUpScript", strScript, true);
        }

        protected void btnCancelBank_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetupsReportMenu.aspx");
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCompanyUser.SelectedValue != "0")
            {
                GetBranch(Convert.ToInt32(ddlCompanyUser.SelectedValue));
                ddlBranchUser.Enabled = true;
            }
            else
            {
                ddlBranchUser.Enabled = false;
            }
        }

        #endregion

        #region Methods

        private void javaScriptCall(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('" + message + "');", true);
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetProductsList(string prefixText, int count, string contextKey)
        {
            string queryString = @"SELECT Top 20 ProductID,ShortName FROM Products WHERE ShortName
                                   LIKE '%" + prefixText.Trim().ToUpper() + "%'";

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
                adapter.Fill(ds, 0, 30, "Products");
                items = new List<string>();
                string returnString = String.Empty;

                foreach (DataRow dr in ds.Tables[0].Rows)
                    items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["ShortName"].ToString(), dr["ProductID"].ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return items.ToArray();
        }

        private void GetBranch(int CompanyId)
        {
            DataTable dataTable = objReportParameterSetupsBR.GetBranch(CompanyId);
            DataRow dr = dataTable.NewRow();
            dr["BranchId"] = 0;
            dr["BranchName"] = "ALL";
            dataTable.Rows.InsertAt(dr, 0);

            ddlBranchUser.DataSource = dataTable;
            ddlBranchUser.DataTextField = "BranchName";
            ddlBranchUser.DataValueField = "BranchId";
            ddlBranchUser.DataBind();
        }

        private void GetSetupsData(string ReportId)
        {
            DataTable dataTable;
            DataRow dr;

            if (ReportId.Equals(SetupsReportParameter.UserInformation))
            {
                dataTable = objReportParameterSetupsBR.GetUser();
                dr = dataTable.NewRow();
                dr["UserId"] = 0;
                dr["FullName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlUserName.DataSource = dataTable;
                ddlUserName.DataTextField = "FullName";
                ddlUserName.DataValueField = "UserId";
                ddlUserName.DataBind();

                dataTable = objReportParameterSetupsBR.GetCompany();
                dr = dataTable.NewRow();
                dr["CompanyID"] = 0;
                dr["CompanyName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlCompanyUser.DataSource = dataTable;
                ddlCompanyUser.DataTextField = "CompanyName";
                ddlCompanyUser.DataValueField = "CompanyID";
                ddlCompanyUser.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.ProductInformation))
            {
                dataTable = objReportParameterSetupsBR.GetBrand();
                dr = dataTable.NewRow();
                dr["BrandID"] = 0;
                dr["BrandName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlBrandProduct.DataSource = dataTable;
                ddlBrandProduct.DataTextField = "BrandName";
                ddlBrandProduct.DataValueField = "BrandID";
                ddlBrandProduct.DataBind();

                dataTable = objReportParameterSetupsBR.GetModel();
                dr = dataTable.NewRow();
                dr["ModelID"] = 0;
                dr["ModelName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlModelProduct.DataSource = dataTable;
                ddlModelProduct.DataTextField = "ModelName";
                ddlModelProduct.DataValueField = "ModelID";
                ddlModelProduct.DataBind();

                dataTable = objReportParameterSetupsBR.GetColor();
                dr = dataTable.NewRow();
                dr["ColorId"] = 0;
                dr["ColorName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlColorProduct.DataSource = dataTable;
                ddlColorProduct.DataTextField = "ColorName";
                ddlColorProduct.DataValueField = "ColorId";
                ddlColorProduct.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.ProductRatesInformation))
            {
                dataTable = objReportParameterSetupsBR.GetBrand();
                dr = dataTable.NewRow();
                dr["BrandID"] = 0;
                dr["BrandName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlBrandRates.DataSource = dataTable;
                ddlBrandRates.DataTextField = "BrandName";
                ddlBrandRates.DataValueField = "BrandID";
                ddlBrandRates.DataBind();

                dataTable = objReportParameterSetupsBR.GetModel();
                dr = dataTable.NewRow();
                dr["ModelID"] = 0;
                dr["ModelName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlModelRates.DataSource = dataTable;
                ddlModelRates.DataTextField = "ModelName";
                ddlModelRates.DataValueField = "ModelID";
                ddlModelRates.DataBind();

                dataTable = objReportParameterSetupsBR.GetColor();
                dr = dataTable.NewRow();
                dr["ColorId"] = 0;
                dr["ColorName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlColorRates.DataSource = dataTable;
                ddlColorRates.DataTextField = "ColorName";
                ddlColorRates.DataValueField = "ColorId";
                ddlColorRates.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.ColorInformation))
            {
                dataTable = objReportParameterSetupsBR.GetColor();
                dr = dataTable.NewRow();
                dr["ColorId"] = 0;
                dr["ColorName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlColorInfo.DataSource = dataTable;
                ddlColorInfo.DataTextField = "ColorName";
                ddlColorInfo.DataValueField = "ColorId";
                ddlColorInfo.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.CurrencyInformation))
            {
                dataTable = objReportParameterSetupsBR.GetCurrency();
                dr = dataTable.NewRow();
                dr["CurrencyId"] = 0;
                dr["CurrencyName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlCurrencyInfo.DataSource = dataTable;
                ddlCurrencyInfo.DataTextField = "CurrencyName";
                ddlCurrencyInfo.DataValueField = "CurrencyId";
                ddlCurrencyInfo.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.ModelNoInformation))
            {
                dataTable = objReportParameterSetupsBR.GetModel();
                dr = dataTable.NewRow();
                dr["ModelID"] = 0;
                dr["ModelName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlModelNo.DataSource = dataTable;
                ddlModelNo.DataTextField = "ModelName";
                ddlModelNo.DataValueField = "ModelID";
                ddlModelNo.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.InsuranceInformation))
            {
                dataTable = objReportParameterSetupsBR.GetInsurance();
                dr = dataTable.NewRow();
                dr["InsuranceId"] = 0;
                dr["Name"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlInsuranceName.DataSource = dataTable;
                ddlInsuranceName.DataTextField = "Name";
                ddlInsuranceName.DataValueField = "InsuranceId";
                ddlInsuranceName.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.BankInformation))
            {
                dataTable = objReportParameterSetupsBR.GetBank();
                dr = dataTable.NewRow();
                dr["BankID"] = 0;
                dr["BankName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlBank.DataSource = dataTable;
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "BankID";
                ddlBank.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.VendorInformation))
            {
                dataTable = objReportParameterSetupsBR.GetVendors();
                dr = dataTable.NewRow();
                dr["VendorID"] = 0;
                dr["CompanyName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlVendorName.DataSource = dataTable;
                ddlVendorName.DataTextField = "CompanyName";
                ddlVendorName.DataValueField = "VendorID";
                ddlVendorName.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.PortInformation))
            {
                dataTable = objReportParameterSetupsBR.GetPort();
                dr = dataTable.NewRow();
                dr["PortID"] = 0;
                dr["PortName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlPortName.DataSource = dataTable;
                ddlPortName.DataTextField = "PortName";
                ddlPortName.DataValueField = "PortID";
                ddlPortName.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.CompanyInformation))
            {
                dataTable = objReportParameterSetupsBR.GetCompany();
                dr = dataTable.NewRow();
                dr["CompanyID"] = 0;
                dr["CompanyName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlCompanyInfo.DataSource = dataTable;
                ddlCompanyInfo.DataTextField = "CompanyName";
                ddlCompanyInfo.DataValueField = "CompanyID";
                ddlCompanyInfo.DataBind();
            }

            else if (ReportId.Equals(SetupsReportParameter.BranchInformation))
            {
                dataTable = objReportParameterSetupsBR.GetBranch();
                dr = dataTable.NewRow();
                dr["BranchID"] = 0;
                dr["BranchName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlBranchInfo.DataSource = dataTable;
                ddlBranchInfo.DataTextField = "BranchName";
                ddlBranchInfo.DataValueField = "BranchID";
                ddlBranchInfo.DataBind();

                dataTable = objReportParameterSetupsBR.GetCompany();
                dr = dataTable.NewRow();
                dr["CompanyID"] = 0;
                dr["CompanyName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlComapny_Branch.DataSource = dataTable;
                ddlComapny_Branch.DataTextField = "CompanyName";
                ddlComapny_Branch.DataValueField = "CompanyID";
                ddlComapny_Branch.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.WarehouseInformation))
            {
                dataTable = objReportParameterSetupsBR.GetWarehouse();
                dr = dataTable.NewRow();
                dr["WarehouseID"] = 0;
                dr["WarehouseName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlWarehouse.DataSource = dataTable;
                ddlWarehouse.DataTextField = "WarehouseName";
                ddlWarehouse.DataValueField = "WarehouseID";
                ddlWarehouse.DataBind();

                dataTable = objReportParameterSetupsBR.GetBranch();
                dr = dataTable.NewRow();
                dr["BranchID"] = 0;
                dr["BranchName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlBranch_WareHouse.DataSource = dataTable;
                ddlBranch_WareHouse.DataTextField = "BranchName";
                ddlBranch_WareHouse.DataValueField = "BranchID";
                ddlBranch_WareHouse.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.LocationInformation))
            {
                dataTable = objReportParameterSetupsBR.GetLocation();
                dr = dataTable.NewRow();
                dr["LocationID"] = 0;
                dr["LocationName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlLocationName.DataSource = dataTable;
                ddlLocationName.DataTextField = "LocationName";
                ddlLocationName.DataValueField = "LocationID";
                ddlLocationName.DataBind();

                dataTable = objReportParameterSetupsBR.GetBranch();
                dr = dataTable.NewRow();
                dr["BranchID"] = 0;
                dr["BranchName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlBranch_Location.DataSource = dataTable;
                ddlBranch_Location.DataTextField = "BranchName";
                ddlBranch_Location.DataValueField = "BranchID";
                ddlBranch_Location.DataBind();

                dataTable = objReportParameterSetupsBR.GetCompany();
                dr = dataTable.NewRow();
                dr["CompanyID"] = 0;
                dr["CompanyName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlCompany_Location.DataSource = dataTable;
                ddlCompany_Location.DataTextField = "CompanyName";
                ddlCompany_Location.DataValueField = "CompanyID";
                ddlCompany_Location.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.CourierInformation))
            {
                dataTable = objReportParameterSetupsBR.GetCourierComapny();
                dr = dataTable.NewRow();
                dr["CourierID"] = 0;
                dr["CourierName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlCourierName.DataSource = dataTable;
                ddlCourierName.DataTextField = "CourierName";
                ddlCourierName.DataValueField = "CourierID";
                ddlCourierName.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.ZoneInformation))
            {
                dataTable = objReportParameterSetupsBR.GetZone();
                dr = dataTable.NewRow();
                dr["ZoneID"] = 0;
                dr["ZoneName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlZoneInfo.DataSource = dataTable;
                ddlZoneInfo.DataTextField = "ZoneName";
                ddlZoneInfo.DataValueField = "ZoneID";
                ddlZoneInfo.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.BrandInformation))
            {
                dataTable = objReportParameterSetupsBR.GetBrand();
                dr = dataTable.NewRow();
                dr["BrandID"] = 0;
                dr["BrandName"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlBrandName.DataSource = dataTable;
                ddlBrandName.DataTextField = "BrandName";
                ddlBrandName.DataValueField = "BrandID";
                ddlBrandName.DataBind();
            }
            else if (ReportId.Equals(SetupsReportParameter.CityInformation))
            {
                dataTable = objReportParameterSetupsBR.GetCity();
                dr = dataTable.NewRow();
                dr["CountryId"] = 0;
                dr["Name"] = "ALL";
                dataTable.Rows.InsertAt(dr, 0);
                ddlCity.DataSource = dataTable;
                ddlCity.DataTextField = "Name";
                ddlCity.DataValueField = "CountryId";
                ddlCity.DataBind();
            }
        }

        #endregion
    }
}
