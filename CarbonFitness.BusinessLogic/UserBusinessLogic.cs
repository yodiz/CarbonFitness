using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic {
    public class UserBusinessLogic : IUserBusinessLogic {
        private readonly IUserRepository userRepository;

        public UserBusinessLogic(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public User SaveOrUpdate(User user) {
            return userRepository.SaveOrUpdate(user);
        }

        public User Get(int id) {
            return userRepository.Get(id);
        }

    	public User Get(string userName)
    	{
    		return userRepository.Get(userName);
    	}
    }
}