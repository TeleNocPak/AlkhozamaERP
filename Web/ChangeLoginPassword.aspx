<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeLoginPassword.aspx.cs"
    Inherits="ERPWeb.ChangeLoginPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <div class="mainwrap">
        <!-- start login wrap -->
        <div class="login_logo">
            <img src="Modules/img/BrowserPreview_02.png" alt="United Mobile" /></div>
        <div class="ChangeloginPassword_box">
            <table width="100%" border="0" cellspacing="2" cellpadding="2">
                <tr>
                    <td valign="middle" colspan="2">
                        <h1>
                            Change Password</h1>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        &nbsp;
                    </td>
                    <td valign="middle">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        New Password :
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="inputtext" MaxLength="15"
                            TextMode="Password" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword"
                            ErrorMessage="New Password cannot be blank" ValidationGroup="Group1">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtConfirmPassword"
                            ControlToValidate="txtNewPassword" ErrorMessage="New Password and Confirm Password is not same"
                            ValidationGroup="Group1">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        Confirm Password
                    </td>
                    <td valign="middle">
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="inputtext" MaxLength="15"
                            TextMode="Password" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Confirm Password cannot be blank" ValidationGroup="Group1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        &nbsp;
                    </td>
                    <td valign="top">
                                                                        <asp:Button ID="btnSave" 
                            runat="server" CssClass="btninput" OnClick="btnSave_Click"
                                                                            Text="Update" Width="87px" 
                            ValidationGroup="Group1" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="login_copyrigth">
        </div>
        <!-- end login wrap -->
    </div>
    <%--<center>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 19%">
            <tr>
                <td>
                    <div class="heading-admin">
                        Login Panel</div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br>
                    <table border="0" cellspacing="0" cellpadding="0" style="width: 23%">
                        <tr>
                            <td align="left" style="width: 239px; height: 16px" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 239px; height: 18px" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" style="width: 239px">
                                <table width="98%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="forum-text" style="width: 232px">
                                            Login Name
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 232px">
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="inputtext" MaxLength="150"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName"
                                                ErrorMessage="User Name cannot be blank" ValidationGroup="Group1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 232px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="forum-text" style="width: 232px">
                                            Password
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 232px">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="inputtext"
                                                MaxLength="15"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                                                ErrorMessage="Password cannot be blank" ValidationGroup="Group1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 232px">
                                            &nbsp;
                                            <asp:Button ID="btnLogin" runat="server" CssClass="btninput" OnClick="btnLogin_Click"
                                                Text="Login" Width="87px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 232px">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td valign="top" style="width: 51%">
                                                    </td>
                                                    <td width="50%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 232px">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 232px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>--%>
    </form>
</body>
</html>
