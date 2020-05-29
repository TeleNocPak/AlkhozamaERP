<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartofAccount.aspx.cs"
    Inherits="ERPWeb.ChartofAccount" MasterPageFile="~/Modules/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
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
                                            <td height="15px">
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
                                                                Chart of Account</h1>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <%--  <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Panel ID="Panel2" runat="server" GroupingText="Locations" Font-Bold="True">
                                                                <asp:RadioButtonList ID="rdCompanyList" runat="server" CellPadding="4" Font-Size="14px"
                                                                    Font-Bold="true" CellSpacing="4" RepeatColumns="4" 
                                                                    RepeatDirection="Horizontal" AutoPostBack="True" 
                                                                    onselectedindexchanged="rdCompanyList_SelectedIndexChanged">
                                                                </asp:RadioButtonList>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td valign="top" style="height: 9px">
                                                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                    <tr>
                                                        <td valign="top">
                                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td width="50%" valign="top" align="left">
                                                                        <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; width: 420px; height: 600px;">
                                                                            <asp:TreeView ID="tvCOA" runat="server" ShowLines="True" OnSelectedNodeChanged="tvCOA_SelectedNodeChanged">
                                                                                <SelectedNodeStyle BackColor="#E6E6E6" Font-Bold="true" Font-Names="Verdana" ForeColor="#CC3300" />
                                                                                <RootNodeStyle Font-Names="Verdana" Font-Bold="True" Font-Size="13pt" ForeColor="#CC3300" />
                                                                                <NodeStyle Font-Names="Verdana" Font-Size="12px" ForeColor="#4D4D4D" />
                                                                            </asp:TreeView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td width="30%">
                                                                                                Branch
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlBranch" runat="server" Width="270px" CssClass="inputtext"
                                                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBranch"
                                                                                                    Display="Dynamic" ErrorMessage="Branch is mandatory." InitialValue="0" ValidationGroup="Group1"
                                                                                                    SetFocusOnError="true">*</asp:RequiredFieldValidator>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="30%" valign="middle">
                                                                                                Child of
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtParentAccountName" runat="server" BackColor="#EFEFEF" CssClass="inputtext"
                                                                                                    Font-Bold="True" MaxLength="200" ReadOnly="True" Width="260px"></asp:TextBox>
                                                                                                <asp:ImageButton ID="btnEditCOA" runat="server" ImageUrl="~/Modules/img/EditCOA.png"
                                                                                                    OnClick="btnEditCOA_Click" ToolTip="Edit COA" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="30%">
                                                                                                Account Child Code
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtParentAccountCode" runat="server" MaxLength="3" Width="260px"
                                                                                                    BackColor="#EFEFEF" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                                                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtParentAccountCode"
                                                                                                    ValidationGroup="Group1" ErrorMessage="Account Child Code is mandatory.">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                Account Head
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtAccountName" runat="server" Width="260px" MaxLength="200" CssClass="inputtext"></asp:TextBox>
                                                                                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAccountName"
                                                                                                    ValidationGroup="Group1" SetFocusOnError="true" ErrorMessage="Account Name is mandatory.">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="TRLoc1" runat="server" visible="false">
                                                                                            <td style="height: 43px" colspan="2">
                                                                                                <h2>
                                                                                                    Locations</h2>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr id="TRLoc2" runat="server" visible="false">
                                                                                            <td style="height: 43px" colspan="2">
                                                                                                <asp:GridView ID="LocationGridView" runat="server" AutoGenerateColumns="False" CssClass="data"
                                                                                                    DataKeyNames="LocationId" EmptyDataText="No Data Available" Font-Names="verdana"
                                                                                                    Font-Size="8.5pt" GridLines="None" PageSize="15" Width="470px" OnRowDataBound="LocationGridView_RowDataBound">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="Locations">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                                                                                                <asp:HiddenField ID="hfLocationId" runat="server" Value='<%# Bind("LocationId") %>' />
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Appeared">
                                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                            <ItemTemplate>
                                                                                                                <asp:CheckBox ID="chkAppeared" runat="server" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Opening Bal.">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtOpeningBalance" runat="server" Width="140px" Style="text-align: right"
                                                                                                                    MaxLength="13"></asp:TextBox>
                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                                                                    TargetControlID="txtOpeningBalance">
                                                                                                                </cc1:FilteredTextBoxExtender>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                                                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <HeaderStyle CssClass="GrdHeader" HorizontalAlign="Left" />
                                                                                                    <EmptyDataRowStyle />
                                                                                                    <%--<AlternatingRowStyle BorderStyle="None" CssClass="GrdAlternateColor" />--%>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td width="31%">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Button ID="btnSaveNew" runat="server" CssClass="btninput" OnClick="btnSaveNew_Click"
                                                                                                    Text="Save and New" ValidationGroup="Group1" Width="120px" />
                                                                                                <%--<asp:Button ID="btnSave" runat="server" CssClass="btninput" OnClick="btnSave_Click"
                                                                                            Text="Save" Width="87px" ValidationGroup="Group1" />--%>
                                                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btninput" Text="Cancel" Width="120px"
                                                                                                    OnClick="btnCancel_Click" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="20%">
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
                                                                    <td width="21%">
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
            <asp:HiddenField ID="HiddenFieldAccountId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
