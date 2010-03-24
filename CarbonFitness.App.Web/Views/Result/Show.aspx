<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ResultModel>" %>

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

    <h1><%=ResultConstant.ResultTitle%></h1>
    
    
    <% using (var form = Html.BeginForm()) { %>
               
        <div class="editor-label">
            <strong>Your ideal weight is</strong>
            <%= this.Model.IdealWeight.ToString("N2") %>kg (click to change)
        </div>
         
    <% } %> 
    
    
	<%= Html.Script("~/Scripts/Amcharts/Amxy/swfobject.js")%>
	
<%
    StringBuilder pointData = new StringBuilder();
    foreach (var calorieHistory in this.Model.CalorieHistoryList.OrderBy(x=>x.Date)) {
        pointData.Append("<point x='" + calorieHistory.Date.ToShortDateString() + "' y='" + calorieHistory.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' value='1' />");
    }
%>

    <div id="amcharts_1268916729453">You need to upgrade your Flash Player</div>
    <script type="text/javascript">
        var so = new SWFObject("<%= Url.Content("~/Scripts/Amcharts/Amxy/amxy.swf")%>", "amxy", "800", "400", "8", "#FFFFFF");
        so.addVariable("path", "<%= Url.Content("~/Scripts/Amcharts/Amxy/")%>");
        so.addVariable("chart_settings", encodeURIComponent("<settings><background><alpha>100</alpha><border_alpha>20</border_alpha></background><grid><x><alpha>5</alpha></x><y><alpha>0</alpha><fill_color>000000</fill_color><fill_alpha>5</fill_alpha></y></grid><values><x><type>date</type><skip_first>0</skip_first></x></values><balloon><enabled>0</enabled></balloon><help><balloon><text>Select the area to enlarge</text></balloon></help><bullets><hover_brightness>20</hover_brightness><grow_time>2</grow_time><sequenced_grow>1</sequenced_grow></bullets><graphs><graph gid='0'><color>B92F2F</color><width>1</width><balloon_text>{value}</balloon_text><bullet>bubble</bullet><bullet_size>2</bullet_size><bullet_max_size>10</bullet_max_size><bullet_alpha>59</bullet_alpha></graph></graphs><labels><label lid='0'><text><![CDATA[<b>Bubble chart example</b>]]></text><x>10</x><y>20</y><align>center</align></label></labels></settings>"));
        so.addVariable("chart_data", encodeURIComponent("<chart><graphs><graph gid='0'><%=pointData.ToString() %></graph></graphs></chart>"));
        so.write("amcharts_1268916729453");
    </script>


</asp:Content>
