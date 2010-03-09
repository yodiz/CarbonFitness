using System;

namespace CarbonFitness.Data.Model {
	public class UserWeight {
		public DateTime Date { get; set; }
		public User User { get; set; }
		public decimal Weight { get; set; }
	}
}