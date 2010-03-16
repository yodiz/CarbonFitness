using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.Data.Model;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CarbonFitness.DataLayer.Maps
{
	public class IngredientMap : IAutoMappingOverride<Ingredient>
	{
		public void Override(AutoMapping<Ingredient> mapping) {
			mapping.Map(x => x.Name).UniqueKey("IX_Unique_IngredientName");
		}
	}
}
