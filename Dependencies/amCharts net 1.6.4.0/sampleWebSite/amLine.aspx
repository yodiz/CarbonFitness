<%@ Page Language="C#" AutoEventWireup="true" CodeFile="amLine.aspx.cs" Inherits="amLine" MasterPageFile="~/site.master" %>

<%@ Register Assembly="am.Charts" Namespace="am.Charts" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>
            amLine samples</h2>
        <p>
            This sample demonstrates amLine chart with all default settings. It uses data from 2 seperate data source objects (created from one XML file) one for series values
            and second for 2 graphs data</p>
        <p>
            <cc1:LineChart ID="LineChart1" runat="server" DataSeriesIDField="year" DataSourceID="XmlDataSource1">
                <Graphs>
                    <cc1:LineChartGraph runat="server" BulletSize="6" DataSeriesItemIDField="year" DataSourceID="XmlDataSource2"
                        DataValueField="e95" Title="E95">
                    </cc1:LineChartGraph>
                    <cc1:LineChartGraph runat="server" BulletSize="6" DataSeriesItemIDField="year" DataSourceID="XmlDataSource2"
                        DataValueField="e98" Title="E98">
                    </cc1:LineChartGraph>
                </Graphs>
            </cc1:LineChart>
            &nbsp;</p>
    
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/testdatafile_for_columnline.xml"
            XPath="/pricelist/oilprices/price"></asp:XmlDataSource>
        <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/App_Data/testdatafile_for_columnline.xml"
            XPath="/pricelist/fuelprices/price"></asp:XmlDataSource>
        <br />
        Second example uses the same datasources, however data from first datasource is
        used for additional graph and second axis is introduced for the last graph. Some visualizations
        settings are tweaked too.<br /><br />
        <cc1:LineChart ID="LineChart2" runat="server" DataSeriesIDField="year" DataSourceID="XmlDataSource1">
            <Graphs>
                <cc1:LineChartGraph runat="server" Bullet="Square" BulletAlpha="100" BulletSize="6"
                    DataSeriesItemIDField="year" DataSourceID="XmlDataSource1" DataValueField="value"
                    FillAlpha="40" Title="Oil">
                </cc1:LineChartGraph>
                <cc1:LineChartGraph runat="server" Bullet="RoundOutlined" BulletAlpha="100" BulletSize="3"
                    DataSeriesItemIDField="year" DataSourceID="XmlDataSource2" DataValueField="e95"
                    FillAlpha="20" FillColor="Navy" Title="E95">
                </cc1:LineChartGraph>
                <cc1:LineChartGraph runat="server" Axis="Right" Bullet="RoundOutlined" BulletAlpha="100"
                    BulletColor="Fuchsia" BulletSize="10" DataSeriesItemIDField="year" DataSourceID="XmlDataSource2"
                    DataValueField="e98" LineWidth="3" Title="E98">
                </cc1:LineChartGraph>
            </Graphs>
        </cc1:LineChart>
</asp:Content>