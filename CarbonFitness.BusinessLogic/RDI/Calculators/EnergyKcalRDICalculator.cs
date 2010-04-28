using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.RDI.Calculators {
    public interface IEnergyKcalRDICalculator : IRDICalculator { }

    public class EnergyKcalRDICalculator : IEnergyKcalRDICalculator {
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;

        public EnergyKcalRDICalculator(IUserProfileBusinessLogic userProfileBusinessLogic) {
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        public decimal GetRDI(User user, DateTime date, NutrientEntity nutrientEntity) {
            return userProfileBusinessLogic.GetDailyCalorieNeed(user);
        }

        public bool DoesSupportNutrient(NutrientEntity nutrientEntity) {
            return nutrientEntity == NutrientEntity.EnergyInKcal;
        }
    }
}