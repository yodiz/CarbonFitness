using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
	public class UserBusinessLogic : IUserBusinessLogic {
		private readonly IUserRepository userRepository;
	    private readonly IUserProfileRepository userProfileRepository;

	    public UserBusinessLogic(IUserRepository userRepository, IUserProfileRepository userProfileRepository) {
		    this.userRepository = userRepository;
		    this.userProfileRepository = userProfileRepository;
		}

	    public User Create(User user) {

	        var existingUser = userRepository.Get(user.Username);
            if (existingUser != null) {
                throw new UserAlreadyExistException(user.Username);
            }

            var savedUser = userRepository.Save(user);
	        userProfileRepository.Save(new UserProfile {User = savedUser});
	        return savedUser;
		}

		public User Get(int id) {
			return userRepository.Get(id);
		}

		public User Get(string userName) {
			return userRepository.Get(userName);
		}
	}
}