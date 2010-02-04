using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.Model;

namespace CarbonFitness.Repository
{
    public class UserRepository : IUserRepository {

        public UserRepository()
        {

        }

        public int Create(User user)
        {

            return 1;
        }

        public User Get(int userId)
        {
            return null;
        }

        public User Get(string userName) {
            

            return null;
        }

    }
}
