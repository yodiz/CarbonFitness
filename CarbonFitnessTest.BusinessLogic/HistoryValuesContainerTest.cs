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
			Line line = new Line(new Dictionary<DateTime, decimal> {{ DateTime.Now, 123  }} );
			Graph graph = new Graph(line);
		

			Assert.That(graph.Labels, Is.Not.Null);
			Assert.That(graph.Labels.Length, Is.EqualTo(line.Count()));

			Assert.That(graph.LinesContainer.Lines.Count(), Is.EqualTo(1));
			Assert.That(graph.LinesContainer.Lines[0].Count(), Is.EqualTo(line.Count()));
		}

	}
}
