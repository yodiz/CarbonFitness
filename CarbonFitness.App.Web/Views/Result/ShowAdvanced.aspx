<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ResultModel>" %>
<%@ Import Namespace="CarbonFitness.Translation"%>

<%@ Import Namespace="CarbonFitness.Data.Model" %>
<%@ Import Namespace="CarbonFitness.App.Web.FusionCharts" %>
<%@ Import Namespace="CarbonFitness.App.Web.ViewConstants" %>
<%@ Import Namespace="CarbonFitness.App.Web.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=Html.IdFor(m => m.GraphLineOptions) %>").dropdownchecklist({ maxDropHeight: 250 });
            $("#<%=Html.IdFor(m => m.GraphLineOptions) %>").change(function() {
            var graphLines = "" + $("#<%=Html.IdFor(m => m.GraphLineOptions) %>").val();
            graphLines = graphLines.replace(/,/gi, "&graphlines=");
            writeAmChartFlash(graphLines);
            });

            writeAmChartFlash("EnergyInKcal&graphlines=Weight");
        });

        function writeAmChartFlash(graphLines) {
            var so = new SWFObject('<%=Url.Content("~/scripts/amline/amline.swf") %>', 'amline', '600', '300', '8', '#FFFFFF');
            //so.addVariable("path", "../../amline/");
            so.addVariable("settings_file", encodeURIComponent('<%=Url.Content("~/scripts/amline/amline_settings.xml") %>?RID=<%= DateTime.Now.Ticks %>'));
            so.addVariable('data_file', encodeURIComponent('<%= Url.Action<ResultController>(c => c.ShowXml()) %>&graphlines=' + graphLines + '&RID=<%= DateTime.Now.Ticks %>'));
            //so.addVariable('data_file', encodeURIComponent('<%= Url.Content("~/scripts/amline/amline_data3.xml") %>?RID=<%= DateTime.Now.Ticks %>'));
            so.write("amChartContent");
        }
    </script>

    <h1>
        <%=ResultConstant.ResultTitle%>
    </h1>

    <div id="menucontainer">
        <ul class="flat">
            <li><%= Html.ActionLink<ResultController>(c => c.Show(), ResultConstant.WeightEnergyResult)%></li>
            <li>|</li>
            <li><%= Html.ActionLink<ResultController>(c => c.ShowAdvanced(), ResultConstant.AdvancedResult)%></li>
            <li>|</li>
            <li><%= Html.ActionLink<ResultController>(c => c.ShowResultList(), ResultConstant.ResultList)%></li>
        </ul>
	</div>
	<hr style="clear: both" />

    <% using (Html.BeginForm())
       { %>
    
        <div class="editor-label" style="margin-bottom:30px;">
            <strong>Din ideal vikt är</strong><%= Model.IdealWeight.ToString("N1") %>kg
            <p></p>
        </div>
        <div style="height:350px;">
            <div style="float: left;">
                <div id="amChartContent" style="border:1px solid #e4e4e4;">
                    You may have to upgrade your flash player.
                </div>
            </div>
            <div style="float: left; margin-left:8px;">
                <strong>Välj ämnen att visa</strong><br />
                <%= Html.DropDownListFor(m => m.GraphLineOptions, new SelectList(Model.GraphLineOptions, "Value", "Text"), new Dictionary<String, object> { { "multiple", "multiple" } })%>
            </div>
       </div>
    <% } %>
</asp:Content>
