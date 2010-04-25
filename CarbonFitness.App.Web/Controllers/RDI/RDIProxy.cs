using System;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Controllers.RDI {
    public class RDIProxy : IRDIProxy{
        private readonly IRDICalculatorFactory rdiCalculatorFactory;

        public RDIProxy(IRDICalculatorFactory rdiCalculatorFactory) {
            this.rdiCalculatorFactory = rdiCalculatorFactory;
        }

        public decimal getRDI(User user, DateTime date, NutrientEntity nutrientEntity) {
            return rdiCalculatorFactory.GetRDICalculator(nutrientEntity).GetRDI(user, date, nutrientEntity);
        }
    }
}