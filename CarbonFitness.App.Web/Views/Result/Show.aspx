<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ResultModel>" %>
<%@ Import Namespace="CarbonFitness.App.Web.FusionCharts"%>
<%@ Import Namespace="CarbonFitness.App.Web.ViewConstants"%>
<%@ Import Namespace="CarbonFitness.App.Web.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $("#<%=Html.IdFor(m => m.Date) %>").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#<%=Html.IdFor(m => m.Date) %>").change(function() {
                this.form.submit();
            });
        });
    </script>

    <h1><%=ResultConstant.ResultTitle%></h1>
    
    <% using (var form = Html.BeginForm()) { %>
        <div class="date-fields">
            <div class="editor-label">
                <%= Html.LabelFor(m => m.Date) %>
            </div>
            <div class="datepicker">
                <%= Html.EditorFor(m => m.Date) %>
            </div>
        </div>
        
        <div class="editor-label">
            <%= Html.LabelFor(m => m.SumOfCalories) %>
        </div>    
        <div class="editor-label" id="SumOfCalories">
            <%= Html.DisplayTextFor(m => m.SumOfCalories) %>
        </div> 
    <% } %> 
    
    <div>
		<%=Html.FusionCharts()
			.Line2D(Model.CalorieHistoryList, 860, 300, d => Convert.ToDouble(d.Value))
			.Caption("Kalori historik")
			.SubCaption("(kcal)")
			.Label(d => d.Key.ToShortDateString())
			.DecimalPrecision(0)
			.Action(d => "javascript:alert(&apos;You clicked on " + d + "&apos;);")%>				
	</div>

</asp:Content>
