using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository
{
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