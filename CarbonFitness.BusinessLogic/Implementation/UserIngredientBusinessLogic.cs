using System;
using System.Data.SqlTypes;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation
{
    public class UserIngredientBusinessLogic : IUserIngredientBusinessLogic
    {
        private readonly IUserIngredientRepository userIngredientRepository;
        private readonly IIngredientRepository ingredientRepository;

        public UserIngredientBusinessLogic(IUserIngredientRepository userIngredientRepository, IIngredientRepository ingredientRepository)
        {
            this.userIngredientRepository = userIngredientRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public UserIngredient AddUserIngredient(User user, string ingredientName, int measure, DateTime dateTime)
        {
            var userIngredient = new UserIngredient();
            userIngredient.User = user;
            userIngredient.Ingredient = getExistingIngredient(ingredientName);
            userIngredient.Measure = measure;
            userIngredient.Date = dateTime;
            return userIngredientRepository.SaveOrUpdate(userIngredient);
        }

        private Ingredient getExistingIngredient(string ingredientName) {
            var ingredient = ingredientRepository.Get(ingredientName);
            if(ingredient == null) {
                throw new NoIngredientFoundException(ingredientName);
            }
            return ingredient;
        }

        public UserIngredient[] GetUserIngredients(User user, DateTime dateTime) {
            if (dateTime < DateTime.Parse(SqlDateTime.MinValue.ToString()) || dateTime > DateTime.Parse(SqlDateTime.MaxValue.ToString())) {
                throw new InvalidDateException();
            }

            var fromdate = DateTime.Parse(dateTime.ToShortDateString());
            var todate = DateTime.Parse(dateTime.AddDays(1).Date.ToShortDateString());

            return userIngredientRepository.GetUserIngredientsFromUserId(user.Id, fromdate, todate);
        }
    }
}