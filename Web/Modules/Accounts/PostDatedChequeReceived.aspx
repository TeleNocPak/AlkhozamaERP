<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostDatedChequeReceived.aspx.cs"
    Inherits="ERPWeb.PostDatedChequeReceived" MasterPageFile="~/Modules/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <script type="text/javascript" language="javascript">

        function GetDealerName(source, eventArgs) {
            document.getElementById("<%=hfDealerNameAutoCompleted.ClientID%>").value = eventArgs.get_value();
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
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td height="15px">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="50%" class="Page_Heading" align="left">
                                                <h1>
                                                    List of Post Dated Cheque Received</h1>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <table width="980px" border="0" cellspacing="0" cellpadding="0" align="center">
                                        <tr>
                                            <td height="42" valign="middle">
                                                <asp:Panel ID="AgentInfoPanel" runat="server" Style="cursor: hand;" CssClass="search-bor-non">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="border-non">
                                                        <tr>
                                                            <td colspan="2" valign="top">
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td width="1%" valign="middle" class="search-header">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="94%" valign="middle" class="search-header">
                                                                            Search
                                                                        </td>
                                                                        <td width="4%" valign="middle" class="search-header">
                                                                            <img id="imgAgentInfo" src="../img/plus-2.jpg" border="0" alt="+" />
                                                                        </td>
                                                                        <td width="1%" valign="middle" class="search-header">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <cc1:CollapsiblePanelExtender ID="cpeIntroduction" runat="server" ExpandControlID="AgentInfoPanel"
                                                    CollapseControlID="AgentInfoPanel" Collapsed="true" ImageControlID="imgAgentInfo"
                                                    ExpandedImage="../img/minus-2.jpg" CollapsedImage="../img/plus-2.jpg" SuppressPostBack="True"
                                                    TargetControlID="AgentInfoPanelDesc">
                                                </cc1:CollapsiblePanelExtender>
                                                <asp:Panel ID="AgentInfoPanelDesc" runat="server">
                                                    <table width="100%" border="0" cellpadding="3" cellspacing="0" class="border-non-table">
                                                        <tr>
                                                            <td width="4%" height="20">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="4%">
                                                                &nbsp;
                                                            </td>
                                                            <td width="16%">
                                                                Dealer Name :
                                                            </td>
                                                            <td width="30%">
                                                                <asp:TextBox ID="txtDealerName" runat="server" CssClass="inputtext" MaxLength="50"
                                                                    AutoPostBack="false" Width="190px"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="txtDealerNameAutoComplete" runat="server" CompletionInterval="1"
                                                                    CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                                    CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" DelimiterCharacters=""
                                                                    EnableCaching="true" Enabled="True" FirstRowSelected="true" MinimumPrefixLength="3"
                                                                    OnClientItemSelected="GetDealerName" ServiceMethod="GetDealerList" ServicePath=""
                                                                    TargetControlID="txtDealerName" UseContextKey="True">
                                                                </cc1:AutoCompleteExtender>
                                                            </td>
                                                            <td>
                                                                Submit Date :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSubmitDate" runat="server" Width="110px" MaxLength="50"></asp:TextBox>
                                                                <asp:ImageButton ID="imageButton1" runat="server" ImageUrl="../img/Calendar.png" />
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd MMM yyyy"
                                                                    PopupButtonID="imageButton1" TargetControlID="txtSubmitDate">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Cheque Date :
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="txtChequeDate" runat="server" MaxLength="50" Width="110px"></asp:TextBox>
                                                                <asp:ImageButton ID="imageButton3" runat="server" ImageUrl="../img/Calendar.png" />
                                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd MMM yyyy"
                                                                    PopupButtonID="imageButton3" TargetControlID="txtChequeDate">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                Cheque No :
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="txtChequeNo" runat="server" MaxLength="50" Width="110px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                Bank Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBankName" runat="server" MaxLength="50" Width="110px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Branch Name :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBranchName" runat="server" MaxLength="50" Width="110px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                Paid Status :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPaidStatus" runat="server" CssClass="inputtext" Width="157px">
                                                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                Amount :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="12" Width="80px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtAmount">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <asp:DropDownList ID="ddlAmount" runat="server" Width="60px">
                                                                    <asp:ListItem>=</asp:ListItem>
                                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                                    <asp:ListItem>&lt;=</asp:ListItem>
                                                                    <asp:ListItem>&gt;=</asp:ListItem>
                                                                </asp:DropDownList>
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
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="btninput" OnClick="btnSearch_Click"
                                                                    Text="Search" Width="87px" />
                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btninput" Text="Cancel" Width="87px"
                                                                    OnClick="btnCancel_Click" />
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
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="PostDatedChequeId" EmptyDataText="No Data Available"
                                        Width="980px" OnPageIndexChanging="GridView_PageIndexChanging" OnRowCommand="GridView_RowCommand"
                                        OnRowDataBound="GridView_RowDataBound" OnSorting="GridView_Sorting" PageSize="15"
                                        Font-Names="verdana" Font-Size="8.5pt" CssClass="data" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Serial">
                                                <ItemStyle HorizontalAlign="left" Width="40px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerialNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" SortExpression="DealerName">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Submit Date">
                                                <ItemTemplate>
                                                    <%# GetSubmitDate(Eval("SubmitDate"))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque Date">
                                                <ItemTemplate>
                                                    <%# GetChequeDate(Eval("ChequeDate"))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No" SortExpression="ChequeNo">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BankName" HeaderText="Bank" SortExpression="BankName">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Acc.Received">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAccountStatus" runat="server" Checked='<%# Convert.ToBoolean(Eval("AccountReceived")) %>'
                                                        AutoPostBack="true" Enabled="true" OnCheckedChanged="chkAccountStatus_CheckedChanged" />
                                                    <asp:HiddenField ID="hfPDCDetailId" runat="server" Value='<%# Bind("PostDatedChequeDetailId") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Paid">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPaid" runat="server" CausesValidation="false" CommandName="Paid"
                                                        CommandArgument='<%# Bind("PostDatedChequeDetailId") %>' Text="Paid"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Width="4%" Wrap="False" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="GrdHeader" HorizontalAlign="Left" />
                                        <EmptyDataRowStyle />
                                        <AlternatingRowStyle BorderStyle="None" CssClass="GrdAlternateOrderRequisition" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <cc1:ModalPopupExtender ID="Paid_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                DynamicServicePath="" Enabled="True" EnableViewState="true" PopupControlID="PaidTopPanel"
                OkControlID="btnMFinishPaid" TargetControlID="btnAddPaidDummy" RepositionMode="RepositionOnWindowResizeAndScroll">
            </cc1:ModalPopupExtender>
            <table width="100%">
                <tr>
                    <td align="center" valign="middle">
                        <asp:Panel ID="PaidTopPanel" runat="server" Style="display: none;" Width="500px"
                            Height="300px">
                            <asp:Panel ID="PaidInnerPanel" runat="server" Height="300px" Style="background-color: #ffffff;
                                border: solid 1px Gray; color: Black" Width="500px">
                                <table cellspacing="0" cellpadding="0" border="0" style="width: 500px; border-collapse: collapse;
                                    border-collapse: separate;">
                                    <tr>
                                        <td style="width: 100%; cursor: default;">
                                            <table cellspacing="0" cellpadding="0" border="0" style="width: 100%; border-collapse: collapse;
                                                border-collapse: separate;">
                                                <tr>
                                                    <td valign="top">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td align="left" valign="top">
                                                                    <table width="100%" border="0" cellpadding="2" cellspacing="2">
                                                                        <tr>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left" colspan="2">
                                                                                <strong>Add Post Dated Cheque Received</strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="7%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td width="30%">
                                                                                Paid Date :
                                                                            </td>
                                                                            <td align="left">
                                                                                <%--  <asp:TextBox ID="txtPaid" runat="server" Width="110px" MaxLength="16"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtPaid"
                                                                                    Display="Dynamic" ErrorMessage="Bank is mandatory." SetFocusOnError="true" ValidationGroup="Group3">*</asp:RequiredFieldValidator>
                                                                                <asp:ImageButton ID="imageButton2" runat="server" ImageUrl="../img/Calendar.png" />
                                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy"
                                                                                    PopupButtonID="imageButton2" TargetControlID="txtPaid">
                                                                                </cc1:CalendarExtender>--%>
                                                                                <asp:TextBox ID="txtPaid" runat="server" MaxLength="50" Width="80px"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="txtSubmitDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                                                    PopupButtonID="imageButton4" TargetControlID="txtPaid">
                                                                                </cc1:CalendarExtender>
                                                                                <cc1:MaskedEditExtender ID="Maskededitextender1" runat="server" AcceptNegative="Left"
                                                                                    DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtPaid">
                                                                                </cc1:MaskedEditExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtPaid"
                                                                                    Display="Dynamic" ErrorMessage="Submit date is mandatory." SetFocusOnError="true"
                                                                                    ValidationGroup="Group3">*</asp:RequiredFieldValidator>
                                                                                <asp:ImageButton ID="imageButton4" runat="server" ImageUrl="../img/Calendar.png" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                Bank :
                                                                            </td>
                                                                            <td width="300px" align="left">
                                                                                <asp:DropDownList ID="ddlBank" runat="server" Width="210px">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlBank"
                                                                                    Display="Dynamic" InitialValue="0" ValidationGroup="Group3" ErrorMessage="Bank is mandatory."
                                                                                    SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                Deposit Slip No :
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtDSNo" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtDSNo"
                                                                                    Display="Dynamic" ErrorMessage="Bank is mandatory." SetFocusOnError="true" ValidationGroup="Group3">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    Narration :
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtNarration" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Button ID="btnAccontPaid" runat="server" CssClass="btninput" Text="Save" Width="87px"
                                                                                        OnClick="btnAccontPaid_Click" ValidationGroup="Group3" />
                                                                                    &nbsp;
                                                                                    <asp:Button ID="btnAccountCancel" runat="server" CssClass="btninput" Text="Cancel"
                                                                                        Width="87px" OnClick="btnAccountCancel_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <asp:Button ID="btnMFinishPaid" runat="server" Style="visibility: hidden" />
                                                                            <asp:Button ID="btnAddPaidDummy" runat="server" Style="visibility: hidden" />
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfSearchText" runat="server" />
            <asp:HiddenField ID="hfDealerNameAutoCompleted" runat="server" Value="0" />
            <asp:HiddenField ID="hfPDCDetailId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
