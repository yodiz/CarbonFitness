<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<InputWeightModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%=WeightConstant.WeightTitle%></h1>
	 <% using (Html.BeginForm()) { %>
    
		 <%= Html.EditorFor(m => m.Weight)%>
	    
		 <%= Html.EditorFor(m => m.Date)%>
	    
		 <%= Html.SubmitButton("saveButton", "Spara")%>
		 
    <% } %>

</asp:Content>
