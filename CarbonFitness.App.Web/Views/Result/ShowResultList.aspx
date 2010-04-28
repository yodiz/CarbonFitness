<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ResultListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ShowResultList
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <%=ResultConstant.ResultTitle%>
    </h1>
    <div id="menucontainer">
        <ul class="flat">
            <li><%= Html.ActionLink<ResultController>(c => c.Show(), ResultConstant.WeightEnergyResult)%></li>
            <li>|</li>
            <li><%= Html.ActionLink<ResultController>(c => c.ShowAdvanced(), ResultConstant.AdvancedResult)%></li>
            <li>|</li>
            <li><%= Html.ActionLink<ResultController>(c => c.ShowResultList(), ResultConstant.ResultList)%></li>
        </ul>
    </div>
    <hr style="clear: both" />

    <table class="" id="nutrientsummarylist">
        <tr>
            <th>Datum</th>
			  <th>Energi (g)</th>
			  <th>Protein (g)</th>
			  <th>Fett (g)</th>
			  <th>Kolhydrater (g)</th>
			  <th>Fibrer (g)</th>
			  <th>Järn (mg)</th>
        </tr>
        <%foreach (var nutrientSum in Model.NutrientSumList) {%>
        <tr>
            <td><%=nutrientSum.Date.ToShortDateString()%></td>
            <%foreach (var nutrientValue in nutrientSum.NutrientValues) {
            %>
            <td><%=nutrientValue.Value.ToString("N2")%></td>
            <%
            }
            %>
        </tr>
        <%
        }
        %>
        <tr class="nutrientSum">
	        <td >Medelvärde:</td>
		    <%foreach (var nutrientValue in Model.NutrientAverage.NutrientValues) {%>
            <td><%=nutrientValue.Value.ToString("N2")%></td>
            <%
            }
            %>
      </tr>
   </table>
</asp:Content>
