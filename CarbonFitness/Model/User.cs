using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarbonFitness.Model
{
    public class User
    {
        public string Username { get; set; }

        public User(string username)
        {
            this.Username = username;
        }


    }
}
