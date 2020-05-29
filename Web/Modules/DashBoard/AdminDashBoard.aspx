<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashBoard.aspx.cs"
    Inherits="ERPWeb.AdminDashBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ERP Application</title>
    <link rel="stylesheet" type="text/css" href="../css/main.css">
    <link rel="stylesheet" type="text/css" href="jqtransformplugin/jqtransform.css" />
    <link rel="stylesheet" type="text/css" href="formValidator/validationEngine.jquery.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="home_wrap">
        <!-- start header section -->
        <div class="home_header">
            <div class="home_logo">
                <img src="../img/BrowserPreview_02.png" alt="Alkhozama" />
            </div>
            <div class="home_Welcome">
                <strong>COMPANY : </strong>
                <asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal>
                &nbsp;&nbsp; <strong>BRANCH : </strong>
                <asp:Literal ID="ltrBranchName" runat="server"></asp:Literal>

<%--                &nbsp;&nbsp; <strong>LOCATION : </strong>
                <asp:Literal ID="ltrLocationName" runat="server"></asp:Literal>--%>
                <br />
                <strong>USER : </strong>
                <asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
            </div>
        </div>
         
        <!-- end header section -->
        <!-- start left col -->
        <div class="whitebg">
            <div class="rigth_col">
                <div class="rigth_col_main_heading">
                    <h1>
                        Welcome to Alkhozama</h1>
                    <h2>
                        Enterprise Resource Planing Solution [<em>ERP</em>]</h2>
                </div>
                <!-- start home flow chart tabs -->
                <div class="home_flow_chart_tabs">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="34%" valign="middle" align="center">
                                <a href="../Setups/SetupsMenu.aspx" onmouseover="document.adminico.src='../img/BrowserPreview_03_hover.png'"
                                    onmouseout="document.adminico.src='../img/home_tabs_BrowserPreview_03.png'">
                                    <img src="../img/home_tabs_BrowserPreview_03.png" alt="Administration - Manage your admin settings -"
                                        border="0" name="adminico" /></a>
                            </td>
                            <td width="35%" valign="middle" align="center">
<%--                                <a href="../Imports/ImportMenu.aspx" onmouseover="document.purchase.src='../img/home_tabs_BrowserPreview_05_hover.png'"--%>
                                    <a href="#" onmouseover="document.purchase.src='../img/home_tabs_BrowserPreview_05_hover.png'"
                                    onmouseout="document.purchase.src='../img/home_tabs_BrowserPreview_05.png'">
                                    <img src="../img/home_tabs_BrowserPreview_05.png" name="purchase" alt="Purchase/Import - Manage Purchase/Import settings -"
                                        border="0" /></a>
                            </td>
                            <td width="31%" valign="middle" align="center">
<%--                                <a href="../Sales/SalesMenu.aspx" onmouseover="document.sales.src='../img/home_tabs_BrowserPreview_07_hover.png'"--%>
                                    <a href="#" onmouseover="document.sales.src='../img/home_tabs_BrowserPreview_07_hover.png'"
                                    onmouseout="document.sales.src='../img/home_tabs_BrowserPreview_07.png'">
                                    <img src="../img/home_tabs_BrowserPreview_07.png" name="sales" alt="Sales - Manage Sales settings -"
                                        border="0" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" align="center">
                                <a href="../Accounts/AccountMenu.aspx" onmouseover="document.Financial.src='../img/home_tabs_BrowserPreview_15_hover.png'"
                                    onmouseout="document.Financial.src='../img/home_tabs_BrowserPreview_15.png'">
                                    <img src="../img/home_tabs_BrowserPreview_15.png" name="Financial" alt="Financial Setting - Manage your -"
                                        border="0" /></a>
                            </td>
                            <td valign="middle" align="center">
<%--                                <a href="../Inventory/InventoryMenu.aspx" onmouseover="document.Inventory.src='../img/home_tabs_BrowserPreview_10_hover.png'"--%>
                                    <a href="#" onmouseover="document.Inventory.src='../img/home_tabs_BrowserPreview_10_hover.png'"
                                    onmouseout="document.Inventory.src='../img/home_tabs_BrowserPreview_10.png'">
                                    <img src="../img/home_tabs_BrowserPreview_10.png" name="Inventory" alt="Inventory Setting - Manage Inventory -"
                                        border="0" /></a>
                            </td>
                            <td valign="bottom" align="center">
                                <a href="#" onmouseover="document.HumanResource.src='../img/home_tabs_BrowserPreview_13_hover.png'"
                                    onmouseout="document.HumanResource.src='../img/home_tabs_BrowserPreview_13.png'">
                                    <img src="../img/home_tabs_BrowserPreview_13.png" name="HumanResource" alt="Human Resource - Manage Sales settings -"
                                        border="0" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle">
                                &nbsp;
                            </td>
                            <td valign="middle" align="center">
                                <a href="../../Logout.aspx" onmouseover="document.LognOut.src='../img/home_tabs_BrowserPreview_19_hover.png'"
                                    onmouseout="document.LognOut.src='../img/home_tabs_BrowserPreview_19.png'">
                                    <img src="../img/home_tabs_BrowserPreview_19.png" name="LognOut" alt="Please LognOut - Please click here to logout account -"
                                        border="0" /></a>
                            </td>
                            <td valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- end home flow chart tabs -->
                <!-- end home flow chart tabs -->
            </div>
            <!-- end rigth col -->
        </div>
    </div>
    <div class="powered_by">
        Powered by TeleNoc
    </div>
    <!-- End Home Page -->
    </form>
</body>
</html>
