using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class UserIngredientBusinessLogic : IUserIngredientBusinessLogic {
        private readonly IIngredientRepository ingredientRepository;
        private readonly INutrientRepository nutrientRepository;
        private readonly IUserIngredientRepository userIngredientRepository;

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
            userIngredient.Date = dateTime.AddSeconds(1); // Otherwise it will show up on both today and yesterday
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

        public ILine GetNutrientHistory(NutrientEntity nutrientEntity, User user) {
            var userIngredients = Get100DaysUserIngredients(user);

            var valueSumPerDateFromUserIngredients = GetValueSumPerDateFromUserIngredients(userIngredients, x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(nutrientEntity)));

            int nutrientId = nutrientRepository.GetByName(nutrientEntity.ToString()).Id;
            return new Line(nutrientId, valueSumPerDateFromUserIngredients);
        }

        public decimal GetNutrientSumForDate(User user, NutrientEntity entity, DateTime date) {
            var userIngredients = GetUserIngredients(user, date);
            decimal value;
            GetValueSumPerDateFromUserIngredients(userIngredients, x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(entity))).TryGetValue(date.Date, out value);
            return value;
        }

        public IEnumerable<INutrientSum> GetNutrientSumList(IEnumerable<NutrientEntity> nutrients, User user) {
            var userIngredients = Get100DaysUserIngredients(user);

            var dates = getDatesfromuserIngredient(userIngredients);
            var result = new List<NutrientSum>();
            foreach (var date in dates.ToArray()) {
                var nutrientSum = new NutrientSum();
                nutrientSum.Date = date;
                nutrientSum.NutrientValues = new Dictionary<NutrientEntity, decimal>();
                foreach (var nutrient in nutrients) {
                    var nutrientSumValue = getNutrientIngredientSumForDate(date, userIngredients, x => x.GetNutrientIngredientDisplayValue(x.GetNutrient(nutrient)));
                    nutrientSum.NutrientValues.Add(new KeyValuePair<NutrientEntity, decimal>(nutrient, nutrientSumValue));
                }
                result.Add(nutrientSum);
            }
            return result.ToArray();
        }

        private UserIngredient[] Get100DaysUserIngredients(User user) {
            var now = DateTime.Now.Date;
            return userIngredientRepository.GetUserIngredientsByUser(user.Id, now.AddDays(-100), now.AddDays(1));
        }

        public INutrientAverage GetNutrientAverage(IEnumerable<NutrientEntity> nutrientEntities, User user) {
            var result = new NutrientAverage();
            result.NutrientValues = new Dictionary<NutrientEntity, decimal>();

            var nutrientSums = GetNutrientSumList(nutrientEntities, user);
            if(nutrientSums.Count() == 0) {
                return result;
            }

            foreach (var nutrientEntity in nutrientEntities) {
                var nutrientSum = 0M;
                foreach (var sum in nutrientSums) {
                    nutrientSum += sum.NutrientValues[nutrientEntity];
                }
                var average = nutrientSum / nutrientSums.Count();
                result.NutrientValues.Add(nutrientEntity, average);
            }
            return result;
        }

        public void DeleteUserIngredient(User user, int userIngredientId, DateTime date)
        {
            userIngredientRepository.Delete(userIngredientRepository.Get(userIngredientId));
        }

        public IEnumerable<DateTime> getDatesfromuserIngredient(UserIngredient[] userIngredients) {
            var result = new List<DateTime>();
            foreach (var userIngredient in userIngredients) {
                var date = userIngredient.Date.Date;
                if (!result.Contains(date)) {
                    result.Add(date);
                }
            }
            return result;
        }

        public decimal getNutrientIngredientSumForDate(DateTime date, IEnumerable<UserIngredient> userIngredients, Func<Ingredient, decimal> valueToSum) {
            decimal sum = 0;
            var userIngredientsInDate = from u in userIngredients where u.Date.Date == date select u;
            foreach (var userIngredient in userIngredientsInDate) {
                sum += userIngredient.GetActualCalorieCount(valueToSum);
            }
            return sum;
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