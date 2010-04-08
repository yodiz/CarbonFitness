using System;

namespace CarbonFitness.BusinessLogic.Exceptions {
    public class UserAlreadyExistException : Exception {

        public string Username { get; private set; }

        public UserAlreadyExistException(string username) {
            this.Username = username;
        }

        public override string Message
        {
            get
            {
                return base.Message + " Username: " + Username;
            }
        }
    }
}