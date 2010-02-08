<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="CarbonFitnessWeb.ViewConstants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create a new user</h2>
    
    <% using (var aForm = Html.BeginForm()) { %>
    
        <div>
        Username
        <%= Html.TextBox(UserConstant.UsernameElement) %>
        </div>
        <div>
        Password
        <%= Html.TextBox(UserConstant.PasswordElement)%>
        </div>
        <input type="submit" name="Save" value="<%= UserConstant.SaveElement %>" />
    
    <% } %>

</asp:Content>
