namespace CarbonFitness.BusinessLogic {
	public interface IMembershipBusinessLogic {
		int MinPasswordLength { get; }

		bool ValidateUser(string userName, string password);
		//MembershipCreateStatus CreateUser(string userName, string password, string email);
		bool ChangePassword(string userName, string oldPassword, string newPassword);
	}
}