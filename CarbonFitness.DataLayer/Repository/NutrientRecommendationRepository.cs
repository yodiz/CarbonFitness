using System;
using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public class NutrientRecommendationRepository : NHibernateRepositoryWithTypedId<NutrientRecommendation,int> ,INutrientRecommendationRepository{
        public override NutrientRecommendation Get(int Id) {
            throw new NotImplementedException("Use get by nutrientId");
        }

        //public NutrientRecommendation GetByNutrientId(int nutrientId) {
        //    return (from n in Session.Linq<NutrientRecommendation>()
        //                where n.Nutrient.Id == nutrientId
        //                 select n).FirstOrDefault();
        //}

        public NutrientRecommendation GetByNutrient(Nutrient nutrient) {
            return (from n in Session.Linq<NutrientRecommendation>()
                    where n.Nutrient == nutrient
                    select n).FirstOrDefault();
        }
    }
}