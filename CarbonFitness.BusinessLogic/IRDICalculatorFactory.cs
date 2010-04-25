using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IRDICalculatorFactory {
        IRDICalculator GetRDICalculator(NutrientEntity entity);
        void AddRDICalculator(IRDICalculator calculator);
    }
}