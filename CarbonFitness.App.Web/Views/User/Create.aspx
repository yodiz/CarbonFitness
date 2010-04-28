<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="CarbonFitness.App.Web.ViewConstants"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	viktprognos.se - Skapa användare
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Skapa en ny användare</h2>
    
    <% using (Html.BeginForm()) {%>
            <div class="editor-label">
		        Användarnamn:
	        </div>
	        <div class="editor-field">
		        <%=Html.TextBox(UserConstant.UsernameElement)%>
	        </div>
            <div class="editor-label">
		        Lösenord:
	        </div>
	        <div class="editor-field">
		        <%=Html.TextBox(UserConstant.PasswordElement, "", new Dictionary<string, object> {{"type", "password"}})%>
	        </div>

        <input type="submit" name="Save" value="<%= UserConstant.SaveElement %>" />
    
    <% } %>

</asp:Content>
