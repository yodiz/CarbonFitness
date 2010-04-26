using Autofac;

namespace CarbonFitness.Translation {
    public class TranslationModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<NutrientTranslator>().As<INutrientTranslator>();
        }
    }
}