using System;
using System.Linq;
using CarbonFitness.Data.Model;
using SharpArch.Data.NHibernate;
using NHibernate.Linq;

namespace CarbonFitness.DataLayer.Repository {
    public class NutrientRepository : NHibernateRepositoryWithTypedId<Nutrient, int>, INutrientRepository {
        public Nutrient GetByName(string name) {

            var q = from n in this.Session.Linq<Nutrient>()
                    where n.Name.Equals(name) select n;

            return q.FirstOrDefault();
        }
    }
}