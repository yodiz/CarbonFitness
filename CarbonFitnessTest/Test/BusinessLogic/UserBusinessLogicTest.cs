

using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework.SyntaxHelpers;

namespace CarbonFitnessTest.Test.BusinessLogic
{
    [TestFixture]
    public class UserBusinessLogicTest
    {


        [Test]
        public void shouldCallSaveOrUpdateOnDataLayer() {
            var mockFactory = new MockFactory(MockBehavior.Strict);
            var userRepositoryMock = mockFactory.Create<IUserRepository>();
            userRepositoryMock.Setup(x => x.SaveOrUpdate(It.IsAny<User>())).Returns(new User("myUser"));

            var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object);
            userBusinessLogic.SaveOrUpdate(new User("myUserToSave"));

            userRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldCallGetOnDataLayer() {
            var mockFactory = new MockFactory(MockBehavior.Strict);
            var userRepositoryMock = mockFactory.Create<IUserRepository>();
            userRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new User());
            
            var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object);
            userBusinessLogic.Get(2);
            userRepositoryMock.VerifyAll();
        }
    }
}
