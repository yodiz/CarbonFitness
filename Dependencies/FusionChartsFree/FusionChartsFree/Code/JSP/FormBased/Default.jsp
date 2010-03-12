<HTML>
	<HEAD>
		<TITLE>FusionCharts Free - Form Based Data Charting Example</TITLE>
		<style type="text/css">
			<!--
			body {
				font-family: Arial, Helvetica, sans-serif;
				font-size: 12px;
			}
			.text{
				font-family: Arial, Helvetica, sans-serif;
				font-size: 12px;
			}
			-->
		</style>
	</HEAD>
	<BODY>
		<%
		/*
		In this example, we first present a form to the user, to input data.
		For a demo, we present a very simple form intended for a Restaurant to indicate
		sales of its various product categories at lunch time (for a week). 
		The form is rendered in this page (Default.jsp). It submits its data to
		Chart.jsp. In Chart.jsp, we retrieve this data, convert into XML and then
		render the chart.
		*/
		
		//So, basically this page is just a form. 
		%>
		<CENTER>
			<h2><a href="http://www.fusioncharts.com" target="_blank">FusionCharts Free</a> Form-Based Data Example</h2>
			<p class='text'>Please enter how many items of each category you
			sold within this week. We'll plot this data on a Pie chart.</p>
			<p class='text'>To keep things simple, we're not validating for
			non-numeric data here. So, please enter valid numeric values only. In
			your real-world applications, you can put your own validators.</p>
			<FORM NAME='SalesForm' ACTION='Chart.jsp' METHOD='POST'>
			<table width='50%' align='center' cellpadding='2' cellspacing='1'
				border='0' class='text'>
				<tr>
					<td width='50%' align='right'><B>Soups:</B> &nbsp;</td>
					<td width='50%'><input type='text' size='5' name='Soups'
						value='108'> bowls</td>
				</tr>
				<tr>
					<td width='50%' align='right'><B>Salads:</B> &nbsp;</td>
					<td width='50%'><input type='text' size='5' name='Salads'
						value='162'> plates</td>
				</tr>
				<tr>
					<td width='50%' align='right'><B>Sandwiches:</B> &nbsp;</td>
					<td width='50%'><input type='text' size='5' name='Sandwiches'
						value='360'> pieces</td>
				</tr>
				<tr>
					<td width='50%' align='right'><B>Beverages:</B> &nbsp;</td>
					<td width='50%'><input type='text' size='5' name='Beverages'
						value='171'> cans</td>
				</tr>
				<tr>
					<td width='50%' align='right'><B>Desserts:</B> &nbsp;</td>
					<td width='50%'><input type='text' size='5' name='Desserts'
						value='99'> plates</td>
				</tr>
				<tr>
					<td width='50%' align='right'>&nbsp;</td>
					<td width='50%'><input type='submit' value='Chart it!'></td>
				</tr>
				</table>
			</FORM>
			<BR><H5 ><a href='../default.htm'>&laquo; Back to list of examples</a></h5>
		</CENTER>
	</BODY>
</HTML>
