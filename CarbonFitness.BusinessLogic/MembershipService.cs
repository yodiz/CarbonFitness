using System;
using System.Web.Security;
using CarbonFitness.Model;
using CarbonFitness.Repository;

namespace CarbonFitness {
	public class MembershipService : IMembershipService {
		public MembershipService(IUserRepository userRepository) {
			UserRepository = userRepository;
		}

		protected IUserRepository UserRepository { get; set; }

		public int MinPasswordLength { get { return 4; } }

		public bool ValidateUser(string userName, string password) {
			User user = UserRepository.Get(userName);
			if (user != null && user.Password == password) {
				return true;
			}

			return false;
		}

		//public MembershipCreateStatus CreateUser(string userName, string password, string email) {
		//   throw new NotImplementedException();
		//}

		public bool ChangePassword(string userName, string oldPassword, string newPassword) {
			throw new NotImplementedException();
		}

	}
}