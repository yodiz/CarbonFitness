<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		<%=ProfileConstant.ProfileInputTitle%>
	</h1>
	<div class="profile-input">
		<h3>
			<%=ProfileConstant.ProfileInputTitle%>
		</h3>
			
		<%=Html.ValidationSummary(ProfileConstant.InvalidIdealWeightInput) %>
		<% using (Html.BeginForm()) { %>
		<div class="editor-label">
			<%= Html.LabelFor(m => m.Length)%>
		</div>
		<div class="editor-field">
			<%= Html.EditorFor(m => m.Length)%>
		</div>
		<div class="editor-label">
			<%= Html.LabelFor(m => m.IdealWeight)%>
		</div>
		<div class="editor-field">
			<%= Html.EditorFor(m => m.IdealWeight)%>
		</div>
		<div>
			<input type="submit" value="Spara" />
		</div>
		<% } %>
	</div>
</asp:Content>
