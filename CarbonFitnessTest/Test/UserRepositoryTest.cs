
using CarbonFitness.Repository;
using CarbonFitness.Model;
using NUnit.Framework;

namespace CarbonFitnessTest.Test
{
    [TestFixture]
    public class UserRepositoryTest
    {

        [Test]
        public void Create()
        {

            var userRepository = new UserRepository();

            var userId = userRepository.Create(new User("myUser"));

            Assert.AreEqual(1, userId); 
        }


    }
}
