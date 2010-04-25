using CarbonFitness.Data.Model;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CarbonFitness.DataLayer.Maps {
    public class NutrientRecommendationMap : IAutoMappingOverride<NutrientRecommendation> {
        public void Override(AutoMapping<NutrientRecommendation> mapping) {
            mapping.HasMany(x => x.GenderRecommendations).Cascade.All().Inverse();
        }
    }
}