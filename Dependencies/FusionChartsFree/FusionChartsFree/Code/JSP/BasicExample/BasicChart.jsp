<HTML>
	<HEAD>
		<TITLE>FusionCharts Free - Simple Column 3D Chart</TITLE>
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
			<h4>Basic example using pre-built Data.xml</h4>
			<%
				
				/*
				This page demonstrates the ease of generating charts using FusionCharts.
				For this chart, we've used a pre-defined Data.xml (contained in /Data/ folder)
				Ideally, you would NOT use a physical data file. Instead you'll have 
				your own JSP to create the XML data document. Such examples are also present.
				For a head-start, we've kept this example very simple.
				*/
				
				//Create the chart - Column 3D Chart with data from Data/Data.xml
			%> 
				<jsp:include page="../Includes/FusionChartsHTMLRenderer.jsp" flush="true"> 
					<jsp:param name="chartSWF" value="../../FusionCharts/FCF_Column3D.swf" /> 
					<jsp:param name="strURL" value="Data/Data.xml" /> 
					<jsp:param name="strXML" value="" /> 
					<jsp:param name="chartId" value="myFirst" /> 
					<jsp:param name="chartWidth" value="600" /> 
					<jsp:param name="chartHeight" value="300" /> 
					<jsp:param name="debugMode" value="false" /> 	
				</jsp:include>
			
			<BR>
			<BR>
			<a href='../NoChart.html' target="_blank">Unable to see the chart
			above?</a>
			<BR><H5 ><a href='../default.htm'>&laquo; Back to list of examples</a></h5>
		</CENTER>
	</BODY>
</HTML>
