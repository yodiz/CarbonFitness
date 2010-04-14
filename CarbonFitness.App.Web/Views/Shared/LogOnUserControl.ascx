<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
	if (Request.IsAuthenticated) {
		%>
        Välkommen <b><%=Html.Encode(Page.User.Identity.Name)%></b>!
        [ <%=Html.ActionLink("Logga ut", "LogOff", "Account")%> ]
<%
	}
	else {
		%> 
        [ <%=Html.ActionLink("Logga in", "LogOn", "Account")%> ]
<%
	}
%>
