using System;
using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
	public class UserWeight : Entity {
		public virtual DateTime Date { get; set; }
		public virtual User User { get; set; }
		public virtual decimal Weight { get; set; }
	}
}