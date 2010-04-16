using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class UserProfile : Entity {
        public virtual GenderType Gender{ get; set;}
        public virtual decimal Weight { get; set; }
        public virtual decimal IdealWeight { get; set; }
        public virtual decimal Length { get; set; }
        public virtual User User { get; set; }
    }
}