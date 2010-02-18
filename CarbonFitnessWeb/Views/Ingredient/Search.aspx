<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CarbonFitness.Data.Model.Ingredient>>" %>
<% foreach (var item in Model) { %>
<%= item.Name %>    
<%
} %>
