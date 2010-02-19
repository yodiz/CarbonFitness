using System;
using System.Collections.Generic;
using System.Linq;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Moq;
using NUnit.Framework;
using User = CarbonFitness.Data.Model.User;

namespace CarbonFitnessTest.DataLayer.ObjectBased
{
	[TestFixture]
	public class ObjectDbContextTest
	{
		[Test]
		public void shouldHaveGenericAsQueryable()
		{
			var context = new ObjectDbContext(null);
			IQueryable<User> obj = context.AsQueryable<User>();
			Assert.That(obj, Is.InstanceOf(typeof (IQueryable)));
		}

		[Test]
		public void ShouldHaveAsQueryableWithContentFromDb4ODatabase()
		{
			var queryableMock = new Mock<IDb4oLinqQueryable<User>>();

			var db4OClientMock = new Mock<IObjectContainer>();
			db4OClientMock.Setup(x => x.Cast<User>());

			var context = new ObjectDbContext(db4OClientMock.Object);
			var result = context.AsQueryable<User>();

			db4OClientMock.VerifyAll();
			Assert.That(result, Is.EqualTo(queryableMock.Object));
		}
	}

	public class ObjectDbContext
	{
		private readonly IObjectContainer objectClient;

		public ObjectDbContext(IObjectContainer objectClient)
		{
			this.objectClient = objectClient;
		}

		public IQueryable<T> AsQueryable<T>()
		{
			return objectClient.AsQueryable<T>();
		}
	}
}