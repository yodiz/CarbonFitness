﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CarbonFitnessTest.Web.Controller.IngredientController
{
    [TestFixture]
    public class SearchTest
    {
        [Test]
        public void shouldSearchIngredients()
        {
            Ingredient[] ingredients;
            var ingredientBusinessLogicMock = new Mock<IIngredientBusinessLogic>(MockBehavior.Strict);
            ingredientBusinessLogicMock.Setup(x => x.Search(It.IsAny<String>())).Returns(new[]
                                                                                             {
                                                                                                 new Ingredient(),
                                                                                                 new Ingredient(),
                                                                                                 new Ingredient()
                                                                                             });

            var ingredientController = new CarbonFitnessWeb.Controllers.IngredientController(ingredientBusinessLogicMock.Object);

            var actionResult = (ViewResult)ingredientController.Search("abb");
            ingredients = (Ingredient[])actionResult.ViewData.Model;

            Assert.That(ingredients.Count(), Is.GreaterThan(2));
        }
    }
}

