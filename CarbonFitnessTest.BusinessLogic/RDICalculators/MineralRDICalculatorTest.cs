using System;
using System.Collections.Generic;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.RDI.Calculators;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic.RDICalculators {
    [TestFixture]
    public class MineralRDICalculatorTest {
        [Test]
        public void shouldGetNutrientRDI() {
            const decimal expectedIronForMan12Years = 11;

            var expectedValue = createRecommendation(expectedIronForMan12Years);
            
            var nutrientRecommendationBusinessLogicMock = new Mock<INutrientRecommendationBusinessLogic>();
            nutrientRecommendationBusinessLogicMock.Setup(x => x.GetNutrientRecommendation(NutrientEntity.IronInmG)).Returns(expectedValue);
            NutrientRecommendation result = new MineralRDICalculator(nutrientRecommendationBusinessLogicMock.Object, null).GetNutrientRecommendation(NutrientEntity.IronInmG);
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        private NutrientRecommendation createRecommendation(decimal expectedIronForMan12Years) {
            return new NutrientRecommendation {
                Nutrient = new Nutrient(),
                GenderRecommendations = new List<GenderNutrientRecommendation> {
                    new GenderNutrientRecommendation{
                        GenderType = new GenderType{Name = "Man"}, 
                        GenderAgeRecommendations = new List<GenderAgeNutrientRecommendation> {
                            new GenderAgeNutrientRecommendation{
                                StartAgeInterval = 10,
                                EndAgeInterval = 35,
                                Value = expectedIronForMan12Years
                            }
                        }
                    }
                }
            };
        }

        [Test]
        public void shouldGetUserWeight() {
            const decimal expectedValue = 75M;
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetWeight(It.IsAny<User>())).Returns(expectedValue);

            decimal result = new MineralRDICalculator(null, userProfileBusinessLogicMock.Object).GetUserWeight(new User());
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void shouldGetUserAge() {
            const int expectedValue = 32;
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetAge(It.IsAny<User>())).Returns(expectedValue);

            int result = new MineralRDICalculator(null, userProfileBusinessLogicMock.Object).GetUserAge(new User());
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void shouldGetUserGender() {
            var expectedValue = new GenderType();
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetGender(It.IsAny<User>())).Returns(expectedValue);

            GenderType result = new MineralRDICalculator(null, userProfileBusinessLogicMock.Object).GetUserGender(new User());
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void shouldGetRDI() {
            const decimal proteinRDI = 1.2M;
            var nutrientRecommendationBusinessLogicMock = new Mock<INutrientRecommendationBusinessLogic>();
            nutrientRecommendationBusinessLogicMock.Setup(x => x.GetNutrientRecommendation(NutrientEntity.ProteinInG)).Returns(createRecommendation(proteinRDI));

            const decimal weight = 75M;
            const int age = 32;
            var gender = new GenderType{Name = "Man"};
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetWeight(It.IsAny<User>())).Returns(weight);
            userProfileBusinessLogicMock.Setup(x => x.GetAge(It.IsAny<User>())).Returns(age);
            userProfileBusinessLogicMock.Setup(x => x.GetGender(It.IsAny<User>())).Returns(gender);

            decimal result = new MineralRDICalculator(nutrientRecommendationBusinessLogicMock.Object, userProfileBusinessLogicMock.Object).GetRDI(new User(), DateTime.Now, NutrientEntity.ProteinInG);

            Assert.That(result, Is.EqualTo(proteinRDI));
        }

        [Test]
        public void shouldSupportMineralsOnly() {
            var nutrientRecommendationBusinessLogicMock = new Mock<INutrientRecommendationBusinessLogic>();
            nutrientRecommendationBusinessLogicMock.Setup(x => x.GetNutrientRecommendation(It.Is<NutrientEntity>(y => y == NutrientEntity.CarbonHydrateInG))).Returns(null as NutrientRecommendation);
            nutrientRecommendationBusinessLogicMock.Setup(x => x.GetNutrientRecommendation(It.Is<NutrientEntity>(y => y == NutrientEntity.ZincInmG))).Returns(createRecommendation(12));

            var calculator = new MineralRDICalculator(nutrientRecommendationBusinessLogicMock.Object, null);
            Assert.That(calculator.DoesSupportNutrient(NutrientEntity.CarbonHydrateInG), Is.False);
            Assert.That(calculator.DoesSupportNutrient(NutrientEntity.ZincInmG));
        }
    }
}