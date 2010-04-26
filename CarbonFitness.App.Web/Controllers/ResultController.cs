using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using AutoMapper;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;
using CarbonFitness.Translation;
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
        private readonly INutrientTranslator nutrientTranslator;

        public ResultController(IUserProfileBusinessLogic userProfileBusinessLogic, IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext, IGraphBuilder graphBuilder, IUserWeightBusinessLogic userWeightBusinessLogic, INutrientBusinessLogic nutrientBusinessLogic, INutrientTranslator nutrientTranslator)
        {
            this.nutrientTranslator = nutrientTranslator;
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
            model.Nutrients = getNutrientOptions();
            model.IdealWeight = userProfileBusinessLogic.GetIdealWeight(userContext.User);
            return View(model);
        }

        private IEnumerable<SelectListItem> getNutrientOptions() {
            var nutrientOptions = new List<SelectListItem>();
            var nutrients =  nutrientBusinessLogic.GetNutrients();
            foreach (var nutrient in nutrients) {
                var option = new SelectListItem {
                    Text = nutrientTranslator.GetString(nutrient.Name),
                    Value= nutrient.Name,
                    Selected = false
                };
                nutrientOptions.Add(option);
            }
            return nutrientOptions;
        }

        //For WatiN testing purposes the result needs to go inside an html result.    
        [Authorize]
        public ActionResult ShowXmlInsideHtml(params string[] graphLines) {
            return new ContentResult {
                Content = "<html><head></head><body>" + getAmChartAsXml(GetNutrientEntitys(graphLines), shouldShowWeight(graphLines)) + "</body></html>",
                ContentEncoding = Encoding.UTF8,
                ContentType = "text/html"
            };
        }

        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ShowXml(params string[] graphLines) {
            return new ContentResult {
                Content = getAmChartAsXml(GetNutrientEntitys(graphLines), shouldShowWeight(graphLines)),
                ContentType = "text/xml"
            };
        }

        private string getAmChartAsXml(IEnumerable<NutrientEntity> nutrients, bool showWeight) {
            AmChartData amChartData = getAmChartData(nutrients, showWeight);
            return serializeAmChartData(amChartData);
        }

        private AmChartData getAmChartData(IEnumerable<NutrientEntity> nutrients, bool showWeight) {
            Graph graph = GetGraph(nutrients, showWeight);

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
            foreach (var nutrient in nutrients) {
                var line = userIngredientBusinessLogic.GetNutrientHistory(nutrient, userContext.User);
                lines.Add(line);
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

        public NutrientEntity[] GetNutrientEntitys(string[] nutrients) {
            var nutrientEntities = new List<NutrientEntity>();
            foreach (var s in nutrients) {
                try {
                    nutrientEntities.Add((NutrientEntity)Enum.Parse(typeof(NutrientEntity), s));
                } catch(ArgumentException ) {} //TODO:Please fix. When calling action the System.String gets added to Nutrient Collection
            }
            return nutrientEntities.ToArray();
        }

        public bool shouldShowWeight(string[] graphLines) {
            foreach (var s in graphLines) {
                if (s.Equals("Weight")) {
                    return true;
                }
            }
            return false;
        }
    }
}