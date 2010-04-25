using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.RDI.Calculators {
    public class RDICalculatorFactory : IRDICalculatorFactory {
        private readonly IList<IRDICalculator> calculators = new List<IRDICalculator>();
        public IRDICalculator GetRDICalculator(NutrientEntity nutrientEntity) {
            foreach (var calculator in calculators) {
                if( calculator.DoesSupportNutrient(nutrientEntity)) {
                    return calculator;
                }
            }
            throw new NotSupportedException("RDI Calculator not supported for nutrient: " + nutrientEntity);
        }

        public void AddRDICalculator(IRDICalculator calculator) {
            calculators.Add(calculator);
        }

        //private static RDICalculatorFactory instance = null;
        //public static RDICalculatorFactory GetInstance() {
        //    if(instance == null) {
        //        instance = new RDICalculatorFactory();
        //    }
        //    return instance;
        //}
    }
}