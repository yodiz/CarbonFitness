<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    viktprognos.se - verktyg för vikt och hälsa 
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Om oss</h1>
    <div style="height:515px;">
        <div style="width:500px; float:left;">
        <p>
            Tack för att du besöker våran webbplats!<p></p>

            <b>Vår grundtanke</b> är att vara en oberoende aktör inom kost, träning och hälsa. 
            Vi ger dig den kunskap och de verktyg som du behöver för att lyckas med ett hälsosamt liv - helt gratis.<p></p>

            Konkurrensen inom vikt, hälsa och träning är stentuff och det är många som är ute efter dina pengar. 
            Genom att pressentera olika mirakelmetoder, sälja mirakelprodukter, abonnemang osv. tycker vi att många företag idag vilseleder konsumenter. 
            Därför är vi <b>oberoende och gratis.</b><p></p>

            Att vara oberoende innebär för oss att vi inte ger specifika råd kring särskillda metoder utan istället informerar om och <b>sprider kunskap</b> kring många olika metoder. 
            Det är kunskapen, motivationen, de långsiktiga målen och en förändrad livsstil som leder till bättre hälsa. 
            På vår webbplats kan du läsa om och pröva dig fram till vad som fungerar för dig.<p></p>

            På sikt tänker vi bygga upp en mycket stor databas med samlad information från hundratusentals användares träningsresultat och viktnedgång. 
            Vi behandlar data som anonymt och värnar om våra användares integritet.<p></p>

            Vi strävar efter en väl fungerande webbplats som är så enkel som möjligt för dig att använda och tar gärna emot dina synpunkter så att vi kan förbättra oss!<p></p>

            Kontakta gärna oss om du på något vis vill bidra med material och artiklar till webbplatsen eller om du är intresserad av samarbeten/finansiering.<p></p>

            Till sist, om du gillar vårt arbete och vill hjälpa till, <b>informera gärna dina vänner</b> om att vi finns och arbetar för din och andras goda hälsa!<p></p>

            Med Vänlig Hälsning

            <a href="http://www.viktprognos.se">viktprognos.se</a>  
           </p> 
        </div>
        <div class="profileimage">
            <%= Html.Image("/content/about.jpg", new Dictionary<string, object> { { "style", "width:320px;" } })%>
        </div>
    </div>
    
</asp:Content>
