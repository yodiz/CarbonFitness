using System;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
    [TestFixture]
    public class RDICalculatorTest {
        [Test]
        public void shouldGetNutrientRDI() {
            const decimal expectedValue = 1.2M;
            var nutrientBusinessLogicMock = new Mock<INutrientBusinessLogic>();
            nutrientBusinessLogicMock.Setup(x => x.GetRDI(NutrientEntity.ProteinInG)).Returns(expectedValue);
            decimal result = new RDICalculator(nutrientBusinessLogicMock.Object, null).GetNutrientRDI(NutrientEntity.ProteinInG);
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void shouldGetUserKilo() {
            const decimal expectedValue = 75M;
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetWeight(It.IsAny<User>())).Returns(expectedValue);

            decimal result = new RDICalculator(null, userProfileBusinessLogicMock.Object).GetUserWeight(new User());
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void shouldGetRDI() {
            const decimal proteinRDI = 1.2M;
            var nutrientBusinessLogicMock = new Mock<INutrientBusinessLogic>();
            nutrientBusinessLogicMock.Setup(x => x.GetRDI(NutrientEntity.ProteinInG)).Returns(proteinRDI);

            const decimal weight = 75M;
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetWeight(It.IsAny<User>())).Returns(weight);

            decimal result = new RDICalculator(nutrientBusinessLogicMock.Object, userProfileBusinessLogicMock.Object).GetRDI(new User(), NutrientEntity.ProteinInG);

            Assert.That(result, Is.EqualTo(weight * proteinRDI));
        }
    }
}