using System;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Data.NHibernate;

namespace CarbonFitnessTest.Integration.Results {
	public abstract class ResultsTestBase : IntegrationBaseTest {
		protected DateTime Now = DateTime.Now.Date;
		protected int UserId;

		[TestFixtureSetUp]
		public override void TestFixtureSetUp() {
			base.TestFixtureSetUp();
			UserId = new CreateUserTest(Browser).getUniqueUserId();
			new AccountLogOnTest(Browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);

			clearUserIngredients();
			addOneUserIngredient(Now.ToString(), 200);
		}

		protected void addOneUserIngredient(string date, int weightMeasure) {
			var inputFoodTest = new InputFoodTest(Browser);

			inputFoodTest.changeDate(date);
			inputFoodTest.createIngredientIfNotExist("Arne anka");
			inputFoodTest.addUserIngredient("Arne anka", weightMeasure.ToString());
		}

		private void clearUserIngredients() {
			var repository = new UserIngredientRepository();
			var userIngredients = new UserIngredientRepository().GetAll();
			foreach (var userIngredient in userIngredients) {
				repository.Delete(userIngredient);
			}
			NHibernateSession.Current.Flush();
		}
	}
}