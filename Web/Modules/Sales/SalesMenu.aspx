<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesMenu.aspx.cs" Inherits="ERPWeb.SalesMenu"
    MasterPageFile="~/Modules/MasterPage.Master" %>

<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
            <div class="Menu_main_heading">
                <h1>
                    Sales</h1>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="topUpdate"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div id="progressBackgroundFilter">
                    </div>
                    <div id="processMessage">
                        <img alt="Processing" src="../img/loading_3.gif" style="vertical-align: middle" class="loadingImage" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <!-- start home flow chart tabs -->
            <div class="home_flow_chart_tabs">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="34%" height="101" align="center" valign="middle">
                            <a href="../Sales/DealerType.aspx" onmouseover="document.DealerType.src='../img/DealerTypeblack.png'"
                                onmouseout="document.DealerType.src='../img/DealerTypegreen.png'">
                                <img src="../img/DealerTypegreen.png" alt="Dealer Type" border="0" name="DealerType" /></a>
                        </td>
                        <td width="35%" valign="middle" align="center">
                            <a href="../Sales/DealerSignBoard.aspx" onmouseover="document.DealerSignBoard.src='../img/DealerSignBoardblack.png'"
                                onmouseout="document.DealerSignBoard.src='../img/DealerSignBoardgreen.png'">
                                <img src="../img/DealerSignBoardgreen.png" alt="Dealer Sign Board" border="0" name="DealerSignBoard" /></a>
                        </td>
                        <td width="31%" valign="middle" align="center">
                            <a href="../Sales/SupplierIncentive.aspx" onmouseover="document.SupplierIncentive.src='../img/SupplierIncentiveblack.png'"
                                onmouseout="document.SupplierIncentive.src='../img/SupplierIncentivegreen.png'">
                                <img src="../img/SupplierIncentivegreen.png" alt="Supplier Incentive" border="0"
                                    name="SupplierIncentive" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td width="34%" height="101" align="center" valign="middle">
                            <a href="../Sales/DealerList.aspx" onmouseover="document.Dealer.src='../img/dealerblack.png'"
                                onmouseout="document.Dealer.src='../img/dealergreen.png'">
                                <img src="../img/dealergreen.png" alt="Dealer Information" border="0" name="Dealer" /></a>
                        </td>
                        <td width="35%" valign="middle" align="center">
                            <a href="../Sales/DealerMonthlyTargetList.aspx" onmouseover="document.DealerMonthlyTarget.src='../img/dealermonthlytargetblack.png'"
                                onmouseout="document.DealerMonthlyTarget.src='../img/dealermonthlytargetgreen.png'">
                                <img src="../img/dealermonthlytargetgreen.png" alt="Dealer Monthly Target" border="0"
                                    name="DealerMonthlyTarget" /></a>
                        </td>
                        <td width="31%" valign="middle" align="center">
                            <a href="../Sales/RebateInspectionList.aspx" onmouseover="document.RebateInspection.src='../img/rebateinspectionblack.png'"
                                onmouseout="document.RebateInspection.src='../img/rebateinspectiongreen.png'">
                                <img src="../img/rebateinspectiongreen.png" alt="Rebate Inspection" border="0" name="RebateInspection" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle">
                            <a href="../Sales/PostDatedChequeList.aspx" onmouseover="document.PostDatedCheque.src='../img/postdatedchequeblack.png'"
                                onmouseout="document.PostDatedCheque.src='../img/postdatedchequegreen.png'">
                                <img src="../img/postdatedchequegreen.png" alt="Post Dated Cheque" border="0" name="PostDatedCheque" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Sales/DealerIncentiveList.aspx" onmouseover="document.DealerIncentive.src='../img/dealerIncentiveblack.png'"
                                onmouseout="document.DealerIncentive.src='../img/dealerIncentivegreen.png'">
                                <img src="../img/dealerIncentivegreen.png" alt="Dealer Incentive" border="0" name="DealerIncentive" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Sales/SalesManTargetList.aspx" onmouseover="document.SalesManTarget.src='../img/saleManTargetblack.png'"
                                onmouseout="document.SalesManTarget.src='../img/saleManTargetgreen.png'">
                                <img src="../img/saleManTargetgreen.png" alt="Sales Man Target" border="0" name="SalesManTarget" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle">
                            <a href="../Sales/LimitInfoList.aspx" onmouseover="document.limitInfo.src='../img/limitblack.png'"
                                onmouseout="document.limitInfo.src='../img/limitgreen.png'">
                                <img src="../img/limitgreen.png" name="limitInfo" alt="limit Information" border="0" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Sales/StockReceivedNoteList.aspx" onmouseover="document.StockReceivedNote.src='../img/StockReceivedNotesblack.png'"
                                onmouseout="document.StockReceivedNote.src='../img/StockReceivedNotesgreen.png'">
                                <img src="../img/StockReceivedNotesgreen.png" alt="Stock Received Note" border="0"
                                    name="StockReceivedNote" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Sales/SalesOrderList.aspx" onmouseover="document.SalesOrder.src='../img/SalesOrderblack.png'"
                                onmouseout="document.SalesOrder.src='../img/SalesOrdergreen.png'">
                                <img src="../img/SalesOrdergreen.png" alt="Sales Order" border="0" name="SalesOrder" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle">
                            <a href="../Sales/SalesInvoiceList.aspx" onmouseover="document.SalesInvoice.src='../img/saleInvoiceblack.png'"
                                onmouseout="document.SalesInvoice.src='../img/saleInvoicegreen.png'">
                                <img src="../img/saleInvoicegreen.png" name="SalesInvoice" alt="Sales Invoice" border="0" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Sales/SaleReturnList.aspx" onmouseover="document.SalesRetrun.src='../img/SalesReturnblack.png'"
                                onmouseout="document.SalesRetrun.src='../img/SalesReturngreen.png'">
                                <img src="../img/SalesReturngreen.png" name="SalesRetrun" alt="Sales Retrun" border="0" /></a>
                        </td>
                        <td valign="middle" align="center">
                            <a href="../Sales/OnlineTransactionList.aspx" onmouseover="document.OnlineTransaction.src='../img/OnlineTransactionblack.png'"
                                onmouseout="document.OnlineTransaction.src='../img/OnlineTransactiongreen.png'">
                                <img src="../img/OnlineTransactiongreen.png" name="OnlineTransaction" alt="Online Transaction"
                                    border="0" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="101" valign="middle">
                            <a href="../Sales/SalesReportMenu.aspx" onmouseout="document.SalesReports.src='../img/ImportReportgreen.png'"
                                onmouseover="document.SalesReports.src='../img/ImportReportblack.png'">
                                <img alt="Sales Reports" border="0" name="SalesReports" src="../img/ImportReportgreen.png" /></a>
                        </td>
                        <td align="center" valign="middle">
                            &nbsp;
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
