<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<InputFoodModel>" %>
<%@ Import Namespace="CarbonFitness.Data.Model"%>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	 <script type="text/javascript">
	 	$(document).ready(function() {
	 		$("#<%=Html.IdFor(m => m.Date) %>").datepicker({ dateFormat: 'yy-mm-dd' });
	 		$("#<%=Html.IdFor(m => m.Date) %>").change(function() {
	 			$("#<%=Html.IdFor(m => m.Ingredient) %>").val("");
	 			$("#<%=Html.IdFor(m => m.Measure) %>").val("0");
	 			$("form").submit();
	 		});

	 		var actionUrl = '<%= Url.Action<IngredientController>(c => c.Search(null)) %>';
	 		$("#<%=Html.IdFor(m => m.Ingredient)%>").autocomplete(actionUrl);
	 	});
	</script>
	 
	 
	 <h1><%=FoodConstant.FoodTitle%></h1>
	 
	 <div id="menucontainer">
		<ul class="flat">
			<li><%= Html.ActionLink<FoodController>(c => c.Input(), SiteMasterConstant.FoodInputLinkText)%></li>
			<li><%= Html.ActionLink<ResultController>(c => c.Show(), SiteMasterConstant.ResultLinkText)%></li>
		</ul>
	</div>
	<hr style="clear: both" />
	 
	 <%=Html.ValidationSummary() %>
	 <% using ( Html.BeginForm()) { %>
		  <div class="input-fields">
				<div class="date-fields">
					 <div class="editor-label">
						  <%= Html.LabelFor(m => m.Date) %>
					 </div>
					 <div class="datepicker">
						  <%= Html.EditorFor(m => m.Date) %>
					 </div>
				 </div>
				
				<div class="food-input">
					 <h3><%=FoodConstant.FoodInputTitle%></h3>
					 <div class="editor-label">
						  <%= Html.LabelFor(m => m.Ingredient) %>
					 </div>
					 <div class="editor-field">
						  <%= Html.EditorFor(m => m.Ingredient) %>
					 </div>
				
					 <div class="editor-label">
						  <%= Html.LabelFor(m => m.Measure) %>
					 </div>
					 <div class="editor-field-number">
						  <%= Html.EditorFor(m => m.Measure) %>
					 </div>
					 
					 <div class="submit-field">
						<%= Html.SubmitButton("save", "Spara") %>
					 </div>
				</div>
		  </div>       
		  <hr style="clear: both" />

		  <%
		  if (Model.UserIngredients != null) {
				%>
				<table class="">
					 <tr>
						  <th>Livsmedel</th>
						  <th>Vikt (g)</th>
						  <th>Protein (g)</th>
						  <th>Fett (g)</th>
						  <th>Kolhydrater (g)</th>
						  <th>Fibrer (g)</th>
						  <th>Järn (mg)</th>
					 </tr>
				<%
				
				foreach (var userIngredient in Model.UserIngredients) {
				%>
					 <tr>
						  <td><%=userIngredient.Ingredient.Name%></td>
						  <td><%=userIngredient.Measure%></td>
						  <td><%=userIngredient.GetActualCalorieCount(x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(NutrientEntity.ProteinInG))).ToString("N2")%></td>
						  <td><%=userIngredient.GetActualCalorieCount(x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(NutrientEntity.FatInG))).ToString("N2")%></td>
						  <td><%=userIngredient.GetActualCalorieCount(x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(NutrientEntity.CarbonHydrateInG))).ToString("N2")%></td>
						  <td><%=userIngredient.GetActualCalorieCount(x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(NutrientEntity.FibresInG))).ToString("N2")%></td>
						  <td><%=userIngredient.GetActualCalorieCount(x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(NutrientEntity.IronInmG))).ToString("N2")%></td>
					</tr>
					 <%
				}
                if (Model.UserIngredients.Count() > 0) {
				%>
				    <tr class="nutrientSum">
						  <td ></td>
						  <td ></td>
						  <td ><%=Model.SumOfProtein.ToString("N2")%> / <%=Model.RDIOfProtein.ToString("N2")%></td>
						  <td ><%=Model.SumOfFat.ToString("N2")%> / <%=Model.RDIOfFat.ToString("N2")%></td>
						  <td ><%=Model.SumOfCarbonHydrates.ToString("N2")%> / <%=Model.RDIOfCarbonHydrates.ToString("N2")%></td>
						  <td ><%=Model.SumOfFiber.ToString("N2")%> / <%=Model.RDIOfFiber.ToString("N2")%></td>
						  <td ><%=Model.SumOfIron.ToString("N2")%> / <%=Model.RDIOfIron.ToString("N2")%></td>
					</tr>
				<%
		        }
		        %>  
				</table>
				<%
		  }
		  %>   
	 <% 
	 }
	 %>
</asp:Content>
