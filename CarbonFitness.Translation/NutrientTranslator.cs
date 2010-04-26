using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CarbonFitness.Translation
{
    public interface INutrientTranslator : ITranslationHelper { }

    public class NutrientTranslator : BaseTranslationHelper, INutrientTranslator {
        public NutrientTranslator() : base("CarbonFitness.Translation.Resources.NutrientTranslation", typeof(BaseTranslationHelper).Assembly) { }
    }
}
