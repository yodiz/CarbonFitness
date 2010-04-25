using CarbonFitness.Data.Model;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CarbonFitness.DataLayer.Maps
{
    public class GenderNutrientRecommendationMap : IAutoMappingOverride<GenderNutrientRecommendation> {
        public void Override(AutoMapping<GenderNutrientRecommendation> mapping) {
            mapping.HasMany(x => x.GenderAgeRecommendations).Cascade.All().Inverse();
        }
    }

}
