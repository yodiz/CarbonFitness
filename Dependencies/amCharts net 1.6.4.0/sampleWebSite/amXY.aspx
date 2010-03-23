<%@ Page Language="C#" AutoEventWireup="true" CodeFile="amXY.aspx.cs" Inherits="amXY" MasterPageFile="~/site.master" %>

<%@ Register Assembly="am.Charts" Namespace="am.Charts" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>
            amXY sample</h2>
        <p>
            This sample demonstrates a simple mathematical chart with parabola
            and hyperbola
            data generated in the code</p>
        <p>
            <cc1:XyChart ID="XyChart1" runat="server" MarginRight="5">
                <Graphs>
                    <cc1:XyChartGraph runat="server" Title="Parabola" LineWidth="5">
                    </cc1:XyChartGraph>
                    <cc1:XyChartGraph runat="server" Title="Hyperbola" LineWidth="5">
                    </cc1:XyChartGraph>
                </Graphs>
                <Labels>
                    <cc1:ChartLabel Align="Right" Text="Parabola &amp; Hyperbola" TextColor="#FF8000"
                        TextSize="20" />
                </Labels>
            </cc1:XyChart>
            &nbsp;&nbsp;
        </p>
    
</asp:Content>