<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<InputFoodModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%= FoodConstant.FoodTitle %></h1>
    <h2><%= FoodConstant.FoodInputTitle %></h2>
    
    
    <% foreach (var userIngredient in Model.UserIngredients) { %>
		<%= userIngredient.Ingredient.Name %>, <%= userIngredient.Measure %><br />
    <% } %>
    
    
    <% using (var form = Html.BeginForm()) { %>
		  Name:
		  <%= Html.EditorFor(m => m.Ingredient) %>
		  
		  Meassure:
		  <%= Html.EditorFor(m => m.Measure) %>
		  		  
		  <input type="submit" value="<%= FoodConstant.Submit %>" />
    
    <% } %>
    

</asp:Content>
