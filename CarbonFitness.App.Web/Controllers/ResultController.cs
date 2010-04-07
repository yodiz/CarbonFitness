using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using AutoMapper;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.UnitHistory;
using MvcContrib.ActionResults;

namespace CarbonFitness.App.Web.Controllers {
    [HandleError]
    public class ResultController : Controller {
        private readonly IGraphBuilder graphBuilder;
        private readonly IUserContext userContext;
        private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;
        private readonly IUserWeightBusinessLogic userWeightBusinessLogic;
        private readonly INutrientBusinessLogic nutrientBusinessLogic;

        public ResultController(IUserProfileBusinessLogic userProfileBusinessLogic, IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext, IGraphBuilder graphBuilder, IUserWeightBusinessLogic userWeightBusinessLogic, INutrientBusinessLogic nutrientBusinessLogic)
        {
            this.userIngredientBusinessLogic = userIngredientBusinessLogic;
            this.userContext = userContext;
            this.graphBuilder = graphBuilder;
            this.userWeightBusinessLogic = userWeightBusinessLogic;
            this.nutrientBusinessLogic = nutrientBusinessLogic;
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        [Authorize]
        public ActionResult Show() {
            var model = new ResultModel();
            model.Nutrients = nutrientBusinessLogic.GetNutrients();
            //model.CalorieHistoryList = userIngredientBusinessLogic.GetCalorieHistory(userContext.User);
            model.IdealWeight = userProfileBusinessLogic.GetIdealWeight(userContext.User);
            return View(model);
        }

        //For WatiN testing purposes the result needs to go inside an html result.
        [Authorize]
        public ActionResult ShowXmlInsideHtml() {
            return new ContentResult {
                Content = "<html><head></head><body>" + getAmChartAsXml() + "</body></html>",
                ContentEncoding = Encoding.UTF8,
                ContentType = "text/html"
            };
        }

        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ShowXml() {
            return new ContentResult {
                Content = getAmChartAsXml(),
                ContentType = "text/xml"
            };
        }

        private AmChartData getAmChartData() {
            ILine calorieLine = userIngredientBusinessLogic.GetCalorieHistory(userContext.User);
            Graph graph = graphBuilder.GetGraph(calorieLine);

            var amChartData = new AmChartData();

            Mapper.Map(graph, amChartData);
            return amChartData;
        }

        private string getAmChartAsXml() {
            AmChartData amChartData = getAmChartData();
            return serializeAmChartData(amChartData);
        }

        private string serializeAmChartData(AmChartData amChartData) {
            var serializer = new XmlSerializer(typeof(AmChartData));
            var writer = new StringWriter();
            serializer.Serialize(writer, amChartData);
            return writer.ToString();
        }

        [Authorize]
        public XmlResult ShowWeightPrognosisXml() {
            Graph graph = graphBuilder.GetGraph(userWeightBusinessLogic.GetProjectionList(userContext.User));
            var amChartData = new AmChartData();

            Mapper.Map(graph, amChartData);

            return new XmlResult(amChartData);
        }
    }
}