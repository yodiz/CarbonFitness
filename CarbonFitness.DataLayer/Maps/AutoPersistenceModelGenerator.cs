using System;
using System.Linq;
using System.Reflection;
using CarbonFitness.Data.Model;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate.FluentNHibernate;

namespace CarbonFitness.DataLayer.Maps {
	public class AutoPersistenceModelGenerator : IAutoPersistenceModelGenerator {
		public AutoPersistenceModel Generate() {

			//todo: add conventions etc.
			var autoPersistanceModel = new AutoPersistenceModel()
				.AddEntityAssembly(Assembly.GetAssembly(typeof(User)))
				.Where(GetAutoMappingFilter)
				.Conventions.Add(
					//PrimaryKey.Name.Is(x=> PrimaryKey.Name+"Id")
				)
				.UseOverridesFromAssemblyOf<UserMap>();

			return autoPersistanceModel;

			//var mappings = AutoPersistenceModel
			//   .MapEntitiesFromAssemblyOf<>()
			//   .ForTypesThatDeriveFrom<User>(map => map.Map(u => u.Username).SetAttribute("unique-key", "IX_Unique_UserName"))
			//   .ForTypesThatDeriveFrom<Ingredient>(map => map.Map(i => i.Name).SetAttribute("unique-key", "IX_Unique_IngredientName"))
			//   .Where(GetAutoMappingFilter)
			//   .WithConvention(GetConventions)
			//   .UseOverridesFromAssemblyOf<AutoPersistenceModelGenerator>();
			//return mappings;
		}

		/// <summary>
		/// Provides a filter for only including types which inherit from the IEntityWithTypedId interface.
		/// This might be considered a little hackish having this magic string in the comparison, but since
		/// the interface is generic, it wouldn't be possible to compare the type directly.
		/// </summary>
		private static bool GetAutoMappingFilter(Type t) {
			return t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>));
		}

		//private static void GetConventions(Convention c)
		//{
		//   c.GetPrimaryKeyNameFromType = type => type.Name + "ID";
		//   c.FindIdentity = type => type.Name == "Id";
		//   // c.GetTableName = type => Inflector.Net.Inflector.Pluralize(type.Name);
		//   c.IsBaseType = IsBaseTypeConvention;
		//   c.GetForeignKeyNameOfParent = type => type.Name + "ID";
		//}

		//private static bool IsBaseTypeConvention(Type arg) {
		//   var derivesFromEntity = arg == typeof(Entity);
		//   var derivesFromEntityWithTypedId = arg.IsGenericType &&
		//      (arg.GetGenericTypeDefinition() == typeof(EntityWithTypedId<>));

		//   return derivesFromEntity || derivesFromEntityWithTypedId;
		//}
	}
}