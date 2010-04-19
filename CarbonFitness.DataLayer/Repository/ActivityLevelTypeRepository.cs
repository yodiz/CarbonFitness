using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public class ActivityLevelTypeRepository : NHibernateRepositoryWithTypedId<ActivityLevelType, int>, IActivityLevelTypeRepository {
        public ActivityLevelType GetByName(string name) {
            return (from n in Session.Linq<ActivityLevelType>()
            where n.Name.Equals(name)
            select n).FirstOrDefault();
        }
    }
}