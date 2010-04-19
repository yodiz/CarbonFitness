using System.Collections.Generic;
using System.Linq;
using CarbonFitness.App.Web.Controllers.ViewTypeConverters;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ProfileController.ViewTypeConverter {
    [TestFixture]
    public class GenderViewTypeConverterTest {
        [Test]
        public void shouldGetStoredGenderForLoggedinUserAsSelectListItem() {
            var expectedGender = new GenderType { Name = "selected" };

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetGender(It.IsAny<User>())).Returns(expectedGender);

            var genderBusinessLogicMock = new Mock<IGenderTypeBusinessLogic>();
            genderBusinessLogicMock.Setup(x => x.GetGenderTypes()).Returns(new List<GenderType> { new GenderType { Name = "selected" }, new GenderType { Name = "notSelected" } });

            var genderViewTypes = new GenderViewTypeConverter(userProfileBusinessLogicMock.Object, genderBusinessLogicMock.Object).GetViewTypes(new User());

            var selectedGender = (from gen in genderViewTypes where gen.Selected select gen).FirstOrDefault();

            userProfileBusinessLogicMock.VerifyAll();
            Assert.That(selectedGender.Text, Is.SameAs(expectedGender.Name));
        }
    }
}
