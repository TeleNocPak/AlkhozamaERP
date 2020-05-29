<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportParameterSetups.aspx.cs"
    Inherits="ERPWeb.ReportParameterSetups" MasterPageFile="~/Modules/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<script type="text/javascript" language="javascript">
    function GetProductID(source, eventArgs) {
        document.getElementById("<%=hfProductIDAutoCompleted.ClientID%>").value = eventArgs.get_value();
    }
</script>    
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="5" cellspacing="5">
                <tr>
                    <td>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    <asp:ValidationSummary ID="vldSummary" runat="server" CssClass="notification error"
                                        DisplayMode="List" ForeColor="" ValidationGroup="Group1" Width="92%" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="notification error"
                                        DisplayMode="List" ForeColor="" ValidationGroup="Group2" Width="92%" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:MultiView ID="mvReports" runat="server" ActiveViewIndex="0">
                                        <%-- Start User Information  --%>
                                        <asp:View ID="vwUserInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        User Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                User Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlUserName" runat="server" Width="218px" CssClass="inputtext">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                                Company
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlCompanyUser" runat="server" Width="218px" CssClass="inputtext"
                                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Branch
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlBranchUser" runat="server" CssClass="inputtext" Width="218px"
                                                                                    Enabled="False">
                                                                                    <asp:ListItem Value="0">ALL</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Products Information  --%>
                                        <asp:View ID="vwProductInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Product Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Product Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:TextBox ID="txtProduct" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                <cc1:AutoCompleteExtender ID="txtProductAutoComplete" runat="server" DelimiterCharacters=""
                                                                                    Enabled="True" MinimumPrefixLength="3" ServiceMethod="GetProductsList" ServicePath=""
                                                                                    TargetControlID="txtProduct" UseContextKey="True" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                                                                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                                    CompletionListCssClass="AutoCompleteExtender_CompletionList" OnClientItemSelected="GetProductID"
                                                                                    CompletionInterval="1" FirstRowSelected="true" EnableCaching="true">
                                                                                </cc1:AutoCompleteExtender>
                                                                            </td>
                                                                            <td width="160px">
                                                                                Brand
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlBrandProduct" runat="server" CssClass="inputtext" Width="218px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Model
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlModelProduct" runat="server" CssClass="inputtext" Width="218px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                                Color
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlColorProduct" runat="server" CssClass="inputtext" Width="218px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="180px">
                                                                                FTA
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:CheckBox ID="chkFTA" runat="server" Text="" />
                                                                            </td>
                                                                            <td width="160px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Product Rates Information  --%>
                                        <asp:View ID="vwProductRatesInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Product Rates Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Product Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:TextBox ID="txtProduct2" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                                    Enabled="True" MinimumPrefixLength="3" ServiceMethod="GetProductsList" ServicePath=""
                                                                                    TargetControlID="txtProduct2" UseContextKey="True" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                                                                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                                    CompletionListCssClass="AutoCompleteExtender_CompletionList" OnClientItemSelected="GetProductID"
                                                                                    CompletionInterval="1" FirstRowSelected="true" EnableCaching="true">
                                                                                </cc1:AutoCompleteExtender>
                                                                            </td>
                                                                            <td width="160px">
                                                                                Brand
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlBrandRates" runat="server" CssClass="inputtext" Width="218px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Model
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlModelRates" runat="server" CssClass="inputtext" Width="218px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                                Color
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlColorRates" runat="server" CssClass="inputtext" Width="218px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Color Information  --%>
                                        <asp:View ID="vwColorInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Color Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Color Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlColorInfo" runat="server" Width="218px" CssClass="inputtext">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Currency Information  --%>
                                        <asp:View ID="vwCurrencyInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Currency Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Currency Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlCurrencyInfo" runat="server" Width="218px" CssClass="inputtext">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Model No. Information  --%>
                                        <asp:View ID="vwModelNoInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Model No. Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Model No.
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlModelNo" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Insurance  Information  --%>
                                        <asp:View ID="vwInsuranceInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Insurance Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Insurance Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlInsuranceName" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Bank Information  --%>
                                        <asp:View ID="vwBankInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Bank Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="90px">
                                                                                Bank
                                                                            </td>
                                                                            <td width="310px">
                                                                                <asp:DropDownList ID="ddlBank" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="90px">
                                                                                Bank Type
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlBankType" runat="server" Width="218px" CssClass="inputtext">
                                                                                    <asp:ListItem Value="0">ALL</asp:ListItem>
                                                                                    <asp:ListItem Value="Local">LOCAL</asp:ListItem>
                                                                                    <asp:ListItem Value="Vendor">VENDOR</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Vendor (Exporter) Information  --%>
                                        <asp:View ID="vwVendor_Exporter_Info" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Vendor (Exporter) Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Vendor Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlVendorName" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Port Information  --%>
                                        <asp:View ID="vwPortInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Port Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Port Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlPortName" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Company Information  --%>
                                        <asp:View ID="vwCompanyInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Company Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Company Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlCompanyInfo" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Branch Information  --%>
                                        <asp:View ID="vwBranchInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Branch Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Branch Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlBranchInfo" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                                Company
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlComapny_Branch" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Warehouse Information  --%>
                                        <asp:View ID="vwWarehouseInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Warehouse Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Warehouse
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlWarehouse" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                                Branch
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlBranch_WareHouse" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Location Information  --%>
                                        <asp:View ID="vwLocationInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Location Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Location Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlLocationName" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                                Branch
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlBranch_Location" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Company
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlCompany_Location" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Courier Information  --%>
                                        <asp:View ID="vwCourierInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Courier Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Courier Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlCourierName" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Zone Information  --%>
                                        <asp:View ID="vwZoneInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Zone Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Zone Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlZoneInfo" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <%-- Start Brand Information  --%>
                                        <asp:View ID="vwBrandInfo" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        Brand Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                Brand Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlBrandName" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                         <%-- City Information  --%>
                                        <asp:View ID="vwCity" runat="server">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td class="Page_Heading" align="left">
                                                                    <h1>
                                                                        City Information</h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 9px">
                                                        <table border="0" cellpadding="5" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="180px">
                                                                                City Name
                                                                            </td>
                                                                            <td width="280px">
                                                                                <asp:DropDownList ID="ddlCity" runat="server" Width="204px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="160px">
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td width="20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="20%">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btnPreviewBank" runat="server" CssClass="btninput" Text="Preview"
                                                    Width="100px" OnClick="btnPreviewBank_Click" />&nbsp;
                                                <asp:Button ID="btnCancelBank" runat="server" CssClass="btninput" Text="Cancel" Width="100px"
                                                    OnClick="btnCancelBank_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfReportId" runat="server" />
                        <asp:HiddenField ID="hfProductIDAutoCompleted" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
