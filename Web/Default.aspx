<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ERPWeb.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ERP Application</title>
    <link rel="stylesheet" type="text/css" href="Modules/css/main.css">
    <link rel="stylesheet" type="text/css" href="jqtransformplugin/jqtransform.css" />
    <link rel="stylesheet" type="text/css" href="formValidator/validationEngine.jquery.css" />
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="jqtransformplugin/jquery.jqtransform.js"></script>
    <script type="text/javascript" src="jqtransformplugin/script.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainwrap">
        <!-- start login wrap -->
        <div class="login_logo">
            <img src="Modules/img/BrowserPreview_02.png" alt="Alkhozama" /></div>
        <div class="login_box">
            <table width="100%" border="0" cellspacing="2" cellpadding="2">
                <tr>
                    <td valign="middle">
                        &nbsp;
                    </td>
                    <td valign="middle">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Names="verdana" Font-Size="8.5pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        Username:
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtUserName" runat="server" MaxLength="80" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName"
                            ErrorMessage="User Name cannot be blank" ValidationGroup="Group1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        Password:
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="15" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password cannot be blank" ValidationGroup="Group1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        &nbsp;
                    </td>
                    <td valign="middle">
                        Forgot Password? <a href="#">Click here</a>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        &nbsp;
                    </td>
                    <td valign="top">
                        <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="Modules/img/loginbutton.png"
                            OnClick="btnLogin_Click" ValidationGroup="Group1" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="login_copyrigth">
            All rights Reserved Copyright &copy; Alkhozama
        </div>
        <!-- end login wrap -->
    </div>
    <cc1:ModalPopupExtender ID="Location_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
        DynamicServicePath="" Enabled="True" EnableViewState="true" PopupControlID="LocationTopPanel"
        OkControlID="btnCancel" TargetControlID="btnAddLocationDummy" RepositionMode="RepositionOnWindowResizeAndScroll">
    </cc1:ModalPopupExtender>
    <table width="100%">
        <tr>
            <td align="center" valign="middle">
                <asp:Panel ID="LocationTopPanel" runat="server" Style="display: none;" Width="340px"
                    Height="270px">
                    <asp:Panel ID="LocationInnerPanel" runat="server" Style="background-color: #ffffff;
                        border: solid 1px Gray; color: Black" Width="340px" Height="270px">
                        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%; border-collapse: collapse;
                            border-collapse: separate;">
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td valign="top" width="12%">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <h2>
                                                    Locations</h2>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; width:280px; height: 135px;">
                                                    <asp:RadioButtonList ID="lstLocation" runat="server" CellPadding="2" CellSpacing="2" Font-Size="13px" Font-Bold="true" CssClass="radiobutton-input">
                                                    </asp:RadioButtonList>
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btnLocationLogin" runat="server" CssClass="btninput" OnClick="btnLocationLogin_Click"
                                                    Text="Login" Width="100px" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btninput" Text="Cancel" Width="100px" />
                                                <asp:Button ID="btnMFinishLocation" runat="server" Style="visibility: hidden" />
                                                <asp:Button ID="btnAddLocationDummy" runat="server" Style="visibility: hidden" />
                                            </td>
                                            <td valign="top">
                                                &nbsp;
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

    <asp:HiddenField ID="hfPassword" runat="server" />

    </form>
</body>
</html>
