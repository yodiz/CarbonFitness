using System;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
    [TestFixture]
    public class UserProfileBusinessLogicTest {
        private Mock<User> getUserMock() {
            var expectedUserId = 32;
            var userMock = new Mock<User>();
            userMock.Setup(x => x.Id).Returns(expectedUserId);
            return userMock;
        }
        
        private User User { get { return getUserMock().Object; } }
    
        [Test]
        public void shouldCreateUserProfileIfNotExistingForUser() {
            decimal expectedIdealWeight = 42;

            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(User.Id)).Returns((UserProfile)null);
            userProfileRepositoryMock.Setup(x => x.SaveOrUpdate(It.Is<UserProfile>(y => y.Id == 0)));

            new UserProfileBusinessLogic(userProfileRepositoryMock.Object, new Mock<IGenderTypeBusinessLogic>().Object, new Mock<IActivityLevelTypeBusinessLogic>().Object, null).SaveProfile(User, expectedIdealWeight, 1, 1, 1, "", "");

            userProfileRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldHaveImplementation() {
            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            IUserProfileBusinessLogic userProfileBusinessLogic = new UserProfileBusinessLogic(userProfileRepositoryMock.Object, null, null, null);
            Assert.That(userProfileBusinessLogic, Is.Not.Null);
        }
        
        [Test]
        public void shouldReuseExistingUserProfileForUserIfExisting() {
            decimal expectedIdealWeight = 42;

            var userProfileMock = new Mock<UserProfile>();
            userProfileMock.Setup(x => x.Id).Returns(234);
            var userProfile = userProfileMock.Object;

            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(User.Id)).Returns(userProfile);
            userProfileRepositoryMock.Setup(x => x.SaveOrUpdate(userProfile));

            new UserProfileBusinessLogic(userProfileRepositoryMock.Object, new Mock<IGenderTypeBusinessLogic>().Object, new Mock<IActivityLevelTypeBusinessLogic>().Object, null).SaveProfile(User, expectedIdealWeight, 1, 1, 1, "", "");

            userProfileRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldSaveProfile() {
            const decimal expectedIdealWeight = 64;
            const decimal expectedLength = 1.83M;
            const decimal expectedWeight = 78M;
            const int expectedAge = 23;
            const string expectedGenderToGet = "Man";
            const string expectedActivityLevelToGet = "Medel";
            var expectedGender = new GenderType();
            var expectedActivityLevel = new ActivityLevelType();
            
            var genderTypeBusinessLogicMock = new Mock<IGenderTypeBusinessLogic>();
            genderTypeBusinessLogicMock.Setup(x => x.GetGenderType(expectedGenderToGet)).Returns(expectedGender);

            var activityLevelTypeBusinessLogicMock = new Mock<IActivityLevelTypeBusinessLogic>();
            activityLevelTypeBusinessLogicMock.Setup(x => x.GetActivityLevelType(expectedActivityLevelToGet)).Returns(expectedActivityLevel);

            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.SaveOrUpdate(It.Is<UserProfile>(y => y.Weight == expectedWeight && y.Length == expectedLength && y.IdealWeight == expectedIdealWeight && y.User.Id == User.Id && y.Age == expectedAge && y.Gender == expectedGender && y.ActivityLevel == expectedActivityLevel)));

            new UserProfileBusinessLogic(userProfileRepositoryMock.Object, genderTypeBusinessLogicMock.Object, activityLevelTypeBusinessLogicMock.Object, null).SaveProfile(User, expectedIdealWeight, expectedLength, expectedWeight, expectedAge, expectedGenderToGet, expectedActivityLevelToGet);
            userProfileRepositoryMock.VerifyAll();
            activityLevelTypeBusinessLogicMock.VerifyAll();
        }
        
        [Test]
        public void shouldGetIdealWeight() {
            const decimal expectedIdealWeight = 42M;
            var userProfile = new UserProfile { IdealWeight = expectedIdealWeight, User = User };
            AssertProperty(expectedIdealWeight, User, userProfile, x => x.GetIdealWeight(User));
        }
        
        [Test]
        public void shouldGetLength(){
            const decimal expectedLength = 1.83M;
            var userProfile = new UserProfile { Length = expectedLength, User = User };
            AssertProperty(expectedLength, User, userProfile, x => x.GetLength(User));
        }

        [Test]
        public void shouldGetWeight() {
            const decimal expectedWeight = 83M;
            var userProfile = new UserProfile { Weight = expectedWeight, User = User };
            AssertProperty(expectedWeight, User, userProfile, x => x.GetWeight(User));
        }
        
        [Test]
        public void shouldGetAge() {
            const int expectedAge = 24;
            var userProfile = new UserProfile { Age = expectedAge, User = User };
            AssertProperty(expectedAge, User, userProfile, x => x.GetAge(User));
        }

        [Test]
        public void shouldGetGender() {
            var expectedGender = new GenderType();
            var userProfile = new UserProfile { Gender = expectedGender, User = User };
            AssertProperty(expectedGender, User, userProfile, x => x.GetGender(User));
        }

        [Test]
        public void shouldIfNoGenderExistDefaultMale() {
            GenderType expectedGender = null;
            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            var userProfile = new UserProfile { Gender = expectedGender, User = User };
            userProfileRepositoryMock.Setup(x => x.GetByUserId(User.Id)).Returns(userProfile);

            var genderTypeBusinessLogic = new Mock<IGenderTypeBusinessLogic>();
            genderTypeBusinessLogic.Setup(x => x.GetGenderType(It.IsAny<string>())).Returns(new GenderType {Name = "Man"});

            var result = new UserProfileBusinessLogic(userProfileRepositoryMock.Object, genderTypeBusinessLogic.Object, null, null).GetGender(User);
            Assert.That(result.Name, Is.EqualTo("Man"));
        }

        [Test]
        public void shouldIfNoActivityLevelExistDefaultLow() {
            const string expectedActivityType = "Låg";
            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            var activityLevelTypeBusinessLogicMock = new Mock<IActivityLevelTypeBusinessLogic>();

            userProfileRepositoryMock.Setup(x => x.GetByUserId(It.IsAny<int>())).Returns(new UserProfile());
            activityLevelTypeBusinessLogicMock.Setup(x => x.GetActivityLevelType(expectedActivityType)).Returns(new ActivityLevelType{Name = expectedActivityType});

            var result = new UserProfileBusinessLogic(userProfileRepositoryMock.Object, null, activityLevelTypeBusinessLogicMock.Object, null).GetActivityLevel(User);
            
            activityLevelTypeBusinessLogicMock.VerifyAll();
            Assert.That(result.Name, Is.EqualTo(expectedActivityType));
        }

        [Test]
        public void shouldGetActivityLevel() {
            var expectedActivityLevel = new ActivityLevelType();
            var userProfile = new UserProfile { ActivityLevel = expectedActivityLevel, User = User };
            AssertProperty(expectedActivityLevel, User, userProfile, x => x.GetActivityLevel(User));
        }

        [Test]
        public void shouldGetBMI() {
            const decimal weight = 83M;
            const decimal length = 83M;
            var userProfile = new UserProfile { Weight = weight, Length = length, User = User };

            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(User.Id)).Returns(userProfile);

            var result = new UserProfileBusinessLogic(userProfileRepositoryMock.Object, null, null, null).GetBMI(User);

            Assert.That(result, Is.EqualTo(weight / (length * length)));
        }


        [Test]
        public void shouldGetBMR() {
            const decimal weight = 83M;
            const int age = 24;
            const int height = 174;
            var gender = new GenderType{Name="Man"};
            var activityLevel = new ActivityLevelType { Name = "Medel" };
            var calorieCalculator = new Mock<ICalorieCalculator>();
            
            var userProfile = new UserProfile { Weight = weight, Age = age, Length = height , Gender = gender, ActivityLevel = activityLevel,  User = User };

            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(User.Id)).Returns(userProfile);

            calorieCalculator.Setup(x => x.GetBMR(weight, height, age, gender)).Returns(123);

            var result = new UserProfileBusinessLogic(userProfileRepositoryMock.Object, null, null, calorieCalculator.Object).GetBMR(User);

            Assert.That(result, Is.EqualTo(123));
        }

        [Test]
        public void shouldGetZeroAsBMIIfLenghtIsZero() {
            const decimal weight = 83M;
            var userProfile = new UserProfile { Weight = weight, Length = 0, User = User };

            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(User.Id)).Returns(userProfile);

            var result = new UserProfileBusinessLogic(userProfileRepositoryMock.Object, null, null, null).GetBMI(User);
            Assert.That(result, Is.EqualTo(0));
        }

        private void AssertProperty(object expectedResult, User user, UserProfile resultProfile, Func<UserProfileBusinessLogic, object> propertyToFetch)
        {
           var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(user.Id)).Returns(resultProfile);

            var result = propertyToFetch(new UserProfileBusinessLogic(userProfileRepositoryMock.Object, null, null, null));

            Assert.That(result, Is.EqualTo(expectedResult));
            userProfileRepositoryMock.VerifyAll();
        }
    }
}