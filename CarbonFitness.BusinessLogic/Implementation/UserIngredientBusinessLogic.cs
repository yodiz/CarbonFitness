using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
	public class UserIngredientBusinessLogic : IUserIngredientBusinessLogic {
		private readonly IIngredientRepository ingredientRepository;
		private readonly IUserIngredientRepository userIngredientRepository;

		public UserIngredientBusinessLogic(IUserIngredientRepository userIngredientRepository, IIngredientRepository ingredientRepository) {
			this.userIngredientRepository = userIngredientRepository;
			this.ingredientRepository = ingredientRepository;
		}

		public UserIngredient AddUserIngredient(User user, string ingredientName, int measure, DateTime dateTime) {
			var userIngredient = new UserIngredient();
			userIngredient.User = user;
			userIngredient.Ingredient = GetExistingIngredient(ingredientName);
			userIngredient.Measure = measure;
			userIngredient.Date = dateTime;
			return userIngredientRepository.SaveOrUpdate(userIngredient);
		}

		public UserIngredient[] GetUserIngredients(User user, DateTime dateTime) {
			if (dateTime < DateTime.Parse(SqlDateTime.MinValue.ToString()) || dateTime > DateTime.Parse(SqlDateTime.MaxValue.ToString())) {
				throw new InvalidDateException();
			}

			var fromdate = DateTime.Parse(dateTime.ToShortDateString());
			var todate = DateTime.Parse(dateTime.AddDays(1).Date.ToShortDateString());

			return userIngredientRepository.GetUserIngredientsByUser(user.Id, fromdate, todate);
		}

		public IHistoryValues GetCalorieHistory(User user) {
			var date = DateTime.Now.Date;
			var userIngredients = userIngredientRepository.GetUserIngredientsByUser(user.Id, date.AddDays(-100), date.AddDays(1));

			var history = GetCalorieSumPerDateFromUserIngredients(userIngredients);

			return new HistoryValues(history);
		}

		private Dictionary<DateTime, double> GetCalorieSumPerDateFromUserIngredients(IEnumerable<UserIngredient> userIngredients) {
			var history = new Dictionary<DateTime, double>();

			foreach (var ingredient in userIngredients) {
				var ingredientDate = ingredient.Date.Date;
				var ingredientCalories = (double) ingredient.Ingredient.EnergyInKcal;

				if (history.ContainsKey(ingredientDate)) {
					history[ingredientDate] += ingredientCalories;
				} else {
					history.Add(ingredientDate, ingredientCalories);
				}
			}
			return history;
		}

		private Ingredient GetExistingIngredient(string ingredientName) {
			var ingredient = ingredientRepository.Get(ingredientName);
			if (ingredient == null) {
				throw new NoIngredientFoundException(ingredientName);
			}
			return ingredient;
		}
	}
}