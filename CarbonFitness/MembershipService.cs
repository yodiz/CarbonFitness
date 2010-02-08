
using System.Web.Security;

namespace CarbonFitness
{
    public class MembershipService : IMembershipService
    {
        public int MinPasswordLength {
            get { throw new System.NotImplementedException(); } }

        public bool ValidateUser(string userName, string password) {
            return true;
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email) {
            throw new System.NotImplementedException();
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword) {
            throw new System.NotImplementedException();
        }
    }
}
