using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.RDI.Importers {
    public interface IIronRDIImporter : INutrientRDIImporter { }

    public class IronRDIImporter : BaseRDIImporter, IIronRDIImporter {
        public IronRDIImporter(INutrientBusinessLogic nutrientBusinessLogic, IGenderTypeBusinessLogic genderTypeBusinessLogic) : base(nutrientBusinessLogic, genderTypeBusinessLogic) {
            WomanRecommendations.Add(createAgeRecommendation(-1, 6, 8));
            WomanRecommendations.Add(createAgeRecommendation(6, 9, 9));

            WomanRecommendations.Add(createAgeRecommendation(10, 13, 11));
            WomanRecommendations.Add(createAgeRecommendation(14, 17, 15));
            WomanRecommendations.Add(createAgeRecommendation(18, 30, 15));
            WomanRecommendations.Add(createAgeRecommendation(31, 60, 15));
            WomanRecommendations.Add(createAgeRecommendation(61, 74, 9));
            WomanRecommendations.Add(createAgeRecommendation(75, 200, 9));

            ManRecommendations.Add(createAgeRecommendation(-1, 6, 8));
            ManRecommendations.Add(createAgeRecommendation(6, 9, 9));
            ManRecommendations.Add(createAgeRecommendation(-1, 10, 8));

            ManRecommendations.Add(createAgeRecommendation(10, 17, 11));
            ManRecommendations.Add(createAgeRecommendation(18, 200, 9));
        }

        protected override NutrientEntity nutrientEntity {
            get { return NutrientEntity.IronInmG;  }
        }
    }
}