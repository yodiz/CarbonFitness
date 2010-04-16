﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProfileModel>" %>
<%@ Import Namespace="CarbonFitness.Data.Model"%>

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
		        
		         <div class="editor-label" style="clear:both;">
	                <%= Html.LabelFor(m => m.GenderTypes)%>
                </div>
                <div class="editor-field">
	               
	                <% foreach (GenderType genderType in Model.GenderTypes) {
                            string selected = "";
                            if (Model.Gender.Name == genderType.Name) {
                                selected = "checked";
                            }
                            %><%= String.Format("<input type=\"radio\" value=\"{0}\" name=\"{1}\" id=\"{2}\" {4}><label for=\"{2}\">{3}</label>", genderType.Id, "fieldName", genderType.Id, genderType.Name, selected)%><br />
                <% } %>
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
