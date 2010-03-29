using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CarbonFitness.BusinessLogic;
using Moq;
using MvcContrib.ActionResults;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web;
using CarbonFitness.Data.Model;

namespace CarbonFitnessTest.Web.Controller.ResultController
{
    [TestFixture]
    public class ShowWeightPrognosisXmlTest
    {

        [Test]
        public void shouldReturnWeightPrognosisXml()
        {
            var user = new User("Min user");
            var userProfileBusinessLogicMock =  new Mock<IUserProfileBusinessLogic>(MockBehavior.Strict);
            var userProfileBusinessLogic = userProfileBusinessLogicMock.Object;

            var userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>(MockBehavior.Strict);
            //userIngredientBusinessLogicMock.Setup(x=> x.GetCalorieHistory())
            var userIngredientBusinessLogic = userIngredientBusinessLogicMock.Object;

            var userContextMock = new Mock<IUserContext>(MockBehavior.Strict);
            userContextMock.Setup(X => X.User).Returns(user);
            var userContext = userContextMock.Object;

            var graphBuilderMock = new Mock<IGraphBuilder>(MockBehavior.Strict);
            var graphBuilder = graphBuilderMock.Object;


            var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic, userIngredientBusinessLogic, userContext, graphBuilder);

            XmlResult result = (XmlResult)resultController.ShowWeightPrognosisXml();
            Assert.That(result.ObjectToSerialize, Is.AssignableTo(typeof(AmChartData)));

            var amChartData = (AmChartData)result.ObjectToSerialize;
            var dataPoints = amChartData.DataPoints;
            Assert.That(dataPoints, Is.Not.Null);
            var firstPoint = dataPoints.First();
            var firstPointDate = DateTime.Parse(firstPoint.Value);

            Assert.That(firstPointDate, Is.GreaterThan(DateTime.Today));
        }

        //[Test]
        //public void shouldReturnWeightPrognosisForSpecifiedDate()
        //{

        //}


    }
}
