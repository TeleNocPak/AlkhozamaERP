<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRolesList.aspx.cs" Inherits="ERPWeb.UserRolesList"
    MasterPageFile="~/Modules/MasterPage.Master" %>

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
                                                    List of User Roles</h1>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnAdd" runat="server" CssClass="btninput" Text="Add Role" Width="100px"
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
                                                            <td  height="20">
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
                                                                Role Name :
                                                            </td>
                                                            <td width="35%">
                                                                <asp:TextBox ID="txtRoleName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="btninput" 
                                                                    OnClick="btnSearch_Click" Text="Search" Width="87px" />
                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btninput" 
                                                                    OnClick="btnCancel_Click" Text="Cancel" Width="87px" />
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
                                        AutoGenerateColumns="False" DataKeyNames="ID" EmptyDataText="No Data Available"
                                        Width="980px" OnPageIndexChanging="GridView_PageIndexChanging" OnRowCommand="GridView_RowCommand"
                                        OnRowDataBound="GridView_RowDataBound" OnSorting="GridView_Sorting" PageSize="15"
                                        Font-Names="verdana" Font-Size="8.5pt" CssClass="data" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Serial">
                                                <ItemStyle HorizontalAlign="left" Width="60px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerialNo" runat="server" Width="34px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" SortExpression="UserId" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Role Name" SortExpression="Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Edit Command">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnView" runat="server" CausesValidation="false" CommandName="View"
                                                        CommandArgument='<%# Bind("ID") %>' Text="View"></asp:LinkButton>
                                                    &nbsp;|&nbsp;
                                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                        CommandArgument='<%# Bind("ID") %>' Text="Edit"></asp:LinkButton>
                                                    &nbsp;|&nbsp;
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("ID") %>'
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
