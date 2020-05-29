<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="ERPWeb.Location"
    MasterPageFile="~/Modules/MasterPage.Master" %>

<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="Page_Heading" align="left">
                                                            <h1>
                                                                Location</h1>
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
                                                                    <td width="20%">
                                                                       Company *
                                                                    </td>
                                                                    <td>
                                                                       <asp:DropDownList ID="ddlCompany" runat="server" Width="218px" 
                                                                            CssClass="inputtext" AutoPostBack="True" 
                                                                            onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCompany"
                                                                            Display="Dynamic" ErrorMessage="Company is mandatory." InitialValue="0" ValidationGroup="Group1"
                                                                            SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="20%">
                                                                       Branch *
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="220px" 
                                                                            CssClass="inputtext">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                            ControlToValidate="ddlBranch" Display="Dynamic" ErrorMessage="Branch is mandatory."
                                                                            InitialValue="0" ValidationGroup="Group1" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="20%">
                                                                            Location Name *
                                                                        </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLocationName" runat="server" CssClass="inputtext" MaxLength="50"
                                                                            Width="204px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLocationName"
                                                                            ErrorMessage="Location Name is mandatory." ValidationGroup="Group1">*</asp:RequiredFieldValidator>
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
                                                                        <asp:Button ID="btnSaveNew" runat="server" CssClass="btninput" OnClick="btnSaveNew_Click"
                                                                            Text="Save and New" ValidationGroup="Group1" Width="120px" />
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="btninput" OnClick="btnSave_Click"
                                                                            Text="Save" Width="87px" ValidationGroup="Group1" />
                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btninput" Text="Cancel" Width="87px"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
