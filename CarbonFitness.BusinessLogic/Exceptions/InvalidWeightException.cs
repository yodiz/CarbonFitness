using System;

namespace CarbonFitness.BusinessLogic.Exceptions {
	public class InvalidWeightException :Exception {
		public InvalidWeightException(string message) : base(message) {}
	}
}