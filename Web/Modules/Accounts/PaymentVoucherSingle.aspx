<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentVoucherSingle.aspx.cs"
    Inherits="ERPWeb.PaymentVoucherSingle" MasterPageFile="~/Modules/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">

        function GetAccountID(source, eventArgs) {
            document.getElementById("<%=hfAccountAutoCompleted.ClientID%>").value = eventArgs.get_value();
        }

        function GetPartyAccountID(source, eventArgs) {
            document.getElementById("<%=hfPartyAccountAutoCompleted.ClientID%>").value = eventArgs.get_value();
        }
        
    </script>
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
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
            <table width="100%" border="0" cellpadding="5" cellspacing="5">
                <tr>
                    <td>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                <asp:ValidationSummary ID="vldSummary" runat="server" CssClass="notification error"
                                                    DisplayMode="List" ForeColor="" ValidationGroup="Group1" Width="92%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="Page_Heading" align="left">
                                                            <h1>
                                                                Payment Voucher (Cash & Bank)</h1>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="height: 9px">
                                                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                    <tr>
                                                        <td valign="top">
                                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td width="200px">
                                                                        Voucher Type *
                                                                    </td>
                                                                    <td width="345px">
                                                                        <asp:DropDownList ID="ddlVoucherType" runat="server" Width="250px" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Selected="True">Select Voucher Type</asp:ListItem>
                                                                            <asp:ListItem Value="CP">CASH PAYMENT</asp:ListItem>
                                                                            <asp:ListItem Value="BP">BANK PAYMENT</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlVoucherType"
                                                                            Display="Dynamic" InitialValue="0" ValidationGroup="Group1" SetFocusOnError="true"
                                                                            ErrorMessage="Voucher Type is mandatory.">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td width="120px">
                                                                        <asp:Label ID="lblHeadingVoucherNo" runat="server" Text="Voucher No." Visible="false"
                                                                            Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblVoucherNo" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Red"
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Voucher Date *
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtVoucherDate" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                        <asp:ImageButton ID="imageButton1" runat="server" ImageUrl="../img/Calendar.png" />
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imageButton1"
                                                                            TargetControlID="txtVoucherDate">
                                                                        </cc1:CalendarExtender>
                                                                        <cc1:MaskedEditExtender ID="Maskededitextender2" runat="server" AcceptNegative="Left"
                                                                            DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtVoucherDate">
                                                                        </cc1:MaskedEditExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVoucherDate"
                                                                            ValidationGroup="Group1" Display="Dynamic" ErrorMessage="Voucher Date is mandatory.">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Narration
                                                                    </td>
                                                                    <td align="left" colspan="3">
                                                                        <asp:TextBox ID="txtNarration" runat="server" MaxLength="300" Width="765px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table width="100%" border="0" cellspacing="4" cellpadding="0">
                                                                            <tr>
                                                                                <td width="180px">
                                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" Text="Cash / Bank Account Title"></asp:Label>
                                                                                </td>
                                                                                <td width="370px">
                                                                                    <asp:TextBox ID="txtAccountTitle" runat="server" MaxLength="50" OnTextChanged="txtAccountTitle_TextChanged"
                                                                                        Width="320px" AutoPostBack="True"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAccountTitle"
                                                                                        Display="Dynamic" ErrorMessage="Cash/Bank Account Title is mandatory." ValidationGroup="Group1"
                                                                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                                    <cc1:AutoCompleteExtender ID="txtAccountTitleAutoComplete" runat="server" CompletionInterval="1"
                                                                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                                        CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" DelimiterCharacters=""
                                                                                        EnableCaching="true" Enabled="True" FirstRowSelected="true" MinimumPrefixLength="3"
                                                                                        OnClientItemSelected="GetAccountID" ServiceMethod="GetCOAList" ServicePath=""
                                                                                        TargetControlID="txtAccountTitle" UseContextKey="True">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                </td>
                                                                                <td>
                                                                                    Account Code
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAccountCode" runat="server" BackColor="#EFEFEF" MaxLength="100"
                                                                                        ReadOnly="True" Width="200px"></asp:TextBox>
                                                                                    &nbsp;<asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="10pt" Text="Cr."></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table width="100%" border="0" cellspacing="4" cellpadding="0">
                                                                            <tr>
                                                                                <td width="180px">
                                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="10pt" Text="Party Account Title"></asp:Label>
                                                                                </td>
                                                                                <td width="370px">
                                                                                    <asp:TextBox ID="txtPartyAccountTitle" runat="server" MaxLength="50" Width="320px"
                                                                                        AutoPostBack="True" OnTextChanged="txtPartyAccountTitle_TextChanged"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPartyAccountTitle"
                                                                                        Display="Dynamic" ErrorMessage="Party Account Title is mandatory." ValidationGroup="Group1"
                                                                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="1"
                                                                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                                        CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" DelimiterCharacters=""
                                                                                        EnableCaching="true" Enabled="True" FirstRowSelected="true" MinimumPrefixLength="3"
                                                                                        OnClientItemSelected="GetPartyAccountID" ServiceMethod="GetCOAList" ServicePath=""
                                                                                        TargetControlID="txtPartyAccountTitle" UseContextKey="True">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                </td>
                                                                                <td>
                                                                                    Account Code
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPartyAccountCode" runat="server" BackColor="#EFEFEF" MaxLength="100"
                                                                                        ReadOnly="True" Width="200px"></asp:TextBox>
                                                                                        &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                                                        Text="Dr."></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table width="100%" border="0" cellspacing="4" cellpadding="0">
                                                                            <tr>
                                                                                <td width="180px">
                                                                                    <asp:Label ID="lblChequeNo" runat="server" Font-Bold="True" Font-Size="10pt" Text="Cheque No."
                                                                                        Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td width="300px">
                                                                                    <asp:TextBox ID="txtChequeNo" runat="server" MaxLength="20" Width="150px" Visible="false"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtChequeNo"
                                                                                        Display="Dynamic" ErrorMessage="Cheque No. is mandatory." ValidationGroup="Group1"
                                                                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td width="175px">
                                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="10pt" Text="Cash / Cheque Amount"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAmount" runat="server" BackColor="#CCFFCC" Font-Bold="True" Font-Size="18pt"
                                                                                        MaxLength="14" Style="text-align: right" Width="200px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtAmount"
                                                                                        Display="Dynamic" ErrorMessage="Cash / Cheque Amount is mandatory." ValidationGroup="Group1"
                                                                                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmount"
                                                                                        Display="Dynamic" ErrorMessage="Cash / Cheque Amount is not a valid Numeric format."
                                                                                        ValidationExpression="^\d{0,13}(\.\d{1,2})?$" ValidationGroup="Group1" Width="12px"
                                                                                        SetFocusOnError="true">*</asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="trChequeDateHide" visible="false">
                                                                                <td>
                                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="10pt" Text="Cheque Date"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtChequeDate" runat="server" MaxLength="50" Width="110px"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imageButton2" runat="server" ImageUrl="../img/Calendar.png" />
                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imageButton2"
                                                                                        TargetControlID="txtChequeDate">
                                                                                    </cc1:CalendarExtender>
                                                                                    <cc1:MaskedEditExtender ID="Maskededitextender3" runat="server" AcceptNegative="Left"
                                                                                        DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtChequeDate">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtChequeDate"
                                                                                        ValidationGroup="Group1" Display="Dynamic" ErrorMessage="Cheque Date is mandatory.">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
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
                                                    <tr>
                                                        <td valign="top">
                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="20%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="16%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Button ID="btnSaveNew" runat="server" CssClass="btninput" OnClick="btnSaveNew_Click"
                                                                            Text="Save and New" ValidationGroup="Group1" Width="120px" />&nbsp;
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="btninput" OnClick="btnSave_Click"
                                                                            Text="Save" ValidationGroup="Group1" Width="87px" />&nbsp;
                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btninput" OnClick="btnCancel_Click"
                                                                            Text="Cancel" Width="87px" />&nbsp;
                                                                        <asp:Button ID="btnPrint" runat="server" CssClass="btninput" Text="Print" Width="87px"
                                                                            Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
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
            <asp:HiddenField ID="HiddenFieldMode" runat="server" />
            <asp:HiddenField ID="HiddenFieldID" runat="server" />
            <asp:HiddenField ID="hfAccountAutoCompleted" runat="server" Value="0" />
            <asp:HiddenField ID="hfPartyAccountAutoCompleted" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
