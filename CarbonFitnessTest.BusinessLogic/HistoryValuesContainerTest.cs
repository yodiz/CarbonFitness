using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.BusinessLogic.UnitHistory;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
	[TestFixture]
	public class HistoryValuesContainerTest
	{

		[Test]
		public void shouldInitializeLabelsAndAddHistoryValuesWhenAddingFirstHisoryValue() {
			HistoryValues historyValues = new HistoryValues(new Dictionary<DateTime, decimal> {{ DateTime.Now, 123  }} );
			HistoryValuesContainer historyValuesContainer = new HistoryValuesContainer(historyValues);
		

			Assert.That(historyValuesContainer.labels, Is.Not.Null);
			Assert.That(historyValuesContainer.labels.Length, Is.EqualTo(historyValues.Count()));

			Assert.That(historyValuesContainer.unnecessaryContainer.HistoryValueses.Count(), Is.EqualTo(1));
			Assert.That(historyValuesContainer.unnecessaryContainer.HistoryValueses[0].Count(), Is.EqualTo(historyValues.Count()));
		}

	}
}
