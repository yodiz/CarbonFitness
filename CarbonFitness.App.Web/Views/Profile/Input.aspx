<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%=EnergyConstant.EnergyInputTitle%></h1>
    
    
	<div class="energy-input">
	    <h3><%=EnergyConstant.EnergyInputTitle%></h3>
	    
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
	</div>



</asp:Content>
