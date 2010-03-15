using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
	public class User : Entity {
		public User() {
		    Profile = new UserProfile();
		}

		public User(string username) {
			Username = username;
		}

		public User(string username, string password) : this(username) {
			Password = password;
		}

		public virtual string Password { get; set; }

		public virtual string Username { get; set; }

        public virtual UserProfile Profile { get; set; }
	}

    public class UserProfile {
        public decimal IdealWeight { get; set; }
    }
}