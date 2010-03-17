using System;
using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class UserProfile : Entity {
        public virtual decimal IdealWeight { get; set; }
        public virtual User User { get; set; }
    }
}