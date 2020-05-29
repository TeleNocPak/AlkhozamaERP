<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Country.aspx.cs" Inherits="ERPWeb.Country"
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
                                                                Country Information</h1>
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
                                                                        Country Name *</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtName" runat="server" Width="204px" MaxLength="50" CssClass="inputtext"></asp:TextBox>
                                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                                                                            ValidationGroup="Group1" ErrorMessage="Country Name is mandatory." SetFocusOnError="true">*</asp:RequiredFieldValidator>
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
                                                                        <asp:Button ID="btnSaveNew" runat="server" CssClass="btninput" 
                                                                            OnClick="btnSaveNew_Click" Text="Save and New" ValidationGroup="Group1" 
                                                                            Width="120px" />
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="btninput" OnClick="btnSave_Click"
                                                                            Text="Save" Width="87px" ValidationGroup="Group1"/>
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
