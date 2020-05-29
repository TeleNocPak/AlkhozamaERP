<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountMenu.aspx.cs" Inherits="ERPWeb.AccountMenu"
    MasterPageFile="~/Modules/MasterPage.Master" %>

<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
            <div class="Menu_main_heading">
                <h1>
                    Financial</h1>
            </div>
            <!-- start home flow chart tabs -->
            <div class="home_flow_chart_tabs">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="34%" height="101" align="center" valign="middle">
                            <a href="../Accounts/ChartofAccount.aspx" onmouseout="document.Chart_Account.src='../img/COAgreen.png'"
                                onmouseover="document.Chart_Account.src='../img/COAblack.png'"><img alt="Chart of Account"
                                    border="0" name="Chart_Account" src="../img/COAgreen.png" /></a>
                        </td>
                        <td width="35%" valign="middle" align="center">
                            <a href="../Accounts/VoucherList.aspx" onmouseout="document.Voucher_List.src='../img/VoucherBankCashgreen.png'"
                                onmouseover="document.Voucher_List.src='../img/VoucherBankCashblack.png'"><img alt="Voucher List"
                                    border="0" name="Voucher_List" src="../img/VoucherBankCashgreen.png" /></a>
                        </td>
                        <td width="31%" valign="middle" align="center">
                              <a href="../Accounts/PostDatedChequeReceived.aspx" onmouseover="document.PostDatedChequeReceived.src='../img/postdatedchequerecblack.png'"
                                onmouseout="document.PostDatedChequeReceived.src='../img/postdatedchequerecgreen.png'">
                                <img src="../img/postdatedchequerecgreen.png" alt="Post Dated Cheque Received" border="0" name="PostDatedChequeReceived" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle">
                            <a href="../Accounts/GeneralVoucherList.aspx" onmouseout="document.JournalVoucher.src='../img/JournalVouchergreen.png'"
                                onmouseover="document.JournalVoucher.src='../img/JournalVoucherblack.png'"><img alt="Journal Voucher"
                                    border="0" name="JournalVoucher" src="../img/JournalVouchergreen.png" /></a>
                        </td>
                        <td valign="middle" align="center">
                            &nbsp;
                        </td>
                        <td valign="middle" align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="101" align="center" valign="middle">
                            &nbsp;
                        </td>
                        <td valign="middle" align="center">
                            &nbsp;
                        </td>
                        <td valign="middle" align="center">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <!-- end home flow chart tabs -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
