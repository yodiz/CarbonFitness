using CarbonFitness.Data.Model;
using FluentNHibernate.AutoMap;
using FluentNHibernate.AutoMap.Alterations;

namespace CarbonFitness.DataLayer.Maps {
    internal class UserMap : IAutoMappingOverride<User> {
        public void Override(AutoMap<User> mapping) {
            mapping.SetAttribute("lazy", "false");
        }
    }
}