using System;
using CarbonFitness.Data.Model;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CarbonFitness.DataLayer.Maps
{
	public class UserMap : IAutoMappingOverride<User>
	{
		public void Override(AutoMapping<User> map)
		{
			map.Map(u => u.Username).UniqueKey("IX_Unique_UserName");
		}
	}
}