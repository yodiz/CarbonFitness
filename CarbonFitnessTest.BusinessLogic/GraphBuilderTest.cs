using System;
using System.Collections.Generic;
using System.Linq;
using CarbonFitness.BusinessLogic.UnitHistory;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class GraphBuilderTest {
		private ILine[] setupLines(DateTime firstDate, DateTime lastDate) {
			var shortLine = new Line(new Dictionary<DateTime, decimal> {{firstDate.AddDays(2), 2}, {lastDate.AddDays(-2), 3}});
			var longLine = new Line(new Dictionary<DateTime, decimal> {{firstDate, 4}, {lastDate, 5}});
			var noLine = new Line();

			return new[] {shortLine, longLine, noLine};
		}

		[Test]
		public void shouldCreateGraph() {
			var firstDate = DateTime.Now.AddDays(-24);
			var lastDate = DateTime.Now;

			var lines = setupLines(firstDate, lastDate);

			var graphBuilder = new GraphBuilder();

			var graph = graphBuilder.GetGraph(lines);

			Assert.That(graph.Labels.First().Value, Is.EqualTo(firstDate.ToShortDateString()));
			Assert.That(graph.Labels.Last().Value, Is.EqualTo(lastDate.ToShortDateString()));
			Assert.That(graph.LinesContainer.Lines.Length, Is.EqualTo(lines.Length - 1));
		}

		[Test]
		public void shouldRemoveEmptyLine() {
			var lines = setupLines(DateTime.Now.Date.AddDays(-100), DateTime.Now.Date);

			ILine[] result = new GraphBuilder().RemoveEmptyLines(lines);

			Assert.That(result.Length, Is.EqualTo(2));
		}
	}
}