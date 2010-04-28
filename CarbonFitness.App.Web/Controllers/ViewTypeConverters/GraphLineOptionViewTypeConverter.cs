using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitness.Translation;

namespace CarbonFitness.App.Web.Controllers.ViewTypeConverters {
    public interface IGraphLineOptionViewTypeConverter : IViewTypeConverter {
        NutrientEntity[] GetNutrientEntitys(string[] graphLineOptionValue);
        bool shouldShowWeight(string[] graphLineOptions);
    }

    public class GraphLineOptionViewTypeConverter : IGraphLineOptionViewTypeConverter {
        private readonly INutrientBusinessLogic nutrientBusinessLogic;
        private readonly INutrientTranslator nutrientTranslator;

        public GraphLineOptionViewTypeConverter(INutrientBusinessLogic nutrientBusinessLogic, INutrientTranslator nutrientTranslator) {
            this.nutrientBusinessLogic = nutrientBusinessLogic;
            this.nutrientTranslator = nutrientTranslator;
        }

        public IEnumerable<SelectListItem> GetViewTypes(User user) {
            var nutrientOptions = new List<SelectListItem>();
            addGraphlineOption("Vikt (kg)", "Weight", nutrientOptions);
            IEnumerable<Nutrient> nutrients = nutrientBusinessLogic.GetNutrients();
            foreach (Nutrient nutrient in nutrients) {
                addGraphlineOption(nutrientTranslator.GetString(nutrient.Name), nutrient.Name, nutrientOptions);
            }
            return nutrientOptions;
        }

        public NutrientEntity[] GetNutrientEntitys(string[] graphLineOptionValue) {
            var nutrientEntities = new List<NutrientEntity>();
            foreach (string s in graphLineOptionValue) {
                try {
                    nutrientEntities.Add((NutrientEntity) Enum.Parse(typeof(NutrientEntity), s));
                } catch (ArgumentException) {} //TODO:Please fix. When calling action the System.String gets added to Nutrient Collection
            }
            return nutrientEntities.ToArray();
        }

        private void addGraphlineOption(string name, string value, ICollection<SelectListItem> nutrientOptions) {
            var option = new SelectListItem {
                Text = name,
                Value = value,
                Selected = false
            };
            nutrientOptions.Add(option);
        }


        public bool shouldShowWeight(string[] graphLineOptions) {
            foreach (string s in graphLineOptions) {
                if (s.Equals("Weight")) {
                    return true;
                }
            }
            return false;
        }
    }
}