using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface INutrientSum {
        DateTime Date { get; set; }
        IDictionary<NutrientEntity, decimal> NutrientValues { get; set; }
    }

    public class NutrientSum : INutrientSum {
        public DateTime Date { get; set; }
        public IDictionary<NutrientEntity, decimal> NutrientValues { get; set; }
    }
}