<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetupsReportMenu.aspx.cs"
    Inherits="ERPWeb.SetupsReportMenu" MasterPageFile="~/Modules/MasterPage.Master" %>

<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
            <div class="Menu_main_heading">
                <h1>
                    Setup Reports</h1>
            </div>
            <!-- start home flow chart tabs -->
            <div class="home_flow_chart_tabs">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="34%" height="101" align="center" valign="middle">
                            <a href="../Setups/ReportParameterSetups.aspx?ReportId=101" onmouseover="document.userinformation.src='../img/userinfoblack.png'"
                                onmouseout="document.userinformation.src='../img/userinfogreen.png'">
                                <img src="../img/userinfogreen.png" name="userinformation" alt="User Information"
                                    border="0" /></a>
                        </td>
                        <td width="35%" valign="middle" align="center">
                            <a href="../Setups/ReportParameterSetups.aspx?ReportId=102" onmouseover="document.products.src='../img/productsblack.png'"
                                onmouseout="document.products.src='../img/productsgreen.png'">
                                <img src="../img/productsgreen.png" name="products" alt="Products" border="0" /></a>
                        </td>
                        <td width="31%" valign="middle" align="center">
                            <a href="../Setups/ReportParameterSetups.aspx?ReportId=103" onmouseover="document.Product_Rate.src='../img/productrateblack.png'"
                                onmouseout="document.Product_Rate.src='../img/productrategreen.png'">
                                <img src="../img/productrategreen.png" alt="Product Rate" name="Product_Rate" border="0"
                                    id="Product Rate" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle">
                            <a href="../Setups/ReportParameterSetups.aspx?ReportId=104" onmouseover="document.colors.src='../img/colorsblack.png'"
                                onmouseout="document.colors.src='../img/colorsgreen.png'">
                                <img src="../img/colorsgreen.png" alt="Colors" name="colors" border="0" id="colors" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Setups/ReportParameterSetups.aspx?ReportId=105" onmouseover="document.currency.src='../img/currencyblack.png'"
                                onmouseout="document.currency.src='../img/currencygreen.png'">
                                <img src="../img/currencygreen.png" alt="Currency" name="currency" border="0" id="currency" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Setups/ReportParameterSetups.aspx?ReportId=106" onmouseover="document.Model_Numbers.src='../img/modelnumblack.png'"
                                onmouseout="document.Model_Numbers.src='../img/modelnumgreen.png'">
                                <img src="../img/modelnumgreen.png" alt="Model Numbers" name="Model_Numbers" border="0"
                                    id="Model Numbers" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle">
                            <a href="ReportParameterSetups.aspx?ReportId=107" onmouseout="document.Insurance.src='../img/insugreen.png'"
                                onmouseover="document.Insurance.src='../img/insublack.png'">
                                <img src="../img/insugreen.png" alt="Insurance Company" name="Insurance_Company"
                                    border="0" id="Insurance" />
                            </a>
                        </td>
                        <td valign="middle" align="center">
                            &nbsp;<a href="ReportParameterSetups.aspx?ReportId=108" onmouseout="document.banks.src='../img/banksgreen.png'"
                                onmouseover="document.banks.src='../img/banksblack.png'">
                                <img src="../img/banksgreen.png" alt="Banks" name="banks" border="0" id="banks" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="ReportParameterSetups.aspx?ReportId=109" onmouseout="document.exporters.src='../img/exportersgreen.png'"
                                onmouseover="document.exporters.src='../img/exportersblack.png'">
                                <img src="../img/exportersgreen.png" name="exporters" alt="Exporters" border="0" />
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="101" valign="middle">
                            <a href="ReportParameterSetups.aspx?ReportId=110" onmouseover="document.Port_Info.src='../img/portinfoblack.png'"
                                onmouseout="document.Port_Info.src='../img/portinfogreen.png'">
                                <img src="../img/portinfogreen.png" alt="Port Information" name="Port_Info" border="0"
                                    id="Port Information" /></a>
                            <td align="center" valign="middle">
                                &nbsp;<a href="ReportParameterSetups.aspx?ReportId=111" onmouseout="document.Company_Info.src='../img/Companygreen.png'"
                                    onmouseover="document.Company_Info.src='../img/Comapnyblack.png'">
                                    <img src="../img/Companygreen.png" alt="Company Information" name="Company_Info"
                                        border="0" id="Company" /></a>
                            </td>
                            <td align="center" valign="middle">
                                <a href="ReportParameterSetups.aspx?ReportId=112" onmouseout="document.branch.src='../img/branchgreen.png'"
                                    onmouseover="document.branch.src='../img/branchblack.png'">
                                    <img src="../img/branchgreen.png" name="branch" alt="Branch" border="0" />
                                </a>
                            </td>
                    </tr>
                    <tr>
                        <td align="center" height="101" valign="middle">
                            <a href="ReportParameterSetups.aspx?ReportId=113" onmouseout="document.Warehouse.src='../img/warehousegreen.png'"
                                onmouseover="document.Warehouse.src='../img/warehouseblack.png'">
                                <img src="../img/warehousegreen.png" name="Warehouse" alt="Warehouse Information"
                                    border="0" />
                            </a>
                        </td>
                        <td align="center" valign="middle">
                            <a href="ReportParameterSetups.aspx?ReportId=114" onmouseout="document.Location.src='../img/LocationCOAgreen.png'"
                                onmouseover="document.Location.src='../img/LocationCOAblack.png'">
                                <img src="../img/LocationCOAgreen.png" name="Location" alt="Location Information"
                                    border="0" id="Location" /></a>
                        </td>
                        <td align="center" valign="middle">
                            <a href="ReportParameterSetups.aspx?ReportId=115" onmouseout="document.Courier_Company.src='../img/CourierCompanygreen.png'"
                                onmouseover="document.Courier_Company.src='../img/CourierCompanyblack.png'">
                                <img src="../img/CourierCompanygreen.png" name="Courier_Company" alt="Courier Company"
                                    border="0" id="Courier Company" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="101" valign="middle">
                            <a href="ReportParameterSetups.aspx?ReportId=116" onmouseout="document.Zone.src='../img/zonegreen.png'"
                                onmouseover="document.Zone.src='../img/zoneblack.png'">
                                <img src="../img/zonegreen.png" name="Zone" alt="Zone" border="0" id="Zone" />
                            </a>
                        </td>
                        <td align="center" valign="middle">
                            <a href="ReportParameterSetups.aspx?ReportId=117" onmouseout="document.Brands.src='../img/Brandsgreen.png'"
                                onmouseover="document.Brands.src='../img/BrandsBlack.png'">
                                <img src="../img/Brandsgreen.png" name="Brands" alt="Brands" border="0" id="Brands" />
                            </a>
                        </td>
                        <td align="center" valign="middle">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <!-- end home flow chart tabs -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
