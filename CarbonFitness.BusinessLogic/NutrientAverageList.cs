using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface INutrientAverage {
        IDictionary<NutrientEntity, decimal> NutrientValues { get; set; }
    }

    public class NutrientAverage : INutrientAverage {
        public IDictionary<NutrientEntity, decimal> NutrientValues { get; set; }
    }
}