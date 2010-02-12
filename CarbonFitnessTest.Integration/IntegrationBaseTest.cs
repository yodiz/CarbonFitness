using CarbonFitness.DataLayer.Maps;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NUnit.NHibernate;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace CarbonFitnessTest.Integration {
	public abstract class IntegrationBaseTest {
		protected Browser browser;
		protected string baseUrl = "http://localhost:49639";
		public abstract string Url { get; }

		protected IntegrationBaseTest() {
		}

		protected IntegrationBaseTest(Browser browser) {
			this.browser = browser;
			browser.BringToFront();
			browser.GoTo(Url);
		}
		
		[SetUp]
		public void Setup() {
			browser.GoTo(Url);
		}

		[TestFixtureSetUp]
		public virtual void TestFixtureSetUp() {
			string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
			NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
			                       new AutoPersistenceModelGenerator().Generate(),
			                       "../../../CarbonFitnessWeb/NHibernate.config");
			if (browser == null) {
				browser = new IE(Url);
			}
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown() {
			browser.Dispose();
		}
	}
}