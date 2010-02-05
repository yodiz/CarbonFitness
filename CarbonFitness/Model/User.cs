using SharpArch.Core.DomainModel;

namespace CarbonFitness.Model {
    public class User : Entity {
        public User() {
            
        }
        
        public User(string username) {
            Username = username;
        }

        public virtual string Username { get; set; }
    }
}