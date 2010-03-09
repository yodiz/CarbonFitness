using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CarbonFitness.DataLayer.Maps;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NUnit.NHibernate;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	public abstract class IntegrationBaseTest {
		protected string BaseUrl = "http://localhost:49639";
		protected Browser Browser;

		protected IntegrationBaseTest() {}

		protected IntegrationBaseTest(Browser browser) {
			Browser = browser;
			browser.BringToFront();
			browser.GoTo(Url);
		}

		public abstract string Url { get; }

		[SetUp]
		public virtual void Setup() {
			Browser.GoTo(Url);
		}

		[TestFixtureSetUp]
		public virtual void TestFixtureSetUp() {
			string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
			NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
				new AutoPersistenceModelGenerator().Generate(),
				"../../../CarbonFitness.App.Web/NHibernate.config");
			if (Browser == null) {
				Browser = new IE(Url);
			}
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown() {
			Browser.Dispose();
		}

		public string GetFieldNameOnModel<T>(Expression<Func<T, string>> lambdaExpression) {
			return ExpressionHelper.GetExpressionText(lambdaExpression);
		}

		public string GetFieldNameOnModel<T>(Expression<Func<T, int>> lambdaExpression)
		{
			return ExpressionHelper.GetExpressionText(lambdaExpression);
		}

		public string GetFieldNameOnModel<T>(Expression<Func<T, DateTime>> lambdaExpression)
		{
			return ExpressionHelper.GetExpressionText(lambdaExpression);
		}

		public string GetFieldNameOnModel<T>(Expression<Func<T, decimal>> lambdaExpression)
		{
			return ExpressionHelper.GetExpressionText(lambdaExpression);
		}
	}
}