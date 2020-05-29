<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRoles.aspx.cs" Inherits="ERPWeb.UserRoles"
    MasterPageFile="~/Modules/MasterPage.Master" %>

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
                                                    Application Roles</h1>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="100px" align="left">
                                                Role Name *</td>
                                            <td align="left" width="300px">
                                                <asp:TextBox ID="txtRoleName" runat="server" Width="266px" CssClass="inputtext"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkActive" runat="server" CssClass="checkboxclass" 
                                                    Text="Active" Checked="True" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="50%" class="Page_Heading" align="left">
                                                <h2>
                                                    List of Forms</h2>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:GridView ID="FormGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="LinkId" EmptyDataText="No Data Available" HorizontalAlign="Left"
                                        Width="980px" CssClass="data" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Form Name" SortExpression="Name">
                                                <HeaderStyle Width="30%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Allow Add">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%--<asp:CheckBox ID="chkFAllowAdd" runat="server" Checked='<%# Eval("AllowAdd") %>'/>--%>
                                                    <asp:CheckBox ID="chkFAllowAdd" runat="server" Checked='<%# Convert.ToBoolean(Eval("AllowAdd")) %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Edit">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFAllowEdit" runat="server" Checked='<%# Convert.ToBoolean(Eval("AllowEdit")) %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Delete">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFAllowDelete" runat="server" Checked='<%# Convert.ToBoolean(Eval("AllowDelete")) %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow View">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFAllowView" runat="server" Checked='<%# Convert.ToBoolean(Eval("AllowView")) %>'/>
                                                    <asp:HiddenField ID="hfFormId" runat="server" Value='<%# Bind("LinkId") %>' />
                                                    <asp:HiddenField ID="hfRoleDetailId" runat="server" Value='<%# Bind("RoleDetailID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="GrdHeader" HorizontalAlign="Left" />
                                        <AlternatingRowStyle BorderStyle="None" CssClass="GrdAlternateColor" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="50%" class="Page_Heading" align="left">
                                                <h2>
                                                    List of Reports</h2>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:GridView ID="ReportGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="LinkId" EmptyDataText="No Data Available" HorizontalAlign="Left"
                                        Width="980px" CssClass="data" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="Name" HeaderText="Report Name" SortExpression="Name">
                                                <HeaderStyle Width="30%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Allow Add">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRAllowAdd" runat="server" Checked='<%# Convert.ToBoolean(Eval("AllowAdd")) %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Edit">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRAllowEdit" runat="server" Checked='<%# Convert.ToBoolean(Eval("AllowEdit")) %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow Delete">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRAllowDelete" runat="server" Checked='<%# Convert.ToBoolean(Eval("AllowDelete")) %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow View">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRAllowView" runat="server" Checked='<%# Convert.ToBoolean(Eval("AllowView")) %>'/>
                                                    <asp:HiddenField ID="hfFormId" runat="server" Value='<%# Bind("LinkId") %>' />
                                                    <asp:HiddenField ID="hfRoleDetailId" runat="server" Value='<%# Bind("RoleDetailID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="GrdHeader" HorizontalAlign="Left" />
                                        <AlternatingRowStyle BorderStyle="None" CssClass="GrdAlternateColor" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="21%">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btnSave" runat="server" CssClass="btninput" OnClick="btnSave_Click"
                                                    Text="Save" Width="87px" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btninput" Text="Cancel" Width="87px"
                                                    OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="HiddenFieldMode" runat="server" />
                                    <asp:HiddenField ID="HiddenFieldID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
