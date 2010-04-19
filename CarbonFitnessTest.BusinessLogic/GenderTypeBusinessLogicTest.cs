using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
    [TestFixture]
    public class GenderTypeBusinessLogicTest {
        [Test]
        public void shouldGetGenderTypes() {
            var genderTypeRepositoryMock = new Mock<IGenderTypeRepository>();
            var expectedGenderTypes = new List<GenderType>();
            genderTypeRepositoryMock.Setup(x => x.GetAll()).Returns(expectedGenderTypes);
            var result = new GenderTypeBusinessLogic(genderTypeRepositoryMock.Object).GetGenderTypes();
            Assert.That(result, Is.SameAs(expectedGenderTypes));
        }

        [Test]
        public void shouldExportInitialValues() {
            var genderTypeRepositoryMock = new Mock<IGenderTypeRepository>();
            genderTypeRepositoryMock.Setup(x => x.Save(It.Is<GenderType>(y => y.Name == "Man" && y.GenderBMRFactor == 5)));
            genderTypeRepositoryMock.Setup(x => x.Save(It.Is<GenderType>(y => y.Name == "Kvinna" && y.GenderBMRFactor == -161)));
            new GenderTypeBusinessLogic(genderTypeRepositoryMock.Object).ExportInitialValues();
            genderTypeRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldGetGenderType() {
            var genderTypeRepositoryMock = new Mock<IGenderTypeRepository>();
            var expectedGenderType = new GenderType();
            genderTypeRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(expectedGenderType);
            var result = new GenderTypeBusinessLogic(genderTypeRepositoryMock.Object).GetGenderType("Man");
            Assert.That(result, Is.SameAs(expectedGenderType));
        }
    }
}
