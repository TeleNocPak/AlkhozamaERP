<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventCalander.aspx.cs" Inherits="ERPWeb.EventCalander"
    MasterPageFile="~/Modules/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
<div id="calendar" style="position: absolute; width: 100%; height: 100%;"></div>
			
<script src="js/MindFusion.Scheduling.js" type="text/javascript"></script>
<script src="js/MyFirstSchedule.js" type="text/javascript"></script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
