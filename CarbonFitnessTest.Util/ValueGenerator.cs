using System;

namespace CarbonFitnessTest.Util {
	public static class ValueGenerator {
		public static int getRandomInteger() {
			return getRandomInteger(100);
		}

		public static int getRandomInteger(int max) {
			return Math.Abs(1 + (Guid.NewGuid().GetHashCode()%max));
		}

		public static DateTime getRandomDate() {
			return DateTime.Now.Date.AddDays(-getRandomInteger(1000));
		}
	}
}