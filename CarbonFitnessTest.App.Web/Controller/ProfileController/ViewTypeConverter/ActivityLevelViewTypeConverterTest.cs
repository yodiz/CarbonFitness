using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.App.Web.Controllers.ViewTypeConverters;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ProfileController.ViewTypeConverter
{
    [TestFixture]
    public class ActivityLevelViewTypeConverterTest {
        [Test]
        public void shouldShowStoredActivityLevelForLoggedinUser() {
            var expectedActivityLevel = new ActivityLevelType { Name = "selected" };

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetActivityLevel(It.IsAny<User>())).Returns(expectedActivityLevel);

            var activityLevelBusinessLogicMock = new Mock<IActivityLevelTypeBusinessLogic>();
            activityLevelBusinessLogicMock.Setup(x => x.GetActivityLevelTypes()).Returns(new List<ActivityLevelType> { new ActivityLevelType { Name = "selected" }, new ActivityLevelType { Name = "notSelected" } });

            var activityLevelViewTypes = new ActivityLevelViewTypeConverter(userProfileBusinessLogicMock.Object, activityLevelBusinessLogicMock.Object).GetViewTypes(new User());

            var selectedActivityLevel = (from activityLevel in activityLevelViewTypes where activityLevel.Selected select activityLevel).FirstOrDefault();

            userProfileBusinessLogicMock.VerifyAll();
            Assert.That(selectedActivityLevel.Text, Is.SameAs(expectedActivityLevel.Name));
        }
    }
}
