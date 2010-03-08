using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class MembershipBusinessLogic : IMembershipBusinessLogic {
        public MembershipBusinessLogic(IUserRepository userRepository) {
            UserRepository = userRepository;
        }

        protected IUserRepository UserRepository { get; set; }

        public int MinPasswordLength { get { return 4; } }

        public bool ValidateUser(string userName, string password) {
            var user = UserRepository.Get(userName);
            return user != null && user.Password == password;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }
    }
}