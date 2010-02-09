<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Enter food consumed</h2>
    
    
    ..
    
    
    <% using (var form = Html.BeginForm()) { %>
		  Name:
		  <%= Html.TextBox(CarbonFitnessWeb.ViewConstants.FoodConstant.FoodNameElement) %>
		  
		  Meassure:
		  <%= Html.TextBox(CarbonFitnessWeb.ViewConstants.FoodConstant.FoodMessaureElement) %>
		  		  
		  <input type="submit" value="<%= CarbonFitnessWeb.ViewConstants.FoodConstant.Submit %>" />
    
    <% } %>
    

</asp:Content>
