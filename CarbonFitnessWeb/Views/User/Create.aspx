<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>
    
    <% using (var aForm = Html.BeginForm()) { %>
    
        <%= Html.TextBox("username") %>
        
        <input type="submit" name="Save" value="Save" />
    
    <% } %>

</asp:Content>
