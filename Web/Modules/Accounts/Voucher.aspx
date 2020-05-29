<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voucher.aspx.cs" Inherits="ERPWeb.Voucher"
    MasterPageFile="~/Modules/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">

        function GetAccountID(source, eventArgs) {
            document.getElementById("<%=hfAccountAutoCompleted.ClientID%>").value = eventArgs.get_value();
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
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="notification error"
                                                    DisplayMode="List" ForeColor="" ValidationGroup="Group2" Width="92%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="Page_Heading" align="left">
                                                            <h1>
                                                                Payment & Receipt Voucher</h1>
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
                                                                    <td width="130px">
                                                                        Voucher Type *
                                                                    </td>
                                                                    <td width="350px">
                                                                        <asp:DropDownList ID="ddlVoucherType" runat="server" Width="230px">
                                                                            <asp:ListItem Value="0" Selected="True">Select Voucher Type</asp:ListItem>
                                                                            <asp:ListItem Value="CR">CASH RECEIPT</asp:ListItem>
                                                                            <asp:ListItem Value="CP">CASH PAYMENT</asp:ListItem>
                                                                            <asp:ListItem Value="BR">BANK RECEIPT</asp:ListItem>
                                                                            <asp:ListItem Value="BP">BANK PAYMENT</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlVoucherType"
                                                                            Display="Dynamic" InitialValue="0" ValidationGroup="Group1" SetFocusOnError="true"
                                                                            ErrorMessage="Voucher Type is mandatory.">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td width="120px">
                                                                        <asp:Label ID="lblHeadingVoucherNo" runat="server" Text="Voucher No." Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                       <asp:Label ID="lblVoucherNo" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Red"
                                                                            Visible="False"></asp:Label></td>
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
                                                                        Status
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtStatus" runat="server" MaxLength="50" Width="120px" BackColor="#E9E9E9"
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td align="left">
                                                                        Narration
                                                                    </td>
                                                                    <td align="left" colspan="3">
                                                                        <asp:TextBox ID="txtNarration" runat="server"  MaxLength="300" 
                                                                            Width="805px"></asp:TextBox>
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
                                                                                <td width="360px">
                                                                                    Account Title
                                                                                </td>
                                                                                <td width="200">
                                                                                    Account Code</td>
                                                                                <td width="150">
                                                                                    Debit
                                                                                </td>
                                                                                <td width="150">
                                                                                    Credit
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="380px">
                                                                                    <asp:TextBox ID="txtAccountTitle" runat="server" MaxLength="50" Width="320px" 
                                                                                        ontextchanged="txtAccountTitle_TextChanged"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAccountTitle"
                                                                                        Display="Dynamic" ErrorMessage="Account Title is mandatory." ValidationGroup="Group2">*</asp:RequiredFieldValidator>
                                                                                    <cc1:AutoCompleteExtender ID="txtAccountTitleAutoComplete" runat="server" CompletionInterval="1"
                                                                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                                        CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" DelimiterCharacters=""
                                                                                        EnableCaching="true" Enabled="True" FirstRowSelected="true" MinimumPrefixLength="3"
                                                                                        OnClientItemSelected="GetAccountID" ServiceMethod="GetCOAList" ServicePath=""
                                                                                        TargetControlID="txtAccountTitle" UseContextKey="True">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAccountCode" runat="server" MaxLength="100"
                                                                                        Width="150px" BackColor="#EFEFEF" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDebitAmount" runat="server" MaxLength="13" 
                                                                                        Style="text-align: right" Width="115px"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                                                        ControlToValidate="txtDebitAmount" Display="Dynamic" 
                                                                                        ErrorMessage="Debit Amount is not a valid Numeric format." 
                                                                                        ValidationExpression="^\d{0,13}(\.\d{1,2})?$" ValidationGroup="Group2">*</asp:RegularExpressionValidator>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCreditAmount" runat="server" MaxLength="13" Style="text-align: right"
                                                                                        Width="115px"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCreditAmount"
                                                                                        Display="Dynamic" ErrorMessage="Credit Amount is not a valid Numeric format."
                                                                                        ValidationExpression="^\d{0,13}(\.\d{1,2})?$" ValidationGroup="Group2">*</asp:RegularExpressionValidator>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btninput" OnClick="btnAdd_Click"
                                                                                        Text=" Add " ValidationGroup="Group2" Width="100px" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" CssClass="data"
                                                                            DataKeyNames="RowNo" EmptyDataText="No Data Available List" Font-Names="verdana"
                                                                            Font-Size="8.5pt" GridLines="None" OnRowCommand="GridView_RowCommand" PageSize="15"
                                                                            Width="960px">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="AccountCode" HeaderText="Account Code">
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="AccountName" HeaderText="Account Title">
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TotalDebit" HeaderText="Debit">
                                                                                    <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TotalCredit" HeaderText="Credit" SortExpression="Total">
                                                                                    <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("RowNo") %>'
                                                                                            CommandName="Del" OnClientClick="return confirm('Are you sure you want to delete this Record?');">Delete</asp:LinkButton>
                                                                                        <asp:HiddenField ID="hfProductID" runat="server" />
                                                                                        <asp:HiddenField ID="hfColorId" runat="server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="GrdHeader" HorizontalAlign="Left" />
                                                                            <EmptyDataRowStyle />
                                                                            <AlternatingRowStyle BorderStyle="None" CssClass="GrdAlternateColor" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table width="100%" border="0" cellspacing="4" cellpadding="0">
                                                                            <tr>
                                                                                <td width="40%">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td width="20%">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="10pt" Text="Total Debit"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="10pt" Text="Total Credit"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDebitTotal" runat="server" BackColor="#CCFFCC" Font-Bold="True"
                                                                                        Font-Size="10pt" MaxLength="50" ReadOnly="True" Style="text-align: right" Width="180px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCreditTotal" runat="server" BackColor="#D5EAFF" Font-Bold="True"
                                                                                        Font-Size="10pt" MaxLength="50" ReadOnly="True" Style="text-align: right" Width="180px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="#CC3300"
                                                                                        Text="Difference"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDifference" runat="server" BackColor="#FFEAFF" Font-Bold="True"
                                                                                        Font-Size="10pt" MaxLength="50" ReadOnly="True" Style="text-align: right" Width="180px"></asp:TextBox>
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
                                                                            Enabled="false" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
