using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ERP.Utilities;

namespace Web.Modules.Setups
{
    public partial class ReportViewerSetups : System.Web.UI.Page
    {
        static ReportDocument RptDoc = null;
        string s_ReportId = string.Empty, ReportTitle = string.Empty;
        SetupsReportSchema rs = new SetupsReportSchema();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (Session["SelectionFormula"] == null)
            {
                Response.Redirect("SetupsReportMenu.aspx");
            }

            RptDoc = new ReportDocument();

            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings[ApplicationKeys.SqlConnectionString].ConnectionString);
            sqlConn.Open();

            string ReportId = string.Empty, Query = string.Empty, WhereClause = string.Empty, tblName = string.Empty;
            int value;

            if (Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"] != "" && int.TryParse(Request.QueryString["ReportId"].ToString(), out value))
            {
                hfReportId.Value = Request.QueryString["ReportId"].ToString().Trim();
                ReportId = hfReportId.Value;

                SqlCommand objSqlCommand;

                if (ReportId.Equals(SetupsReportParameter.UserInformation))
                {
                    ReportTitle = "User Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptUserList where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.UserMaster.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\UserList.rpt";
                    tblName = "UserMaster";
                }

                else if (ReportId.Equals(SetupsReportParameter.ProductInformation))
                {
                    ReportTitle = "Product Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptProduct where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Products.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\ProductList.rpt";
                    tblName = "Products";
                }

                else if (ReportId.Equals(SetupsReportParameter.ProductRatesInformation))
                {
                    ReportTitle = "Product Rates Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptProductsRate where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.ProductRates.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\ProductRatesList.rpt";
                    tblName = "ProductRates";
                }

                else if (ReportId.Equals(SetupsReportParameter.ColorInformation))
                {
                    ReportTitle = "Color Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from Color where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Color.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\ColorList.rpt";
                    tblName = "Color";
                }
                else if (ReportId.Equals(SetupsReportParameter.CurrencyInformation))
                {
                    ReportTitle = "Currency Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from Currency where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Currency.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\CurrencyList.rpt";
                    tblName = "Currency";
                }
                else if (ReportId.Equals(SetupsReportParameter.ModelNoInformation))
                {
                    ReportTitle = "Model No. Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from ModelNo where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.ModelNo.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\ModelNoList.rpt";
                    tblName = "ModelNo";
                }
                else if (ReportId.Equals(SetupsReportParameter.InsuranceInformation))
                {
                    ReportTitle = "Insurance Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptInsurance where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Insurance.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\InsuranceList.rpt";
                    tblName = "Insurance";
                }
                else if (ReportId.Equals(SetupsReportParameter.BankInformation))
                {
                    ReportTitle = "Bank Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptBank where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Bank.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\BankList.rpt";
                    tblName = "Bank";
                }
                else if (ReportId.Equals(SetupsReportParameter.VendorInformation))
                {
                    ReportTitle = "Vendor Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptVendors where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Vendors.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\VendorList.rpt";
                    tblName = "Vendors";
                }
                else if (ReportId.Equals(SetupsReportParameter.PortInformation))
                {
                    ReportTitle = "Port Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptPort where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Port.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\PortList.rpt";
                    tblName = "Port";
                }

                else if (ReportId.Equals(SetupsReportParameter.CompanyInformation))
                {
                    ReportTitle = "Company Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from Company where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Company.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\CompanyList.rpt";
                    tblName = "Company";
                }

                else if (ReportId.Equals(SetupsReportParameter.BranchInformation))
                {
                    ReportTitle = "Branch Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptBranch where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Branch.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\BranchList.rpt";
                    tblName = "Branch";
                }
                else if (ReportId.Equals(SetupsReportParameter.WarehouseInformation))
                {
                    ReportTitle = "Warehouse Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptWareHouse where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Warehouse.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\WarehouseList.rpt";
                    tblName = "Warehouse";
                }
                else if (ReportId.Equals(SetupsReportParameter.LocationInformation))
                {
                    ReportTitle = "Location Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from vwrptLocation where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Location.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\LocationsList.rpt";
                    tblName = "Location";
                }
                else if (ReportId.Equals(SetupsReportParameter.CourierInformation))
                {
                    ReportTitle = "Courier Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from CourierCompany where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.CourierCompany.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\CourierCompanyList.rpt";
                    tblName = "CourierCompany";
                }
                else if (ReportId.Equals(SetupsReportParameter.ZoneInformation))
                {
                    ReportTitle = "Zone Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from Zone where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Zone.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\ZoneList.rpt";
                    tblName = "Zone";
                }
                else if (ReportId.Equals(SetupsReportParameter.BrandInformation))
                {
                    ReportTitle = "Brand Information";
                    WhereClause = Session["SelectionFormula"].ToString();
                    Query = "select * from Brands where " + WhereClause + "";

                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.Brands.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\BrandList.rpt";
                    tblName = "Brands";
                }
                else if (ReportId.Equals(SetupsReportParameter.CityInformation))
                {
                    ReportTitle = "City Information";
                    WhereClause = Session["SelectionFormula"].ToString();

                    Query = "SELECT CountryId, Name, CountryParentId FROM Country WHERE CountryParentId=1 and " + WhereClause + "";
                    objSqlCommand = new SqlCommand(Query, sqlConn);
                    rs.City.Load(objSqlCommand.ExecuteReader());

                    crSource.Report.FileName = @"Reports\CityList.rpt";
                    tblName = "City";
                }
                sqlConn.Close();
                sqlConn.Dispose();

                RptDoc = crSource.ReportDocument;
                RptDoc.SetDataSource(rs.Tables[tblName]);

                #region Formula Fields

                if (ReportId.Equals(SetupsReportParameter.UserInformation))
                {
                    if (Request.QueryString["UserName"] != null && Request.QueryString["UserName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["UserNameOne"].Text = string.Format("'{0}'", Request.QueryString["UserName"].ToString().Trim());
                    if (Request.QueryString["CompanyName"] != null && Request.QueryString["CompanyName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["CompanyNameOne"].Text = string.Format("'{0}'", Request.QueryString["CompanyName"].ToString().Trim());
                    if (Request.QueryString["BranchName"] != null && Request.QueryString["BranchName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["BranchNameOne"].Text = string.Format("'{0}'", Request.QueryString["BranchName"].ToString().Trim());
                }
               else if (ReportId.Equals(SetupsReportParameter.ProductInformation))
                {
                    if (Request.QueryString["ProductName"] != null && Request.QueryString["ProductName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ProductName"].Text = string.Format("'{0}'", Request.QueryString["ProductName"].ToString().Trim());
                    if (Request.QueryString["VendorName"] != null && Request.QueryString["VendorName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["VendorName"].Text = string.Format("'{0}'", Request.QueryString["VendorName"].ToString().Trim());
                    if (Request.QueryString["ModelNo"] != null && Request.QueryString["ModelNo"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ModelNo"].Text = string.Format("'{0}'", Request.QueryString["ModelNo"].ToString().Trim());
                    if (Request.QueryString["ColorName"] != null && Request.QueryString["ColorName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ColorName"].Text = string.Format("'{0}'", Request.QueryString["ColorName"].ToString().Trim());
                    if (Request.QueryString["FTA"] != null && Request.QueryString["FTA"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["FTA_para"].Text = string.Format("'{0}'", Request.QueryString["FTA"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.ProductRatesInformation))
                {
                    if (Request.QueryString["ProductName"] != null && Request.QueryString["ProductName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ProductName"].Text = string.Format("'{0}'", Request.QueryString["ProductName"].ToString().Trim());
                    if (Request.QueryString["BrandRates"] != null && Request.QueryString["BrandRates"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["BrandRates"].Text = string.Format("'{0}'", Request.QueryString["BrandRates"].ToString().Trim());
                    if (Request.QueryString["ModelRates"] != null && Request.QueryString["ModelRates"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ModelRates"].Text = string.Format("'{0}'", Request.QueryString["ModelRates"].ToString().Trim());
                    if (Request.QueryString["ColorRates"] != null && Request.QueryString["ColorRates"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ColorRates"].Text = string.Format("'{0}'", Request.QueryString["ColorRates"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.ColorInformation))
                {
                    if (Request.QueryString["ColorName"] != null && Request.QueryString["ColorName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ColorName"].Text = string.Format("'{0}'", Request.QueryString["ColorName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.CurrencyInformation))
                {
                    if (Request.QueryString["CurrencyName"] != null && Request.QueryString["CurrencyName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["CurrencyName"].Text = string.Format("'{0}'", Request.QueryString["CurrencyName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.ModelNoInformation))
                {
                    if (Request.QueryString["ModelNo"] != null && Request.QueryString["ModelNo"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ModelNo"].Text = string.Format("'{0}'", Request.QueryString["ModelNo"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.InsuranceInformation))
                {
                    if (Request.QueryString["InsuranceName"] != null && Request.QueryString["InsuranceName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["InsuranceName"].Text = string.Format("'{0}'", Request.QueryString["InsuranceName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.BankInformation))
                {
                    if (Request.QueryString["BankType"] != null && Request.QueryString["BankType"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["BankType"].Text = string.Format("'{0}'", Request.QueryString["BankType"].ToString().Trim());
                    if (Request.QueryString["BankName"] != null && Request.QueryString["BankName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["BankName"].Text = string.Format("'{0}'", Request.QueryString["BankName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.VendorInformation))
                {
                    if (Request.QueryString["VendorName"] != null && Request.QueryString["VendorName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["VendorName"].Text = string.Format("'{0}'", Request.QueryString["VendorName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.PortInformation))
                {
                    if (Request.QueryString["PortName"] != null && Request.QueryString["PortName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["PortName"].Text = string.Format("'{0}'", Request.QueryString["PortName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.CompanyInformation))
                {
                    if (Request.QueryString["CompanyName"] != null && Request.QueryString["CompanyName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["CompanyNameOne"].Text = string.Format("'{0}'", Request.QueryString["CompanyName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.BranchInformation))
                {
                    if (Request.QueryString["ComapnyName"] != null && Request.QueryString["ComapnyName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ComapnyNameOne"].Text = string.Format("'{0}'", Request.QueryString["ComapnyName"].ToString().Trim());
                    if (Request.QueryString["BranchName"] != null && Request.QueryString["BranchName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["BranchNameOne"].Text = string.Format("'{0}'", Request.QueryString["BranchName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.WarehouseInformation))
                {
                    if (Request.QueryString["WarehouseName"] != null && Request.QueryString["WarehouseName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["WarehouseName"].Text = string.Format("'{0}'", Request.QueryString["WarehouseName"].ToString().Trim());
                    if (Request.QueryString["Branch_Warehouse"] != null && Request.QueryString["WarehouseName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["Branch_Warehouse"].Text = string.Format("'{0}'", Request.QueryString["Branch_Warehouse"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.LocationInformation))
                {
                    if (Request.QueryString["LocationName"] != null && Request.QueryString["LocationName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["LocationNameOne"].Text = string.Format("'{0}'", Request.QueryString["LocationName"].ToString().Trim());
                    if (Request.QueryString["Branch_Location"] != null && Request.QueryString["Branch_Location"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["Branch_Location"].Text = string.Format("'{0}'", Request.QueryString["Branch_Location"].ToString().Trim());
                    if (Request.QueryString["Company_Location"] != null && Request.QueryString["Company_Location"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["Company_Location"].Text = string.Format("'{0}'", Request.QueryString["Company_Location"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.CourierInformation))
                {
                    if (Request.QueryString["CourierName"] != null && Request.QueryString["CourierName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["CourierName"].Text = string.Format("'{0}'", Request.QueryString["CourierName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.ZoneInformation))
                {
                    if (Request.QueryString["ZoneInfo"] != null && Request.QueryString["ZoneInfo"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["ZoneInfo"].Text = string.Format("'{0}'", Request.QueryString["ZoneInfo"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.BrandInformation))
                {
                    if (Request.QueryString["BrandName"] != null && Request.QueryString["BrandName"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["BrandName"].Text = string.Format("'{0}'", Request.QueryString["BrandName"].ToString().Trim());
                }

                else if (ReportId.Equals(SetupsReportParameter.CityInformation))
                {
                    if (Request.QueryString["City"] != null && Request.QueryString["City"].ToString().Trim() != string.Empty)
                        RptDoc.DataDefinition.FormulaFields["City"].Text = string.Format("'{0}'", Request.QueryString["City"].ToString().Trim());
                }

                RptDoc.DataDefinition.FormulaFields["CompanyName"].Text = string.Format("'{0}'", Session["CompanyName"].ToString());
                RptDoc.DataDefinition.FormulaFields["BranchName"].Text = string.Format("'{0}'", Session["BranchName"].ToString());
                RptDoc.DataDefinition.FormulaFields["LocationName"].Text = string.Format("'{0}'", Session["LocationName"].ToString());
                RptDoc.DataDefinition.FormulaFields["UserName"].Text = string.Format("'{0}'", Session["UserName"].ToString());
                RptDoc.DataDefinition.FormulaFields["ReportTitle"].Text = string.Format("'{0}'", ReportTitle);

                #endregion

                crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                crViewer.ReportSource = RptDoc;
                crViewer.RefreshReport();
                crViewer.DataBind();
            }
        }
    }
}