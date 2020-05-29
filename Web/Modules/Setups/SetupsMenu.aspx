<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetupsMenu.aspx.cs" Inherits="ERPWeb.SetupsMenu"
    MasterPageFile="~/Modules/MasterPage.Master" %>

<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
            <div class="Menu_main_heading">
                <h1>
                    Administration / Setups</h1>
            </div>
            <!-- start home flow chart tabs -->
            <div class="home_flow_chart_tabs">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="34%" height="101" align="center" valign="middle">
                            <a href="../Setups/ChangePassword.aspx" onmouseover="document.changepsswd.src='../img/changeppswdblack.png'"
                                onmouseout="document.changepsswd.src='../img/changeppswdgreen.png'">
                                <img src="../img/changeppswdgreen.png" alt="Change Password" border="0" name="changepsswd" /></a>
                        </td>
                        <td width="35%" valign="middle" align="center">
                            <a href="../Setups/UserMasterList.aspx" onmouseover="document.userinformation.src='../img/userinfoblack.png'"
                                onmouseout="document.userinformation.src='../img/userinfogreen.png'">
                                <img src="../img/userinfogreen.png" name="userinformation" alt="User Information"
                                    border="0" /></a>
                        </td>
                        <td width="31%" valign="middle" align="center">
                            <a href="../Setups/UserRolesList.aspx" onmouseover="document.userroles.src='../img/userrateblack.png'"
                                onmouseout="document.userroles.src='../img/userrategreen.png'">
                                <img src="../img/userrategreen.png" name="userroles" alt="User Roles" border="0" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle" style="visibility:hidden">
                            <a href="../Setups/ProductsList.aspx" onmouseover="document.products.src='../img/productsblack.png'"
                                onmouseout="document.products.src='../img/productsgreen.png'">
                                <img src="../img/productsgreen.png" name="products" alt="Products" border="0" /></a>
                        </td>
                        <td valign="middle" align="center" style="visibility:hidden">
                            <a href="../Setups/ProductRatesList.aspx" onmouseover="document.Product_Rate.src='../img/productrateblack.png'"
                                onmouseout="document.Product_Rate.src='../img/productrategreen.png'">
                                <img src="../img/productrategreen.png" alt="Product Rate" name="Product_Rate" border="0"
                                    id="Product Rate" /></a>
                        </td>
                        <td valign="middle" align="center" style="visibility:hidden">
                            <a href="../Setups/ColorList.aspx" onmouseover="document.colors.src='../img/colorsblack.png'"
                                onmouseout="document.colors.src='../img/colorsgreen.png'">
                                <img src="../img/colorsgreen.png" alt="Colors" name="colors" border="0" id="colors" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle" style="visibility:hidden">
                            <a href="../Setups/CurrencyList.aspx" onmouseover="document.currency.src='../img/currencyblack.png'"
                                onmouseout="document.currency.src='../img/currencygreen.png'">
                                <img src="../img/currencygreen.png" alt="Currency" name="currency" border="0" id="currency" /></a>
                        </td>
                        <td valign="middle" align="center" style="visibility:hidden">
                            <a href="../Setups/ModelList.aspx" onmouseover="document.Model_Numbers.src='../img/modelnumblack.png'"
                                onmouseout="document.Model_Numbers.src='../img/modelnumgreen.png'">
                                <img src="../img/modelnumgreen.png" alt="Model Numbers" name="Model_Numbers" border="0"
                                    id="Model Numbers" /></a>
                        </td>
                        <td valign="middle" align="center" style="visibility:hidden">
                            <a href="../Setups/InsurerList.aspx" onmouseover="document.Insurance_Company.src='../img/insublack.png'"
                                onmouseout="document.Insurance_Company.src='../img/insugreen.png'">
                                <img src="../img/insugreen.png" alt="Insurance Company" name="Insurance_Company"
                                    border="0" id="Insurance Company" /></a>
                        </td>
                    </tr>
                    <tr style="visibility:hidden">
                        <td height="101" align="center" valign="middle" style="visibility:hidden">
                            <a href="../Setups/BankList.aspx" onmouseover="document.banks.src='../img/banksblack.png'"
                                onmouseout="document.banks.src='../img/banksgreen.png'">
                                <img src="../img/banksgreen.png" alt="Banks" name="banks" border="0" id="banks" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Setups/VendorsList.aspx" onmouseover="document.exporters.src='../img/exportersblack.png'"
                                onmouseout="document.exporters.src='../img/exportersgreen.png'">
                                <img src="../img/exportersgreen.png" name="exporters" alt="Exporters" border="0" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <%--  <a href="../Setups/CustomerList.aspx" onmouseover="document.customer.src='../img/customerblack.png'"
                                onmouseout="document.customer.src='../img/customergreen.png'">
                                <img src="../img/customergreen.png" name="customer" alt="Customers" border="0" /></a>--%>
                            <a href="../Setups/PortList.aspx" onmouseover="document.Port_Info.src='../img/portinfoblack.png'"
                                onmouseout="document.Port_Info.src='../img/portinfogreen.png'">
                                <img src="../img/portinfogreen.png" alt="Port Information" name="Port_Info" border="0"
                                    id="Port Information" /></a>
                        </td>
                    </tr>
                   <!-- 
                    <tr style="visibility:hidden">
                        <td height="101" align="center" valign="middle">
                            <a href="../Setups/CompanyList.aspx" onmouseover="document.Company_Info.src='../img/Comapnyblack.png'"
                                onmouseout="document.Company_Info.src='../img/Companygreen.png'">
                                <img src="../img/Companygreen.png" alt="Company Information" name="Company_Info"
                                    border="0" id="Company" /></a>
                        </td>
                        <td height="101" align="center" valign="middle">
                            <a href="../Setups/BranchList.aspx" onmouseover="document.branch.src='../img/branchblack.png'"
                                onmouseout="document.branch.src='../img/branchgreen.png'">
                                <img src="../img/branchgreen.png" name="branch" alt="Branch" border="0" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Setups/WarehouseList.aspx" onmouseover="document.Warehouse.src='../img/warehouseblack.png'"
                                onmouseout="document.Warehouse.src='../img/warehousegreen.png'">
                                <img src="../img/warehousegreen.png" name="Warehouse" alt="Warehouse Information"
                                    border="0" /></a>
                        </td>
                    </tr>
                    <tr style="visibility:hidden">
                        <td align="center" height="101" valign="middle">
                            <a href="../Setups/EmailTemplates.aspx" onmouseover="document.EmailTemplates.src='../img/emailtemplatesblack.png'"
                                onmouseout="document.EmailTemplates.src='../img/emailtemplatesgreen.png'">
                                <img src="../img/emailtemplatesgreen.png" alt="Email Templates" name="EmailTemplates"
                                    border="0" id="Img1" /></a>
                        </td>
                        <td align="center" height="101" valign="middle">
                            <a href="LocationList.aspx" onmouseout="document.Location.src='../img/LocationCOAgreen.png'"
                                onmouseover="document.Location.src='../img/LocationCOAblack.png'">
                                <img id="Location" alt="Location Information" border="0" name="Location" src="../img/LocationCOAgreen.png" /></a>
                        </td>
                        <td align="center" valign="middle">
                            <a href="../Setups/CourierCompanyList.aspx" onmouseover="document.CourierCompany.src='../img/CourierCompanyblack.png'"
                                onmouseout="document.CourierCompany.src='../img/CourierCompanygreen.png'">
                                <img src="../img/CourierCompanygreen.png" alt="Courier Company" name="CourierCompany"
                                    border="0" id="Img2" /></a>
                        </td>
                    </tr>
                    <tr style="visibility:hidden">
                        <td align="center" height="101" valign="middle">
                            <a href="../Setups/ZoneList.aspx" onmouseover="document.zoneMaster.src='../img/zoneblack.png'"
                                onmouseout="document.zoneMaster.src='../img/zonegreen.png'">
                                <img src="../img/zonegreen.png" alt="zone" name="zoneMaster" border="0" id="zoneMaster" /></a>
                        </td>
                        <td align="center" height="101" valign="middle">
                            <a href="../Setups/BrandsList.aspx" onmouseover="document.Brands.src='../img/Brandsblack.png'"
                                onmouseout="document.Brands.src='../img/Brandsgreen.png'">
                                <img src="../img/Brandsgreen.png" alt="Brands" name="Brands" border="0" id="Brands" /></a>
                        </td>
                        <td align="center" valign="middle">
                            <a href="../Setups/CityList.aspx" onmouseover="document.Cityinfo.src='../img/Cityinfoblack.png'"
                                onmouseout="document.Cityinfo.src='../img/Cityinfogreen.png'">
                                <img src="../img/Cityinfogreen.png" alt="City Info" name="Cityinfo" border="0" id="Cityinfo" /></a>
                        </td>
                    </tr>
                    <tr style="visibility:hidden">
                        <td align="center" height="101" valign="middle">
                            <a href="../Setups/SetupsReportMenu.aspx" onmouseout="document.SetupsReports.src='../img/ImportReportgreen.png'"
                                onmouseover="document.SetupsReports.src='../img/ImportReportblack.png'">
                                <img alt="Setups Reports" border="0" name="SetupsReports" src="../img/ImportReportgreen.png" /></a>
                        </td>
                        <td align="center" height="101" valign="middle">
                            &nbsp;
                        </td>
                        <td align="center" valign="middle">
                            &nbsp;
                        </td>
                    </tr>
                
                    -->
                </table>
            </div>
            <!-- end home flow chart tabs -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
