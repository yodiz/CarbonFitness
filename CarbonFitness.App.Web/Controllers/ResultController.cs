using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using AutoMapper;
using CarbonFitness.App.Web.Controllers.ViewTypeConverters;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;
using MvcContrib.ActionResults;

namespace CarbonFitness.App.Web.Controllers {
    [HandleError]
    public class ResultController : Controller {
        private readonly IGraphBuilder graphBuilder;
        private readonly IUserContext userContext;
        private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;
        private readonly IUserWeightBusinessLogic userWeightBusinessLogic;
        private readonly IGraphLineOptionViewTypeConverter graphLineOptionViewTypeConverter;

        public ResultController(IUserProfileBusinessLogic userProfileBusinessLogic, IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext, IGraphBuilder graphBuilder, IUserWeightBusinessLogic userWeightBusinessLogic, IGraphLineOptionViewTypeConverter graphLineOptionViewTypeConverter) {
            this.graphLineOptionViewTypeConverter = graphLineOptionViewTypeConverter;
            this.userIngredientBusinessLogic = userIngredientBusinessLogic;
            this.userContext = userContext;
            this.graphBuilder = graphBuilder;
            this.userWeightBusinessLogic = userWeightBusinessLogic;
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        [Authorize]
        public ActionResult Show() {
            return View(null as ResultModel);
        }

        [Authorize]
        public ActionResult ShowAdvanced() {
            var model = new ResultModel();
            model.GraphLineOptions = graphLineOptionViewTypeConverter.GetViewTypes(userContext.User);
            model.IdealWeight = userProfileBusinessLogic.GetIdealWeight(userContext.User);

            return View(model);
        }

        [Authorize]
        public ActionResult ShowResultList() {
            var nutrients = new List<NutrientEntity> {
                NutrientEntity.EnergyInKcal,
                NutrientEntity.ProteinInG,
                NutrientEntity.FatInG,
                NutrientEntity.CarbonHydrateInG,
                NutrientEntity.FibresInG,
                NutrientEntity.IronInmG
            };
            var model = new ResultListModel {
                NutrientSumList = userIngredientBusinessLogic.GetNutrientSumList(nutrients, userContext.User),
                NutrientAverage =  userIngredientBusinessLogic.GetNutrientAverage(nutrients, userContext.User)
            };
            return View(model);
        }

        //For WatiN testing purposes the result needs to go inside an html result.    
        [Authorize]
        public ActionResult ShowXmlInsideHtml(params string[] graphLines) {
            return new ContentResult {
                Content = "<html><head></head><body>" + getAmChartAsXml(
                    graphLineOptionViewTypeConverter.GetNutrientEntitys(graphLines), 
                    graphLineOptionViewTypeConverter.shouldShowWeight(graphLines)) + "</body></html>",
                    ContentEncoding = Encoding.UTF8,
                    ContentType = "text/html"
            };
        }

        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ShowXml(params string[] graphLines) {
            return new ContentResult {
                Content = getAmChartAsXml(
                    graphLineOptionViewTypeConverter.GetNutrientEntitys(graphLines), 
                    graphLineOptionViewTypeConverter.shouldShowWeight(graphLines)),
                    ContentType = "text/xml"
            };
        }


        private string getAmChartAsXml(IEnumerable<NutrientEntity> nutrients, bool showWeight) {
            var amChartData = getAmChartData(nutrients, showWeight);
            return serializeAmChartData(amChartData);
        }

        private AmChartData getAmChartData(IEnumerable<NutrientEntity> nutrients, bool showWeight) {
            var graph = GetGraph(nutrients, showWeight);

            var amChartData = new AmChartData();

            Mapper.Map(graph, amChartData);
            return amChartData;
        }

        public Graph GetGraph(IEnumerable<NutrientEntity> nutrients, bool showWeight) {
            var lines = new List<ILine>();
            getWeightLine(showWeight, lines);
            getNutrientLines(nutrients, lines);
            return graphBuilder.GetGraph(lines.ToArray());
        }

        private void getWeightLine(bool showWeight, ICollection<ILine> lines) {
            if(showWeight) {
                var line = userWeightBusinessLogic.GetHistoryLine(userContext.User);
                lines.Add(line);
            }
        }

        private void getNutrientLines(IEnumerable<NutrientEntity> nutrients, ICollection<ILine> lines) {
            if(nutrients != null) {
                foreach (var nutrient in nutrients) {
                    var line = userIngredientBusinessLogic.GetNutrientHistory(nutrient, userContext.User);
                    lines.Add(line);
                }
            }
        }

        private string serializeAmChartData(AmChartData amChartData) {
            var serializer = new XmlSerializer(typeof(AmChartData));
            var writer = new StringWriter();
            serializer.Serialize(writer, amChartData);
            return writer.ToString();
        }

        [Authorize]
        public XmlResult ShowWeightPrognosisXml() {
            var graph = graphBuilder.GetGraph(userWeightBusinessLogic.GetProjectionList(userContext.User));
            var amChartData = new AmChartData();

            Mapper.Map(graph, amChartData);

            return new XmlResult(amChartData);
        }
    }
}