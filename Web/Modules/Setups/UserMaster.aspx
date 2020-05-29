<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="ERPWeb.UserMaster"
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
                                                                User Information</h1>
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
                                                                    <td>
                                                                        Full Name *
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtName" runat="server" Width="245px" MaxLength="100" CssClass="inputtext"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtName"
                                                                            Display="Dynamic" ErrorMessage="Full Name is mandatory." SetFocusOnError="true"
                                                                            ValidationGroup="Group1">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="20%">
                                                                        Login ID *
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLoginID" runat="server" Width="245px" MaxLength="50" CssClass="inputtext"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLoginID"
                                                                            Display="Dynamic" ErrorMessage="Login Id is mandatory." SetFocusOnError="true"
                                                                            ValidationGroup="Group1">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Branch *
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="260px" 
                                                                            CssClass="inputtext" AutoPostBack="True" 
                                                                            onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch"
                                                                            Display="Dynamic" ErrorMessage="Branch is mandatory." InitialValue="0" ValidationGroup="Group1"
                                                                            SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Location *
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Panel ID="Panel2" runat="server" BorderStyle="None" BorderWidth="2px"
                                                                            Direction="LeftToRight" Height="70px" ScrollBars="Horizontal" Width="650px">
                                                                            <asp:CheckBoxList ID="lstLocation" runat="server" 
                                                                                RepeatDirection="Horizontal" CellPadding="2" CellSpacing="2" CssClass="inputtext">
                                                                            </asp:CheckBoxList>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Role *
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="inputtext" Width="260px">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlRole"
                                                                            Display="Dynamic" ErrorMessage="Role is mandatory." InitialValue="0" ValidationGroup="Group1"
                                                                            SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Contact Phone
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhone" runat="server" Width="245px" MaxLength="50" CssClass="inputtext"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Maskededitextender1" runat="server" AcceptNegative="Left"
                                                                            DisplayMoney="Left" Mask="999-99999999" MaskType="None" TargetControlID="txtPhone"
                                                                            ClearMaskOnLostFocus="False">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Contact Mobile
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMobile" runat="server" Width="245px" MaxLength="50" CssClass="inputtext"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="Maskededitextender2" runat="server" AcceptNegative="Left"
                                                                            DisplayMoney="Left" Mask="9999-9999999" MaskType="None" TargetControlID="txtMobile"
                                                                            ClearMaskOnLostFocus="False">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Email
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmail" runat="server" Width="245px" MaxLength="50" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Address
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAddress" runat="server" Height="90px" TextMode="MultiLine" Width="440px"
                                                                            CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="font-size: 13px">
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="font-size: 13px">
                                                                        <strong>User Port Folio</strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Qualification
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQualification" runat="server" Width="245px" MaxLength="100" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        References
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtReference" runat="server" Width="245px" MaxLength="100" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Emergency Contact
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmergency" runat="server" Width="245px" MaxLength="100" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Appointment Date
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAppointmentDate" runat="server" Width="100px" CssClass="inputtext"></asp:TextBox>
                                                                        <asp:ImageButton ID="imageButton1" runat="server" ImageUrl="../img/Calendar.png" />
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imageButton1"
                                                                            TargetControlID="txtAppointmentDate">
                                                                        </cc1:CalendarExtender>
                                                                        <cc1:MaskedEditExtender ID="Maskededitextender3" runat="server" AcceptNegative="Left"
                                                                            DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAppointmentDate">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Others
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOther" runat="server" Width="245px" MaxLength="100" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Supper Admin
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkAdmin" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        User Active
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkActive" runat="server" Checked="True" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
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
                                                                        <asp:Button ID="btnSaveNew" runat="server" CssClass="btninput" OnClick="btnSaveNew_Click"
                                                                            Text="Save and New" ValidationGroup="Group1" Width="120px" />
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="btninput" OnClick="btnSave_Click"
                                                                            Text="Save" Width="87px" ValidationGroup="Group1" />
                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btninput" Text="Cancel" Width="87px"
                                                                            OnClick="btnCancel_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="21%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="21%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        &nbsp;
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
