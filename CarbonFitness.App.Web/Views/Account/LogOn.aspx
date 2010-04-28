<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LogOnModel>" %>
<%@ Import Namespace="CarbonFitness.App.Web.ViewConstants"%>
<%@ Import Namespace="CarbonFitness.App.Web.Models"%>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%= AccountConstant.LoginTitle %>
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= AccountConstant.LoginTitle %></h2>
       
    <p>
        Var vänlig skriv in ditt namn och lösen. <%= Html.ActionLink<UserController>(x => x.Create(), "Registrera konto")%> om du inte har ett konto.
    </p>
    <%= Html.ValidationSummary(true, "Inloggningen misslyckades. Var snäll att åtgärda felen och försök igen.") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Konto information</legend>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.UserName) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.Password) %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.CheckBoxFor(m => m.RememberMe) %>
                    <%= Html.LabelFor(m => m.RememberMe) %>
                </div>
                
                <p>
                    <input type="submit" value="Log On" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
