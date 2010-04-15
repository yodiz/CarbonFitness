﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		<%=ProfileConstant.ProfileInputTitle%>
	</h1>
	<div class="profile-input">			
		<%=Html.ValidationSummary(ProfileConstant.InvalidIdealWeightInput) %>
		<% using (Html.BeginForm()) { %>
		<div style="height:358px;">
		    <div style="float:left;">       		
		        <div class="editor-label">
			        <%= Html.LabelFor(m => m.IdealWeight)%>
		        </div>
		        <div class="editor-field">
			        <%= Html.EditorFor(m => m.IdealWeight)%>
		        </div>
		        
		        <div class="editor-label">
			        <%= Html.LabelFor(m => m.Length)%>
		        </div>
		        <div class="editor-field">
			        <%= Html.EditorFor(m => m.Length)%>
		        </div>
		        
		        <div>
		            <div style="float:left;">		        
		                <div class="editor-label">
			                <%= Html.LabelFor(m => m.Weight)%>
		                </div>
		                <div class="editor-field">
			                <%= Html.EditorFor(m => m.Weight)%>
		                </div>
		            </div>
    		        <div style="float:left; margin-left:20px;">		 
		                <div class="editor-label">
			                <%= Html.LabelFor(m => m.BMI)%>
		                </div>
		                <div class="editor-field" id="BMIField">
			                <%= Html.DisplayFor(m => m.BMI)%>
		                </div>
		            </div>
		        </div>
		        		        
		        <div style="clear:both;">
			        <input type="submit" value="Spara" style="margin-top:20px;"/>
		        </div>
		    </div>
		    <div style="float:right;">
		        <%= Html.Image("/content/halsa.jpg", new Dictionary<string, object> { { "style", "border:1px solid #e4e4e4;" } })%>
		    </div>
		</div>
		
		
		<% } %>
	</div>
</asp:Content>
