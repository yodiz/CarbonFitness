<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Ingredient>>" %>
<%@ Import Namespace="CarbonFitness.Data.Model"%>
<%
	foreach (var item in Model) {%>
<%=item.Name%>    
<%
	}%>
