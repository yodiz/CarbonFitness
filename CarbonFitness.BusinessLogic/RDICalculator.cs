using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IRDICalculator {
        decimal GetRDI(User user, NutrientEntity nutrientEntity);
    }

    public class RDICalculator : IRDICalculator {
        private readonly INutrientBusinessLogic nutrientBusinessLogic;
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;

        public RDICalculator(INutrientBusinessLogic nutrientBusinessLogic, IUserProfileBusinessLogic userProfileBusinessLogic) {
            this.nutrientBusinessLogic = nutrientBusinessLogic;
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        public decimal GetRDI(User user, NutrientEntity nutrientEntity) {
            return GetUserWeight(user) * GetNutrientRDI(nutrientEntity);
        }

        public decimal GetNutrientRDI(NutrientEntity entity) {
            return nutrientBusinessLogic.GetRDI(entity);
        }

        public decimal GetUserWeight(User user) {
            return userProfileBusinessLogic.GetWeight(user);
        }
    }
}