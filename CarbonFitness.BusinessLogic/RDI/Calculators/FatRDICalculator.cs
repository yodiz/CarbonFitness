using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.RDI.Calculators {
    public interface IFatRDICalculator : IRDICalculator { }

    public class FatRDICalculator : IFatRDICalculator {
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;

        public FatRDICalculator(IUserProfileBusinessLogic userProfileBusinessLogic) {
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        public decimal GetRDI(User user, DateTime date, NutrientEntity nutrientEntity) {
            var dailyCalorieNeed = userProfileBusinessLogic.GetDailyCalorieNeed(user);

            //9 kcal/gram ger kolhydrater i energi. 15 - 30 % av vårt energiintag bör bestå av fett 
            return (dailyCalorieNeed * 0.3M) / 9;
        }

        public bool DoesSupportNutrient(NutrientEntity nutrientEntity) {
            return nutrientEntity == NutrientEntity.FatInG;
        }
    }
}