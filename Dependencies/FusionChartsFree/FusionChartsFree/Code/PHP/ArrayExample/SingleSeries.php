<?php
//We've included ../Includes/FusionCharts.php, which contains functions
//to help us easily embed the charts.
include("../Includes/FusionCharts.php");
//We've also included ../Includes/FC_Colors.asp, having a list of colors
//to apply different colors to the chart's columns. We provide a function for it - getFCColor()
include("../Includes/FC_Colors.php");
?>
<HTML>
<HEAD>
	<TITLE>
	FusionCharts Free - Array Example using Single Series Column 3D Chart
	</TITLE>
	<?php
	//You need to include the following JS file, if you intend to embed the chart using JavaScript.
	//Embedding using JavaScripts avoids the "Click to Activate..." issue in Internet Explorer
	//When you make your own charts, make sure that the path to this JS file is correct. Else, you would get JavaScript errors.
	?>	
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
<BODY>

<CENTER>
<h2><a href="http://www.fusioncharts.com" target="_blank">FusionCharts Free</a> Examples</h2>
<h4>Plotting single series chart from data contained in Array.</h4>
<?php
	//In this example, we plot a single series chart from data contained
	//in an array. The array will have two columns - first one for data label/names
	//and the next one for data values.
	
	//Let's store the sales data for 6 products in our array). We also store
	//the name of products. 
	//Store Name of Products
	$arrData[0][1] = "Product A";
	$arrData[1][1] = "Product B";
	$arrData[2][1] = "Product C";
	$arrData[3][1] = "Product D";
	$arrData[4][1] = "Product E";
	$arrData[5][1] = "Product F";
	//Store sales data
	$arrData[0][2] = 567500;
	$arrData[1][2] = 815300;
	$arrData[2][2] = 556800;
	$arrData[3][2] = 734500;
	$arrData[4][2] = 676800;
	$arrData[5][2] = 648500;

	//Now, we need to convert this data into XML. We convert using string concatenation.
	//Initialize <graph> element
	$strXML = "<graph caption='Sales by Product' numberPrefix='$' formatNumberScale='0' decimalPrecision='0'>";
	//Convert data to XML and append
	foreach ($arrData as $arSubData)
		$strXML .= "<set name='" . $arSubData[1] . "' value='" . $arSubData[2] ."' color='". getFCColor() ."' />";

	//Close <graph> element
	$strXML .= "</graph>";
	
	//Create the chart - Column 3D Chart with data contained in strXML
	echo renderChart("../../FusionCharts/FCF_Column3D.swf", "", $strXML, "productSales", 600, 300);
?>
<BR><BR>
<a href='../NoChart.html' target="_blank">Unable to see the chart above?</a>
<H5 ><a href='../default.htm'>&laquo; Back to list of examples</a></h5>

</CENTER>
</BODY>
</HTML>