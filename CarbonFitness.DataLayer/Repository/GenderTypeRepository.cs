using System;
using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public class GenderTypeRepository : NHibernateRepositoryWithTypedId<GenderType, int>, IGenderTypeRepository {
        public GenderType GetByName(string name) {
            var q = from n in Session.Linq<GenderType>()
                    where n.Name.Equals(name)
                    select n;

            return q.FirstOrDefault();
        }
    }
}