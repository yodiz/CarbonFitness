using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Maps;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	public abstract class IntegrationBaseTest {
        protected string BaseUrl = "http://localhost/carbonfitness";

		protected Browser Browser;
		protected const string MVCFileExtension = ".aspx";

		protected IntegrationBaseTest() {}

		protected IntegrationBaseTest(Browser browser) {
			Browser = browser;
			browser.BringToFront();
			browser.GoTo(Url);
		}

		public abstract string Url { get; }
		protected string getUrl(string controller, string action ) {
			return BaseUrl + "/" + controller + MVCFileExtension + "/" + action;
		}

		[SetUp]
		public virtual void Setup() {
			Browser.GoTo(Url);
		}

		[TestFixtureSetUp]
		public virtual void TestFixtureSetUp() {
			//NHibernateInitializer.Instance().InitializeNHibernateOnce(() => {

			//});

			var assembly = Assembly.GetAssembly(typeof(User));
			var mappingAssembly = assembly.CodeBase.ToLower();

			NHibernateSession.Init(new SimpleSessionStorage(), new[] {mappingAssembly},
				new AutoPersistenceModelGenerator().Generate(),
				"../../../CarbonFitness.App.Web/bin/NHibernate.config");

			//string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();

			if (Browser == null) {
				Browser = new IE(Url);
			}
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown() {
			NHibernateSession.Reset();
			Browser.Dispose();
		}

        public string GetFieldNameOnModel<TModel, TReturnType>(Expression<Func<TModel, TReturnType>> lambdaExpression)
        {
            return ExpressionHelper.GetExpressionText(lambdaExpression);
        }
		public string GetFieldNameOnModel<T>(Expression<Func<T, string>> lambdaExpression) {
			return ExpressionHelper.GetExpressionText(lambdaExpression);
		}

		public string GetFieldNameOnModel<T>(Expression<Func<T, int>> lambdaExpression) {
			return ExpressionHelper.GetExpressionText(lambdaExpression);
		}

		public string GetFieldNameOnModel<T>(Expression<Func<T, DateTime>> lambdaExpression) {
			return ExpressionHelper.GetExpressionText(lambdaExpression);
		}

		public string GetFieldNameOnModel<T>(Expression<Func<T, decimal>> lambdaExpression) {
			return ExpressionHelper.GetExpressionText(lambdaExpression);
		}

		protected void ReloadPage(string findLinkByText) {
			Browser.Link(Find.ByText(x => x.Contains(findLinkByText))).Click();
		}
	}
}