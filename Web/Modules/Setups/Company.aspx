<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="ERPWeb.Company"
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
                                                                Company Information</h1>
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
                                                                        Company Name *
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCompanyName" runat="server" Width="204px" MaxLength="60" CssClass="inputtext"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyName"
                                                                            ValidationGroup="Group1" ErrorMessage="Company Name is mandatory." SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Contact Person *
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtContactPerson" runat="server" Width="204px" MaxLength="60" CssClass="inputtext"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContactPerson"
                                                                            ValidationGroup="Group1" ErrorMessage="Contact Person is mandatory." SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        P-O-Box
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPOBox" runat="server" Width="204px" MaxLength="50" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Postal Code
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPostalCode" runat="server" Width="204px" MaxLength="80" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Contact Phone
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtContactPhone" runat="server" Width="204px" MaxLength="30" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Contact Mobile
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtContactMobile" runat="server" Width="204px" MaxLength="30" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Contact Fax No
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtContactFaxNo" runat="server" Width="204px" MaxLength="30" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Email
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmail" runat="server" Width="204px" MaxLength="50" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Web Site
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtWebSite" runat="server" Width="204px" MaxLength="50" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Address
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAddress" runat="server" Width="440px" MaxLength="200" CssClass="inputtext"
                                                                            Height="90px" TextMode="MultiLine"></asp:TextBox>
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
