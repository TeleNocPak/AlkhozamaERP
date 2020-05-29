<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrganizationChart.aspx.cs" Inherits="ERPWeb.OrganizationChart"
    MasterPageFile="~/Modules/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentArea" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:UpdatePanel ID="topUpdate" runat="server">
        <ContentTemplate>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>

<script type="text/javascript">
google.load("visualization", "1", { packages: ["orgchart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    $.ajax({
        type: "POST",
        url: "OrganizationChart.aspx/GetChartData",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Entity');
            data.addColumn('string', 'ParentEntity');
            data.addColumn('string', 'ToolTip');
            for (var i = 0; i < r.d.length; i++) {
                
                var employeeId = r.d[i][0].toString();
                var employeeName = r.d[i][1];
                var designation = r.d[i][2];
                var reportingManager = r.d[i][3] != null ? r.d[i][3].toString() : '';


                data.addRows([[{
                    v: employeeId,
                    f: employeeName + '<div style="width: 80px; height: 30px"><span style="width: 50px;height: 30px; width: 100px;display-margin: auto;text-align: center;">(' + designation + ')</span></div><img src = "EmpPics/' + employeeId + '.jpg" height="42" width="42"/>'
                    //f: employeeName + '<div>(<span>' + designation + '</span>)</div><img src = "Images/1.jpg" />'
                }, reportingManager, designation]]);

            }
            var chart = new google.visualization.OrgChart($("#chart")[0]);
            chart.draw(data, { allowHtml: true });
            //alert('OK...');
        },
        failure: function (r) {
            alert(r.d);
        },
        error: function (r) {
            alert(r.d);
        }
    });
}
</script>
<div id="chart" style ="overflow-x:scroll">
</div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
