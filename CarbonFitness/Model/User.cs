using SharpArch.Core.DomainModel;

namespace CarbonFitness.Model {
	public class User : Entity {
		public User() {
		}

		public User(string username) {
			Username = username;
		}

		public User(string username, string password) : this(username) {
			Password = password;
		}

		public virtual string Password { get; set; }

		public virtual string Username { get; set; }
	}
}