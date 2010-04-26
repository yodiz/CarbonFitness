<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProfileModel>" %>
<%@ Import Namespace="CarbonFitness.Data.Model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		<%=ProfileConstant.ProfileInputTitle%>
	</h1>
	<div class="profile-input">			
		<%=Html.ValidationSummary(ProfileConstant.InvalidInput) %>
		<% using (Html.BeginForm()) { %>
		<div class="profilecontent">
		    <div class="profileinput">       		
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
		                
                <div class="editor-label">
	                <%= Html.LabelFor(m => m.Weight)%>
                </div>
                <div class="editor-field">
	                <%= Html.EditorFor(m => m.Weight)%>
                </div>
		        
		        <div class="editor-label">
	                <%= Html.LabelFor(m => m.Age)%>
                </div>
                <div class="editor-field">
	               <%= Html.EditorFor(m => m.Age)%>
                </div>
		        
                 <div class="editor-label">
                    <%= Html.LabelFor(m => m.GenderViewTypes)%>
                </div>
                <div class="editor-field">
                    <% foreach (var genderType in Model.GenderViewTypes) {%>
                            <%=Html.RadioButton("SelectedGender", genderType.Text, genderType.Selected)%><label><%=genderType.Text%></label>
                    <% } %>
                </div>

                <div class="editor-label">
                    <%= Html.LabelFor(m => m.ActivityLevelViewTypes)%>
                </div>
                <div class="editor-field">
                    <% foreach (var activityType in Model.ActivityLevelViewTypes) {%>
                            <%=Html.RadioButton("SelectedActivityLevel", activityType.Text, activityType.Selected)%><label><%=activityType.Text%></label>
                    <% } %>
                </div>
		        		        
		        <div style="clear:both;">
			        <input type="submit" value="Spara" style="margin-top:20px;"/>
		        </div>
		    </div>
		    <div class="profileresult" >		   	 
                <div class="editor-label">
	                <%= Html.LabelFor(m => m.BMI)%>
                </div>
                <div class="editor-field" id="BMIField">
	                <%= Html.DisplayFor(m => m.BMI)%>
                </div>
                 <div class="editor-label">
                    <%= Html.LabelFor(m => m.BMR)%>
                </div>
                <div class="editor-field" id="BMRField">
                    <%= Html.DisplayFor(m => m.BMR)%> Kcal / dag
                </div>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.DailyCalorieNeed)%>
                </div>
                <div class="editor-field" id="DailyCalorieNeedField">
                    <%= Html.DisplayFor(m => m.DailyCalorieNeed)%> Kcal / dag
                </div>
		    </div>
		    <div class="profileimage">
		        <%= Html.Image("/content/halsa.jpg", new Dictionary<string, object> { { "style", "border:1px solid #e4e4e4;" } })%>
		    </div>
		</div>
		
		
		<% } %>
	</div>
</asp:Content>
