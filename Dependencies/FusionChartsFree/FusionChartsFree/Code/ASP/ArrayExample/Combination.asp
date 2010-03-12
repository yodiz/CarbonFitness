<%@ Language=VBScript %>
<HTML>
<HEAD>
	<TITLE>
	FusionCharts Free - Array Example using Combination Column 3D Line Chart
	</TITLE>
	<%
	'You need to include the following JS file, if you intend to embed the chart using JavaScript.
	'Embedding using JavaScripts avoids the "Click to Activate..." issue in Internet Explorer
	'When you make your own charts, make sure that the path to this JS file is correct. Else, you would get JavaScript errors.
	%>	
	<SCRIPT LANGUAGE="Javascript" SRC="../../FusionCharts/FusionCharts.js"></SCRIPT>
	<style type="text/css">
	<!--
	body {
		font-family: Arial, Helvetica, sans-serif;
		font-size: 12px;
	}
	-->
	</style>
</HEAD>
	<%
	'We've included ../Includes/FusionCharts.asp, which contains functions
	'to help us easily embed the charts.
	%>
<!-- #INCLUDE FILE="../Includes/FusionCharts.asp" -->

<BODY>

<CENTER>
<h2><a href="http://www.fusioncharts.com" target="_blank">FusionCharts Free</a> Examples</h2>
<h4>Plotting Combination chart from data contained in Array.</h4>
<%
	'In this example, we plot a Combination chart from data contained
	'in an array. The array will have three columns - first one for Quarter Name
	'second one for sales figure and third one for quantity. 
		
	Dim arrData(4,3)
	'Store Quarter Name
	arrData(0,1) = "Quarter 1"
	arrData(1,1) = "Quarter 2"
	arrData(2,1) = "Quarter 3"
	arrData(3,1) = "Quarter 4"
	'Store revenue data
	arrData(0,2) = 576000
	arrData(1,2) = 448000
	arrData(2,2) = 956000
	arrData(3,2) = 734000	
	'Store Quantity
	arrData(0,3) = 576
	arrData(1,3) = 448
	arrData(2,3) = 956
	arrData(3,3) = 734
	
	'Now, we need to convert this data into combination XML. 
	'We convert using string concatenation.
	'strXML - Stores the entire XML
	'strCategories - Stores XML for the <categories> and child <category> elements
	'strDataRev - Stores XML for current year's sales
	'strDataQty - Stores XML for previous year's sales
	Dim strXML, strCategories, strDataRev, strDataQty, i
	
	'Initialize <graph> element
	strXML = "<graph caption='Product A - Sales Details' PYAxisName='Revenue' SYAxisName='Quantity (in Units)' numberPrefix='$' formatNumberScale='0' showValues='0' decimalPrecision='0' anchorSides='10' anchorRadius='3' anchorBorderColor='FF8000'>"
	
	'Initialize <categories> element - necessary to generate a multi-series chart
	strCategories = "<categories>"
	
	'Initiate <dataset> elements
	strDataRev = "<dataset seriesName='Revenue' color='AFD8F8' >"
	strDataQty = "<dataset seriesName='Quantity' parentYAxis='S' color='FF8000' >"
	
	
	'Iterate through the data	
	For i=0 to UBound(arrData)-1
		'Append <category name='...' /> to strCategories
		strCategories = strCategories & "<category name='" & arrData(i,1) & "' />"
		'Add <set value='...' color='...'/> to both the datasets
	
		strDataRev = strDataRev & "<set value='" & arrData(i,2) & "' />"
		strDataQty = strDataQty & "<set value='" & arrData(i,3) & "' />"		
	Next
	
	'Close <categories> element
	strCategories = strCategories & "</categories>"
	
	'Close <dataset> elements
	strDataRev = strDataRev & "</dataset>"
	strDataQty = strDataQty & "</dataset>"
	
	'Assemble the entire XML now
	strXML = strXML & strCategories & strDataRev & strDataQty & "</graph>"
	
	'Create the chart - MS Column 3D Line Combination Chart with data contained in strXML
	Call renderChart("../../FusionCharts/FCF_MSColumn3DLineDY.swf", "", strXML, "productSales", 600, 300)
%>
<BR><BR>
<a href='../NoChart.html' target="_blank">Unable to see the chart above?</a><BR>
<H5 ><a href='../default.htm'>&laquo; Back to list of examples</a></h5>
</CENTER>
</BODY>
</HTML>