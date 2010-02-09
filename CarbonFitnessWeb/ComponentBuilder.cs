using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Builder;
using CarbonFitness;
using CarbonFitness.Repository;

namespace CarbonFitnessWeb
{
	 public class ComponentBuilder
	 {
		  private static IContainer _current;

		  public static IContainer Current
		  {
				get
				{
					 if (_current == null) {
						  var builder = new ContainerBuilder();
						  builder.Register<MembershipService>().As<IMembershipService>();
						  builder.Register<UserRepository>().As<IUserRepository>();

					 	_current = builder.Build();	
					 }

					return _current;
				}
		  }
	 }
}
