<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ResultModel>" %>

<%@ Import Namespace="CarbonFitness.Data.Model" %>
<%@ Import Namespace="CarbonFitness.App.Web.FusionCharts" %>
<%@ Import Namespace="CarbonFitness.App.Web.ViewConstants" %>
<%@ Import Namespace="CarbonFitness.App.Web.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=Html.IdFor(m => m.Nutrients) %>").dropdownchecklist({ maxDropHeight: 100 });
            $("#<%=Html.IdFor(m => m.Nutrients) %>").change(function() {
                var nutrients = "" + $("#<%=Html.IdFor(m => m.Nutrients) %>").val();            
                nutrients = nutrients.replace(/,/gi, "&nutrients=");
                writeAmChartFlash(nutrients);
            });

            writeAmChartFlash("EnergyInKcal");
        });

        function writeAmChartFlash(nutrients) {
            var so = new SWFObject('<%=Url.Content("~/scripts/amline/amline.swf") %>', 'amline', '860', '300', '8', '#FFFFFF');
            //so.addVariable("path", "../../amline/");
            so.addVariable("settings_file", encodeURIComponent('<%=Url.Content("~/scripts/amline/amline_settings.xml") %>?RID=<%= DateTime.Now.Ticks %>'));
            so.addVariable('data_file', encodeURIComponent('<%= Url.Action<ResultController>(c => c.ShowXml()) %>&Nutrients=' + nutrients + '&RID=<%= DateTime.Now.Ticks %>'));
            //so.addVariable('data_file', encodeURIComponent('<%= Url.Content("~/scripts/amline/amline_data2.xml") %>?RID=<%= DateTime.Now.Ticks %>'));
            so.write("amChartContent");
        }
    </script>

    <h1>
        <%=ResultConstant.ResultTitle%></h1>
    <% using (Html.BeginForm())
       { %>
    <div class="editor-label">
        <strong>Din ideal vikt är</strong>
        <%= Model.IdealWeight.ToString("N1") %>kg
    </div>
    <% } %>
    <div id="amChartContent">
        You may have to upgrade your flash player.
    </div>
    
    <div class="editor-label">
        <strong>Välj spårämnen för grafen</strong>
        <!--Selected options-->
        <%= Html.DropDownListFor(m => m.Nutrients, new SelectList(Model.Nutrients, "Name", "Name"), new Dictionary<String, object> { { "multiple", "multiple" } })%>
    </div>
</asp:Content>
