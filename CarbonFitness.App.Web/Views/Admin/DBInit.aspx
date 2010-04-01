<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<InputDbInitModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DBInit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>DBInit</h2>
    
    
    <% using (Html.BeginForm()) { %>
    
		<%= Html.EditorFor(x=> x.ImportFilePath) %>
    
		<%= Html.SubmitButton(AdminConstant.RefreshDatabase) %>
    
    <% } %>
</asp:Content>
