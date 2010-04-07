﻿using System;
using System.Collections.Generic;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.AppLogic {
    [TestFixture]
    public class InitialDataValuesExportEngineTest {
        [Test]
        public void shouldExportInitialDataValues() {
            var nutrientBusinessLogicMock = new Mock<INutrientBusinessLogic>();
            nutrientBusinessLogicMock.Setup(x => x.Export());

            new InitialDataValuesExportEngine(nutrientBusinessLogicMock.Object).Export();
            nutrientBusinessLogicMock.VerifyAll(); 
        }
    }
}