﻿using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using CarbonFitness.App.Web.Controllers.RDI;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web.Controllers
{
	[HandleError]
	public class FoodController : Controller {
		private readonly IUserContext userContext;
		private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;
        private readonly IRDIProxy rdiProxy;

        public FoodController(IUserIngredientBusinessLogic userIngredientBusinessLogic, IRDIProxy rdiProxy, IUserContext userContext) {
			this.userIngredientBusinessLogic = userIngredientBusinessLogic;
            this.rdiProxy = rdiProxy;
            this.userContext = userContext;
		}

        //First time when coming to the "kost" page
        [Transaction]
        [Authorize]
        public ActionResult Input() {
            var date = DateTime.Now.Date;
            if(TempData["Date"] != null) {
                date = DateTime.Parse(TempData["Date"].ToString());
            }
            return View(GetInputFoodModel(date));
        }

	    private InputFoodModel GetInputFoodModel(DateTime date) {
	        return new InputFoodModel {
	            UserIngredients = getUserIngredients(date),
	            Date = date,
                SumOfEnergy = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.EnergyInKcal, date),
                SumOfProtein = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.ProteinInG, date),
	            SumOfFat = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.FatInG, date),
	            SumOfFiber = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.FibresInG, date),
	            SumOfCarbonHydrates = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.CarbonHydrateInG, date),
	            SumOfIron = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.IronInmG, date),

                RDIOfEnergy = rdiProxy.getRDI(userContext.User, date, NutrientEntity.EnergyInKcal),
                RDIOfProtein = rdiProxy.getRDI(userContext.User, date, NutrientEntity.ProteinInG),
                RDIOfFat = rdiProxy.getRDI(userContext.User, date, NutrientEntity.FatInG),
                RDIOfFiber = rdiProxy.getRDI(userContext.User, date, NutrientEntity.FibresInG),
                RDIOfCarbonHydrates = rdiProxy.getRDI(userContext.User, date, NutrientEntity.CarbonHydrateInG),
                RDIOfIron = rdiProxy.getRDI(userContext.User, date, NutrientEntity.IronInmG),
	        };
	    }

	    
	    [HttpPost]
		[Transaction]
		[Authorize]
		//When pressing submit, trying to add an ingredient to user
		public ActionResult Input(InputFoodModel model) {
			if (!string.IsNullOrEmpty(model.Ingredient) && model.Measure > 0) {
				AddUserIngredient(model);
			}

	        return View(GetInputFoodModel(model.Date));
		}

        /*[AcceptVerbs(HttpVerbs.Delete)] */
        [Transaction]
        [Authorize]
        public ActionResult DeleteUserIngredient( int userIngredientId, DateTime date) {
            userIngredientBusinessLogic.DeleteUserIngredient(userContext.User, userIngredientId, date);
            TempData["Date"] = date;
            return RedirectToAction("Input", new RouteValueDictionary(GetInputFoodModel(date)));
        }  

		private UserIngredient[] getUserIngredients(DateTime date) {
			try {
				return userIngredientBusinessLogic.GetUserIngredients(userContext.User, date);
			} catch (InvalidDateException) {
				this.AddModelError<InputFoodModel>(x => x.Date, FoodConstant.InvalidDateErrorMessage);
			}
			return null;
		}

        private void AddUserIngredient(InputFoodModel model) {
            try {
                userIngredientBusinessLogic.AddUserIngredient(userContext.User, model.Ingredient, model.Measure, model.Date);
                RemoveFoodInputValues(model);
            } catch (NoIngredientFoundException e) {
                this.AddModelError<InputFoodModel>(x => x.Ingredient, FoodConstant.NoIngredientFoundMessage + e.IngredientName);
            }
        }

        private void RemoveFoodInputValues(InputFoodModel model) {
            model.Ingredient = "";
            model.Measure = 0;

            ModelState.Remove("Ingredient");
            ModelState.Remove("Measure");
        }

        public static string GetPropertyNameFromModel<T>(Controller controller, Expression<Func<T, string>> namedPropertyToGet) {
            return ExpressionHelper.GetExpressionText(namedPropertyToGet);
        }
	}
}