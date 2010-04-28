<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content runat="server" ContentPlaceHolderID="TitleContent">
	viktprognos.se - verktyg för vikt och hälsa 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">

	<div class="splash-photo"><%= Html.Image("~/Content/HomePhoto.jpg") %></div>
		
	<h1>Välkommen till Viktprognos.se!</h1>
	<p>Här kan du enkelt hålla koll på ditt intag av kalorier, din vikt och din träning.
		Resultatet är en viktprognos som ger dig full insikt om vart du är på väg. Du kan
		använda prognosen med din egen diet och lära känna vad som känns bäst för dig. Du
		behöver inte lägga hundratals kronor i månaden för att ha koll på din vikt - den
		här tjänsten är helt gratis!</p>
	<div style="height: 150px;">
	    <div style="float:left; width:49%">
	        <h1>Om oss</h1>	        
            <p>Vi är en oberoende aktör inom kost, träning 
            och hälsa som ger dig de verktyg som du 
            behöver för att lyckas med ett hälsosamt liv - helt gratis.</p>
            <br />
            <%= Html.ActionLink<HomeController>(c => c.About(), "Läs mer om oss >>")%>
        </div>
        <div style="float:right; width:49%">
	    <h1>Hur kan det vara gratis?</h1>
	    <p>
		    Webbplatsen finnasieras av kostkorten som vi säljer. Du kan ha dem i plånboken för
		    att hålla koll på din kaloriförbrukning och skriva in senare. Köp gärna dessa om
		    du gillar webbplatsen. De kostar 49 kr och räcker flera månader.<br />
		    <br />
		    Du kan även skriva ut kostkort gratis här!</p>
        </div>
    </div>
</asp:Content>
