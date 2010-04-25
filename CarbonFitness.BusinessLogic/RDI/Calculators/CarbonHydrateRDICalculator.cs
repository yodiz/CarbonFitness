using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.RDI.Calculators {
    public interface ICarbonHydrateRDICalculator : IRDICalculator { }

    public class CarbonHydrateRDICalculator : ICarbonHydrateRDICalculator {
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;

        public CarbonHydrateRDICalculator(IUserProfileBusinessLogic userProfileBusinessLogic) {
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        public decimal GetRDI(User user, DateTime date, NutrientEntity nutrientEntity) {
            var dailyCalorieNeed = userProfileBusinessLogic.GetDailyCalorieNeed(user);
            //4 kcal/gram ger kolhydrater i energi. 55 - 60 % av vårt energiintag bör bestå av kolhydrater 
            return (dailyCalorieNeed * 0.55M) / 4;
        }

        public bool DoesSupportNutrient(NutrientEntity nutrientEntity) {
            return nutrientEntity == NutrientEntity.CarbonHydrateInG;
        }
    }
}