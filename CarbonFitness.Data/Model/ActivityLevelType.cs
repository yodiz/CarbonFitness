using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class ActivityLevelType : Entity, ILookUpTypes {
        public virtual decimal EnergyFactor { get; set; }
        public virtual string Name { get; set; }
        public virtual int GetId() {
            return Id;
        }
    }
}