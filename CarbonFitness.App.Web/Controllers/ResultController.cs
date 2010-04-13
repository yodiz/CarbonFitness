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
            //model.CalorieHistoryList = userIngredientBusinessLogic.GetNutrientHistory(userContext.User);
            model.IdealWeight = userProfileBusinessLogic.GetIdealWeight(userContext.User);
            return View(model);
        }

        //For WatiN testing purposes the result needs to go inside an html result.    
        [Authorize]
        public ActionResult ShowXmlInsideHtml(params string[] nutrients) {
            return new ContentResult {
                Content = "<html><head></head><body>" + getAmChartAsXml(GetNutrientEntitys(nutrients)) + "</body></html>",
                ContentEncoding = Encoding.UTF8,
                ContentType = "text/html"
            };
        }

        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ShowXml(params string[] nutrients) {
            return new ContentResult {
                Content = getAmChartAsXml(GetNutrientEntitys(nutrients)),
                ContentType = "text/xml"
            };
        }

        private AmChartData getAmChartData(IEnumerable<NutrientEntity> nutrients) {
            Graph graph = GetGraph(nutrients);

            var amChartData = new AmChartData();

            Mapper.Map(graph, amChartData);
            return amChartData;
        }

        public Graph GetGraph(IEnumerable<NutrientEntity> nutrients) {
            var lines = new List<ILine>();
            foreach (var nutrient in nutrients) {
                ILine line = userIngredientBusinessLogic.GetNutrientHistory(nutrient, userContext.User);
                lines.Add(line);
            }
            return graphBuilder.GetGraph(lines.ToArray());
        }

        private string getAmChartAsXml(IEnumerable<NutrientEntity> nutrients) {
            AmChartData amChartData = getAmChartData(nutrients);
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

        public NutrientEntity[] GetNutrientEntitys(string[] nutrients) {
            var nutrientEntities = new List<NutrientEntity>();
            foreach (var s in nutrients) {
                try {
                    nutrientEntities.Add((NutrientEntity)Enum.Parse(typeof(NutrientEntity), s));
                } catch(ArgumentException ) {} //TODO:Please fix. When calling action the System.String gets added to Nutrient Collection
            }
            return nutrientEntities.ToArray();
        }
    }
}