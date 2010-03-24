<%@ Page Language="C#" AutoEventWireup="true" CodeFile="amColumn.aspx.cs" Inherits="amColumn" MasterPageFile="~/site.master" %>

<%@ Register Assembly="am.Charts" Namespace="am.Charts" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>
            amColumn samples</h2>
        <p>
            This sample demonstrates amColumn chart with all default settings (except category
            (x-axis) values are rotated by 90 degrees). It uses data from 2 seperate data source
            object (created from one XML file)</p>
        <p>
            <cc1:columnchart id="ColumnChart1" runat="server" categoryvaluesrotate="90" chartdirectory="~/amcharts/amcolumn/"
                dataseriesidfield="year" datasourceid="XmlDataSource1"><Graphs>
<cc1:ColumnChartGraph runat="server" DataSeriesItemIDField="year" Title="E95 price" DataSourceID="XmlDataSource2" DataValueField="e95"></cc1:ColumnChartGraph>
<cc1:ColumnChartGraph runat="server" DataSeriesItemIDField="year" Title="E98 price" DataSourceID="XmlDataSource2" DataValueField="e98"></cc1:ColumnChartGraph>
<cc1:ColumnChartGraph runat="server" DataSeriesItemIDField="year" Title="Oil price" DataSourceID="XmlDataSource1" DataValueField="value" GraphType="Line"></cc1:ColumnChartGraph>
</Graphs>
</cc1:columnchart>
            &nbsp;</p>
    
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/testdatafile_for_columnline.xml"
            XPath="/pricelist/oilprices/price"></asp:XmlDataSource>
        <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/App_Data/testdatafile_for_columnline.xml"
            XPath="/pricelist/fuelprices/price"></asp:XmlDataSource>
        <br />
        Second example uses the same datasources, however series items (x-axis) values are
        hardcoded thus filtering data only to the years we are interested in. Some visualization
        settings are tweaked too.<br />
        &nbsp;<cc1:ColumnChart ID="ColumnChart2" runat="server" AreaFillAlpha="40" Bullet="RoundOutlined"
            ColumnBorderAlpha="90" ColumnBorderColor="Black" ColumnDataLabelFormatString="{value}"
            ColumnGrowTime="5" Depth="15" LineBalloonTextFormatString="{value}" PlotAreaBackgroundAlpha="10"
            PlotAreaBackgroundColor="Yellow">
            <Series>
                <cc1:ColumnChartSeriesDataItem BackgroundColor="" SeriesItemID="1995" Value="1995" />
                <cc1:ColumnChartSeriesDataItem BackgroundColor="" SeriesItemID="2000" Value="2000" />
                <cc1:ColumnChartSeriesDataItem BackgroundColor="" SeriesItemID="2005" Value="2005" />
                <cc1:ColumnChartSeriesDataItem BackgroundColor="" SeriesItemID="2006" Value="2006" />
                <cc1:ColumnChartSeriesDataItem BackgroundColor="" SeriesItemID="2007" Value="2007" />
                <cc1:ColumnChartSeriesDataItem BackgroundColor="" SeriesItemID="2008" Value="2008" />
            </Series>
            <Graphs>
                <cc1:ColumnChartGraph runat="server" DataSeriesItemIDField="year" DataSourceID="XmlDataSource2"
                    DataValueField="e95" Title="E95 price">
                </cc1:ColumnChartGraph>
                <cc1:ColumnChartGraph runat="server" DataSeriesItemIDField="year" DataSourceID="XmlDataSource2"
                    DataValueField="e98" Title="E98 price">
                </cc1:ColumnChartGraph>
                <cc1:ColumnChartGraph runat="server" DataSeriesItemIDField="year" DataSourceID="XmlDataSource1"
                    DataValueField="value" GraphType="Line" Title="Oil price">
                </cc1:ColumnChartGraph>
            </Graphs>
        </cc1:ColumnChart>
</asp:Content>