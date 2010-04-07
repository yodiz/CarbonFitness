using CarbonFitness.Data.Model;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public class NutrientRepository : NHibernateRepositoryWithTypedId<Nutrient, int>, INutrientRepository { }
}