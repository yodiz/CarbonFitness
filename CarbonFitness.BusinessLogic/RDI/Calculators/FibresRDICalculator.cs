using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.RDI.Calculators {
    public interface IFibresRDICalculator : IRDICalculator { }

    public class FibresRDICalculator : IFibresRDICalculator {
        public decimal GetRDI(User user, DateTime date, NutrientEntity nutrientEntity) {
            return 30;
        }

        public bool DoesSupportNutrient(NutrientEntity nutrientEntity) {
            return nutrientEntity == NutrientEntity.FibresInG;
        }
    }
}