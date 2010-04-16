using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model
{
    public class GenderType : Entity {
        public virtual string Name { get; set; }
    }

    //public enum GenderType {
    //    Male,
    //    Female
    //}
}
