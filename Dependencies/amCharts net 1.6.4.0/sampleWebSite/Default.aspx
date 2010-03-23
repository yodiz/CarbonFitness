<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/site.master" %>

<%@ Register Assembly="am.Charts" Namespace="am.Charts" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>Simple PieChart</h2>
        <p>
            This page shows a amPie chart in it's simplest form with default settings and static
            hard-coded data:</p>
        <br />
        <cc1:piechart id="PieChart1" runat="server">
            <Items>
                <cc1:PieChartDataItem Title="Women" Value="1523" />
                <cc1:PieChartDataItem PullOut="True" Title="Men" Value="1012" />
            </Items>
            <Labels>
                <cc1:ChartLabel Align="Center" Text="&lt;b&gt;Some sample data&lt;/b&gt;" />
            </Labels>
        </cc1:piechart>
</asp:Content>