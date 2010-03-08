<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CarbonFitness.Model.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	View
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>View</h2>
    
    <%= this.Model.Username %>

</asp:Content>
