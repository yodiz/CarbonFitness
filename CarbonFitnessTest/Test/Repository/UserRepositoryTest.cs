
using CarbonFitness.Maps;
using CarbonFitness.Repository;
using CarbonFitness.Model;
using Castle.Windsor;
using NUnit.Framework;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.Test.Repository {
    [TestFixture]
    public class UserRepositoryTest : RepositoryTestsBase
    {

        protected override void LoadTestData() {
            
        }

        //public void InitServiceLocator()
        //{
        //    IWindsorContainer container = new WindsorContainer();
        //    container.AddComponent("duplicateChecker", typeof(IEntityDuplicateChecker), typeof(DuplicateCheckerStub));
        //    container.AddComponent("validator", typeof(IValidator), typeof(Validator));
        //    ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        //}

        [Test]
        public void shouldHaveCreateMethod() {
            var userRepository = new UserRepository();
            var user = userRepository.SaveOrUpdate(new User("myUser"));

            Assert.AreEqual(1, user.Id); 
        }
    }
}