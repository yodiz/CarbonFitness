using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IRDICalculator {
        decimal GetRDI(User user, DateTime date, NutrientEntity nutrientEntity);
        bool DoesSupportNutrient(NutrientEntity nutrientEntity);
    }
}