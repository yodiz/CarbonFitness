<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ResultModel>" %>

<%@ Import Namespace="CarbonFitness.App.Web.FusionCharts" %>
<%@ Import Namespace="CarbonFitness.App.Web.ViewConstants" %>
<%@ Import Namespace="CarbonFitness.App.Web.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		<%=ResultConstant.ResultTitle%></h1>
	<% using (Html.BeginForm())
	 { %>
	<div class="editor-label">
		<strong>Your ideal weight is</strong>
		<%= Model.IdealWeight.ToString("N1") %>kg
	</div>
	<% } %>
	
	<div id="flashcontent">
		You may have to upgrade your flash player.
	</div>

	<script type="text/javascript">
		// <![CDATA[		
		var so = new SWFObject('<%=Url.Content("~/scripts/amline/amline.swf") %>', 'amline', '860', '300', '8', '#FFFFFF');
		//so.addVariable("path", "../../amline/");
		so.addVariable("settings_file", encodeURIComponent('<%=Url.Content("~/scripts/amline/amline_settings.xml") %>'));
		so.addVariable('data_file', encodeURIComponent('<%=Url.Content("~/scripts/amline/amline_data.xml") %>'));
		//so.addVariable('data_file', encodeURIComponent('<%= Url.Action<ResultController>(c => c.ShowXml()) %>'));
		so.write("flashcontent");
		// ]]>
	</script>

	<div>
		<%=Html.FusionCharts()
			.MSLine(new[] {Model.CalorieHistoryList}, 860, 300)
			.Caption("Kalori historik")
			.SubCaption("(kcal)")
			.DecimalPrecision(0)
			.Colors("00cc33")	%>
	</div>
</asp:Content>
