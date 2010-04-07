using System.Collections.Generic;
using System.Linq;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class NutrientBusinessLogic : INutrientBusinessLogic {
        private readonly INutrientRepository nutrientRepository;

        public NutrientBusinessLogic(INutrientRepository nutrientRepository) {
            this.nutrientRepository = nutrientRepository;
        }

        public IEnumerable<Nutrient> GetNutrients() {
            return nutrientRepository.GetAll();
        }

        public void Export() {
            var nutrients = GetNutrients();
            if (nutrients == null || nutrients.Count() == 0) {
                nutrientRepository.Save(new Nutrient {Name = "EnergyInKcal"});
                nutrientRepository.Save(new Nutrient {Name = "FibresInG"});
                nutrientRepository.Save(new Nutrient {Name = "CarbonHydrateInG"});
            }
        }
    }
}