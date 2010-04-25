using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Controllers.RDI {
    public interface IRDIProxy {
        decimal getRDI(User user, DateTime date, NutrientEntity nutrientEntity);
    }
}