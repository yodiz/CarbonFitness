using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Serialization;
using AutoMapper;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.UnitHistory;

namespace CarbonFitness.App.Web.Controllers {
    public class ResultController : Controller {
        private readonly IUserContext userContext;
        private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;

        public ResultController(IUserProfileBusinessLogic userProfileBusinessLogic, IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext) {
            this.userIngredientBusinessLogic = userIngredientBusinessLogic;
            this.userContext = userContext;
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        public ActionResult Show() {
            var model = new ResultModel();
            //model.CalorieHistoryList = userIngredientBusinessLogic.GetCalorieHistory(userContext.User);
            model.IdealWeight = userProfileBusinessLogic.GetIdealWeight(userContext.User);
            return View(model);
        }

        //For watIN testing purposes the result needs to go inside an html result.
        public ActionResult ShowXmlInsideHtml() {
            return new ContentResult {
                Content = "<html><head></head><body>" + getAmChartAsXml() + "</body></html>",
                ContentEncoding = Encoding.UTF8,
                ContentType = "text/html"
            };
        }

		 [OutputCache(NoStore=true, Duration=0, VaryByParam="*")]
        public ActionResult ShowXml()
        {
            return new ContentResult {
                Content = getAmChartAsXml(),
                ContentType = "text/xml"
            };
        }

        private AmChartData GetAmChartData() {
            HistoryValuesContainer historyValueContainer = userIngredientBusinessLogic.GetCalorieHistory(userContext.User);

            var amChartData = new AmChartData();

            Mapper.Map(historyValueContainer, amChartData);
            return amChartData;
        }

        private string getAmChartAsXml() {
            AmChartData amChartData = GetAmChartData();

            return serializeAmChartData(amChartData);
        }

        private string serializeAmChartData(AmChartData amChartData) {
            var serializer = new XmlSerializer(typeof(AmChartData));
            var writer = new StringWriter();
            serializer.Serialize(writer, amChartData);
            return writer.ToString();
        }

       
    }
}