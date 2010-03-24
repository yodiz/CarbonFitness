<%@ Page Language="C#" AutoEventWireup="true" CodeFile="amRadar.aspx.cs" Inherits="amRadar" MasterPageFile="~/site.master" %>

<%@ Register Assembly="am.Charts" Namespace="am.Charts" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>
            amRadar sample</h2>
        <p>
            This sample demonstrates simple databound Radar Chart. It uses data from 2 seperate data source
            object (created from one XML file). It also includes a custom fill from 10000 to 12000 (in blue).</p>
        <p>
        <cc1:RadarChart ID="RadarChart1" runat="server" DataAxisIDField="year" DataSourceID="XmlDataSource1"
            GridColor="Maroon" GridFillAlpha="30" GridFillColor="Lime" GridType="Circles">
            <Graphs>
                <cc1:RadarChartGraph runat="server" BulletSize="6" DataAxisItemIDField="year" DataSourceID="XmlDataSource2"
                    DataValueField="e95" Title="E95 Prices">
                </cc1:RadarChartGraph>
                <cc1:RadarChartGraph runat="server" BulletSize="6" DataAxisItemIDField="year" DataSourceID="XmlDataSource2"
                    DataValueField="e98" Title="E98 prices">
                </cc1:RadarChartGraph>
            </Graphs>
            <Fills>
                <cc1:RadarChartFill Alpha="30" Color="Blue" EndValue="10000" StartValue="12000" />
            </Fills>
        </cc1:RadarChart>
    
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/testdatafile_for_columnline.xml"
            XPath="/pricelist/oilprices/price"></asp:XmlDataSource>
        <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/App_Data/testdatafile_for_columnline.xml"
            XPath="/pricelist/fuelprices/price"></asp:XmlDataSource>
</asp:Content>