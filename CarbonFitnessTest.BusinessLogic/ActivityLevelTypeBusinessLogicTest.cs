using System.Collections.Generic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
    [TestFixture]
    public class ActivityLevelTypeBusinessLogicTest {
        [Test]
        public void shouldGetActivityLevelTypes() {
            var activityLevelTypeRepositoryMock = new Mock<IActivityLevelTypeRepository>();
            var expectedActivityLevelTypes = new List<ActivityLevelType>();
            activityLevelTypeRepositoryMock.Setup(x => x.GetAll()).Returns(expectedActivityLevelTypes);
            var result = new ActivityLevelTypeBusinessLogic(activityLevelTypeRepositoryMock.Object).GetActivityLevelTypes();
            Assert.That(result, Is.SameAs(expectedActivityLevelTypes));
        }

        [Test]
        public void shouldExportInitialValues() {
            var activityLevelTypeRepositoryMock = new Mock<IActivityLevelTypeRepository>();
            activityLevelTypeRepositoryMock.Setup(x => x.Save(It.Is<ActivityLevelType>(y => y.Name == "Mycket låg" && y.EnergyFactor == 1.2M)));
            activityLevelTypeRepositoryMock.Setup(x => x.Save(It.Is<ActivityLevelType>(y => y.Name == "Låg" && y.EnergyFactor == 1.375M)));
            activityLevelTypeRepositoryMock.Setup(x => x.Save(It.Is<ActivityLevelType>(y => y.Name == "Medel" && y.EnergyFactor == 1.55M)));
            activityLevelTypeRepositoryMock.Setup(x => x.Save(It.Is<ActivityLevelType>(y => y.Name == "Hög" && y.EnergyFactor == 1.725M)));
            activityLevelTypeRepositoryMock.Setup(x => x.Save(It.Is<ActivityLevelType>(y => y.Name == "Mycket hög" && y.EnergyFactor == 1.9M)));
            new ActivityLevelTypeBusinessLogic(activityLevelTypeRepositoryMock.Object).ExportInitialValues();
            activityLevelTypeRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldGetActivityLevelTypeFromName(){
            var activityLevelTypeRepositoryMock = new Mock<IActivityLevelTypeRepository>();
            var expectedActivityLevel = new ActivityLevelType();
            var expectedLevelToGet = "Medel";
            activityLevelTypeRepositoryMock.Setup(x => x.GetByName(It.Is<string>(y => y == expectedLevelToGet))).Returns(expectedActivityLevel);
            var result = new ActivityLevelTypeBusinessLogic(activityLevelTypeRepositoryMock.Object).GetActivityLevelType(expectedLevelToGet);
            Assert.That(result, Is.SameAs(expectedActivityLevel));
        }
    }
}
