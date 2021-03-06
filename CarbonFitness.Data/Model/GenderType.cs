﻿using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class GenderType : Entity, ILookUpTypes {
        public virtual string Name { get; set; }
        public virtual int GenderBMRFactor { get; set; }

        public virtual int GetId() {
            return Id;
        }
    }
}
