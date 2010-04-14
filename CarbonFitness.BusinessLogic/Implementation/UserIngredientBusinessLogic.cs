using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
	public class UserIngredientBusinessLogic : IUserIngredientBusinessLogic {
		private readonly IIngredientRepository ingredientRepository;
		private readonly IUserIngredientRepository userIngredientRepository;
        private readonly INutrientRepository nutrientRepository;

		public UserIngredientBusinessLogic(IUserIngredientRepository userIngredientRepository, IIngredientRepository ingredientRepository, INutrientRepository nutrientRepository) {
			this.userIngredientRepository = userIngredientRepository;
			this.ingredientRepository = ingredientRepository;
            this.nutrientRepository = nutrientRepository;
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

		public ILine GetNutrientHistory(NutrientEntity nutrientEntity,User user) {
			var date = DateTime.Now.Date;
			var userIngredients = userIngredientRepository.GetUserIngredientsByUser(user.Id, date.AddDays(-100), date.AddDays(1));

			var valueSumPerDateFromUserIngredients = GetValueSumPerDateFromUserIngredients(userIngredients, x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(nutrientEntity)) );

		    var nutrientId = nutrientRepository.GetByName(nutrientEntity.ToString()).Id;
            return new Line(nutrientId, valueSumPerDateFromUserIngredients);
		}
      
		private Dictionary<DateTime, decimal> GetValueSumPerDateFromUserIngredients(IEnumerable<UserIngredient> userIngredients, Func<Ingredient, decimal> valueToSum) {
			var history = new Dictionary<DateTime, decimal>();

			foreach (var userIngredient in userIngredients) {
				var ingredientDate = userIngredient.Date.Date;
				var ingredientAmount = userIngredient.GetActualCalorieCount(valueToSum);

				if (history.ContainsKey(ingredientDate)) {
					history[ingredientDate] += ingredientAmount;
				} else {
					history.Add(ingredientDate, ingredientAmount);
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