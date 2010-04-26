using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.RDI.Calculators {
    public interface IMineralRDICalculator : IRDICalculator { }

    public class MineralRDICalculator : IMineralRDICalculator
    {
        private readonly INutrientRecommendationBusinessLogic nutrientRecommendationBusinessLogic;
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;

        public MineralRDICalculator(INutrientRecommendationBusinessLogic nutrientRecommendationBusinessLogic, IUserProfileBusinessLogic userProfileBusinessLogic) {
            this.nutrientRecommendationBusinessLogic = nutrientRecommendationBusinessLogic;
            this.userProfileBusinessLogic = userProfileBusinessLogic;
        }

        public bool DoesSupportNutrient(NutrientEntity nutrientEntity) {
            return GetNutrientRecommendation(nutrientEntity) != null;
        }

        public decimal GetRDI(User user, DateTime date, NutrientEntity nutrientEntity) {
            return GetNutrientRecommendation(nutrientEntity).GetValue(GetUserGender(user).Name, GetUserAge(user));
        }

        public NutrientRecommendation GetNutrientRecommendation(NutrientEntity entity){
            return nutrientRecommendationBusinessLogic.GetNutrientRecommendation(entity);
        }

        public decimal GetUserWeight(User user) {
            return userProfileBusinessLogic.GetWeight(user);
        }

        public int GetUserAge(User user) {
            return userProfileBusinessLogic.GetAge(user);
        }

        public GenderType GetUserGender(User user) {
            return userProfileBusinessLogic.GetGender(user);
        }
    }
}