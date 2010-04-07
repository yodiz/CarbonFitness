using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface INutrientBusinessLogic {
        IEnumerable<Nutrient> GetNutrients();
        void Export();
    }
}