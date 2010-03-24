<%@ Page Language="C#" MasterPageFile="~/site.master" %>

<%@ Register Assembly="am.Charts" Namespace="am.Charts" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>
            amPie With Data From Data Source</h2>
        <p>
            This example demonstrates how to bind amPie chart to a data source object. Some of the visual settings are modified too.</p>
        <p>
            <cc1:PieChart ID="PieChart1" runat="server" 
                BorderAlpha="100" BorderColor="#00C000" DataLabelHidePercent="5" DataSourceID="XmlDataSource1"
                DataTitleField="name" DataUrlField="wikipediaUrl" DataValueField="percents" Height="450px"
                PieAngle="20" PieHeight="10" PieInnerRadius="30" Width="100%" SliceBaseColor="#008800" SliceBrightnessStep="30" 
                BackColor="Black" BackgroundAlpha="100" ForeColor="White" LegendBackgroundAlpha="20" LegendBorderColor="Lime" >
                <Labels>
                    <cc1:ChartLabel Align="Center" Text="&lt;b&gt;Ethnic composition in Lithuania&lt;/b&gt;"
                        TextColor="Yellow" TextSize="20" />
                </Labels>
            </cc1:PieChart>
            &nbsp;</p>
        
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/sampledata.xml"
            XPath="//nation"></asp:XmlDataSource>

</asp:Content>