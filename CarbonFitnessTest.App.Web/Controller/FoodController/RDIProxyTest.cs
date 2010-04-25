using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Controllers.RDI;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.FoodController
{
    [TestFixture]
    public class RDIProxyTest
    {
         [Test]
        public void shouldGetRDIOfNutrients() {
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.User).Returns(new User { Username = "myUser" });

            var rdiCalculator = new Mock<IRDICalculator>();
            rdiCalculator.Setup(x => x.GetRDI(It.IsAny<User>(), It.IsAny<DateTime>(), It.Is<NutrientEntity>(y => y == NutrientEntity.IronInmG))).Returns(12M);
            var calcFactoryMock = new Mock<IRDICalculatorFactory>();
            calcFactoryMock.Setup(x => x.GetRDICalculator(It.IsAny<NutrientEntity>())).Returns(rdiCalculator.Object);

            var result = new RDIProxy(calcFactoryMock.Object).getRDI(new User(), DateTime.Now, NutrientEntity.IronInmG);
            
             rdiCalculator.VerifyAll();
             Assert.That(result, Is.EqualTo(12M));
        }

    }
}
