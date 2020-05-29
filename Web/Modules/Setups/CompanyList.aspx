<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyList.aspx.cs" Inherits="ERPWeb.CompanyList"
    MasterPageFile="~/Modules/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
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
                                                    List of Company</h1>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnAdd" runat="server" CssClass="btninput" Text="Add Company" Width="150px"
                                                    OnClick="btnAdd_Click" />
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
                                                            <td height="20">
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
                                                                Company Name :
                                                            </td>
                                                            <td width="26%">
                                                                <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Contact Person :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="100" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Contact Mobile :
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="txtMobile" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                Country :
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Width="150px"></asp:TextBox>
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
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="btninput" Text="Search" Width="87px"
                                                                    OnClick="btnSearch_Click" />
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
                                    <asp:GridView ID="CompanyGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="CompanyId" EmptyDataText="No Data Available"
                                        Width="980px" OnPageIndexChanging="CompanyGridView_PageIndexChanging" OnRowCommand="CompanyGridView_RowCommand"
                                        OnRowDataBound="CompanyGridView_RowDataBound" OnSorting="CompanyGridView_Sorting"
                                        PageSize="15" Font-Names="verdana" Font-Size="8.5pt" CssClass="data" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Serial">
                                                <ItemStyle HorizontalAlign="left" Width="6%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerialNo" runat="server" Width="34px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                           
                                            <asp:BoundField DataField="ContactMobile" HeaderText="Contact Mobile" SortExpression="ContactMobile">
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField HeaderText="Edit Command">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnView" runat="server" CausesValidation="false" CommandName="View"
                                                        CommandArgument='<%# Bind("CompanyId") %>' Text="View"></asp:LinkButton>
                                                    &nbsp;|&nbsp;
                                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                        CommandArgument='<%# Bind("CompanyId") %>' Text="Edit"></asp:LinkButton>
                                                    &nbsp;|&nbsp;
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("CompanyId") %>'
                                                        OnClientClick="return confirm('Are you sure you want to delete this Record?');">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Width="14%" Wrap="False" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="GrdHeader" HorizontalAlign="Left" />
                                        <EmptyDataRowStyle />
                                        <AlternatingRowStyle BorderStyle="None" CssClass="GrdAlternateColor" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
             <asp:HiddenField ID="hfSearchText" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
