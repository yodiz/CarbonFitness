﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<InputWeightModel>" %>
<%@ Import Namespace="CarbonFitness.Data.Model"%>
<%@ Import Namespace="CarbonFitness.App.Web.FusionCharts"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function() {
			$("#<%=Html.IdFor(m => m.Date) %>").datepicker({ dateFormat: 'yy-mm-dd' });
			$("#<%=Html.IdFor(m => m.Date) %>").change(function() {
				var date = $("#<%=Html.IdFor(m => m.Date) %>").attr("value");
				window.location = '<%=Url.Action<WeightController>(c => c.Input(null as DateTime?))%>' + '/' + date;
			});
		});
	</script>
	
	<%=Html.ValidationSummary() %>
    <h1><%=WeightConstant.WeightTitle%></h1>
	 <% using ( Html.BeginForm()) { %>
			<div class="date-fields">
				 <div class="editor-label">
					  <%= Html.LabelFor(m => m.Date) %>
				 </div>
				 <div class="datepicker">
					  <%= Html.EditorFor(m => m.Date) %>
				 </div>
			 </div>
			 <div class="food-input">
				 <div class="editor-label">
					  <%= Html.LabelFor(m => m.Weight)%>
				 </div>
				 <div class="editor-field">
					  <%= Html.EditorFor(m => m.Weight)%>
				 </div>				
				<div class="submit-field">
					<%= Html.SubmitButton("saveButton", "Spara")%>
				</div>
			</div>
			
			<div>
				<%=Html.FusionCharts()
					.Line2D(Model.UserWeightHistoryList, 860, 300, d => Convert.ToDouble(d.Weight))
					.Caption("Vikt historik")
					.SubCaption("(kg)")
					.Label(d => d.Date.ToShortDateString(), 1)
					.DecimalPrecision(1)	%>				
			</div>
		 
    <% } %>

</asp:Content>
