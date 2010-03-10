<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
	Home Page
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
	
	<div class="splash-photo"><%= Html.Image("~/Content/HomePhoto.jpg") %></div>
	
	<h1>Välkommen till Viktprognos.se</h1>
	
</asp:Content>
