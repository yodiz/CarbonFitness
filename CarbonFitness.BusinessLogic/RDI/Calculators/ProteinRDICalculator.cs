using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.RDI.Calculators {
    public interface IProteinRDICalculator : IRDICalculator { }

    public class ProteinRDICalculator : IProteinRDICalculator {
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;

        public ProteinRDICalculator(IUserProfileBusinessLogic userProfileBusinessLogic) {
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        public decimal GetRDI(User user, DateTime date, NutrientEntity nutrientEntity) {
            return (GetUserWeight(user) * 1.4M);
        }

        public decimal GetUserWeight(User user)
        {
            return userProfileBusinessLogic.GetWeight(user);
        }

        public bool DoesSupportNutrient(NutrientEntity nutrientEntity) {
            return nutrientEntity == NutrientEntity.ProteinInG;
        }
    }
}