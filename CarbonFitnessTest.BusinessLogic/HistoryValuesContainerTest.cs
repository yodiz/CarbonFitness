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
			HistoryValuePoints historyValuePoints = new HistoryValuePoints(new Dictionary<DateTime, decimal> {{ DateTime.Now, 123  }} );
			HistoryValuesContainer historyValuesContainer = new HistoryValuesContainer(historyValuePoints);
		

			Assert.That(historyValuesContainer.labels, Is.Not.Null);
			Assert.That(historyValuesContainer.labels.Length, Is.EqualTo(historyValuePoints.Count()));

			Assert.That(historyValuesContainer.unnecessaryContainer.HistoryValuesCollection.Count(), Is.EqualTo(1));
			Assert.That(historyValuesContainer.unnecessaryContainer.HistoryValuesCollection[0].Count(), Is.EqualTo(historyValuePoints.Count()));
		}

	}
}
