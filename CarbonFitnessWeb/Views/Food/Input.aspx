﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<InputFoodModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#<%=Html.IdFor(m => m.Date) %>").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#<%=Html.IdFor(m => m.Date) %>").change(function() {
                $("input[id$='<%=Html.IdFor(m => m.Ingredient) %>']").val("");
                $("input[id$='<%=Html.IdFor(m => m.Measure) %>']").val("0");
                this.form.submit();
            });
            $("#<%=Html.IdFor(m => m.Ingredient)%>").autocomplete("/Ingredient/Search");
        });
	</script>
    
    <h1><%=FoodConstant.FoodTitle%></h1>
    
    <%=Html.ValidationSummary() %>
    <% using (var form = Html.BeginForm()) { %>    
        <div class="input-fields">
            <div class="date-fields">            
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Date) %>
                </div>
                <div class="datepicker">
                    <%= Html.EditorFor(m => m.Date) %>
                </div>
             </div>
            
            <div class="food-input">                
                <h3><%=FoodConstant.FoodInputTitle%></h3>
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Ingredient) %>
                </div>
                <div class="editor-field">
                    <%= Html.EditorFor(m => m.Ingredient) %>
                </div>
            
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Measure) %>
                </div>
                <div class="editor-field-number">
                    <%= Html.EditorFor(m => m.Measure) %>
                </div>
                
                <div class="submit-field">
                    <input type="image" src="../../Content/save.gif" onclick="javascript: this.form.submit();" />
                </div>
            </div>  
        </div>       
        <hr style="clear: both" />

        <%
        if (Model.UserIngredients != null){
            %>
            <table class="">
                <tr>
                    <th>Livsmedel</th>
                    <th>Vikt (g)</th>
                </tr>
            <%
            
            foreach (var userIngredient in Model.UserIngredients)
            {
                %>
                <tr>
                    <td><%=userIngredient.Ingredient.Name%></td>
                    <td><%=userIngredient.Measure%></td>
                </tr>
                <%
            }
            
            %>
            </table>
            <%
        }
        %>   
    <% 
    }
    %>
</asp:Content>
