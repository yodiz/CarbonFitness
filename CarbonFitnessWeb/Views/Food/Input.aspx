<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<InputFoodModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <script type="text/javascript">
        $(function() {
            $("#<%=Html.IdFor(m => m.Date) %>").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#<%=Html.IdFor(m => m.Ingredient)%>").autocomplete("/Ingredient/Search");
        });
	</script>
    
    
    
    <h1><%=FoodConstant.FoodTitle%></h1>
    <h2><%=FoodConstant.FoodInputTitle%></h2>
   
    <%
        if (Model.UserIngredients != null){
            foreach (var userIngredient in Model.UserIngredients)
            {%>
		        <%=userIngredient.Ingredient.Name%>, <%=userIngredient.Measure%><br />
            <%
            }
        }
    %>
    
    
    <fieldset>
    <% using (var form = Html.BeginForm()) { %>
    <div class="datepicker">
        Date: <%=Html.EditorFor(m => m.Date)%>
   </div>
        <div>
            <%= Html.LabelFor(m => m.Ingredient) %>
		    <%= Html.EditorFor(m => m.Ingredient) %>
		    
		</div>
		
		<div>
		  <%= Html.LabelFor(m => m.Measure)%>
		  <%= Html.EditorFor(m => m.Measure) %>
        </div>  		  		  
        
        <br />
        <input type="submit" value="<%= FoodConstant.Submit %>" />
    <% } %>
    </fieldset>
</asp:Content>
