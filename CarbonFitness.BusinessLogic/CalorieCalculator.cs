using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface ICalorieCalculator {
        decimal GetBMR(decimal weight, decimal height, int age, GenderType gender);
        decimal GetDailyCalorieNeed(decimal weight, decimal height, int age, GenderType gender, ActivityLevelType activityLevel);
    }

    public class CalorieCalculator : ICalorieCalculator {
        public decimal GetEnergyFactor(ActivityLevelType activityLevelType) {
            return activityLevelType.EnergyFactor;
        }

        public decimal GetAgeFactor(int age) {
            return (decimal) 4.92 * age;
        }

        public decimal GetWeightFactor(decimal weight) {
            return (decimal) 9.99 * weight;
        }

        public decimal GetHeightFactor(decimal height) {
            return (decimal) 6.25 * height;
        }

        //http://en.wikipedia.org/wiki/Basal_metabolic_rate
        public decimal GetBMR(decimal weight, decimal height, int age, GenderType gender) {
            return GetWeightFactor(weight) + GetHeightFactor(height) - GetAgeFactor(age) + gender.GenderBMRFactor;
        }

        //http://www.bmi-calculator.net/bmr-calculator/harris-benedict-equation/
        public decimal GetDailyCalorieNeed(decimal weight, decimal height, int age, GenderType gender, ActivityLevelType activityLevel) {
            return GetBMR(weight, height, age, gender) * activityLevel.EnergyFactor;
        }
    }

    
}