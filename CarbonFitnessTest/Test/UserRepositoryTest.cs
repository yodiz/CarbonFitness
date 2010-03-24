using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarbonFitness.Repository;
using CarbonFitness.Model;

namespace CarbonFitnessTest.Test
{
    [TestClass]
    public class UserRepositoryTest
    {

        [TestMethod]
        public void Create()
        {

            UserRepository userRepository = new UserRepository();

            var userId = userRepository.Create(new User("myUser"));

            Assert.AreEqual(1, userId); 

        }
    }
}
