<%@ Page Language="C#" MasterPageFile="~/site.master" %>

<%@ Register Assembly="am.Charts" Namespace="am.Charts" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>
            amPie with data from external file</h2>
        <p>
            This example demonstrates loading data from external data file and refreshing (reloading)
            it every 20 seconds</p>
        <p>
            <cc1:piechart id="PieChart1" runat="server" externaldatafileurl="~/amPieExternalDataSource.aspx"
                reloaddatainterval="20" startanimationeffect="Regular" startanimationtime="0"></cc1:piechart>
            &nbsp;</p>
    
</asp:Content>