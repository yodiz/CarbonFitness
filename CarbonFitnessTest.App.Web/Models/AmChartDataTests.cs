using System.IO;
using System.Xml.Serialization;
using AutoMapper;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Models {
	[TestFixture]
	public class AmChartDataTests {
		public string testXml = "<chart><series><value xid=\"0\">1949</value><value xid=\"57\">2006</value></series><graphs><graph gid=\"1\"><value xid=\"0\">2.54</value><value xid=\"57\">58.30</value></graph><graph gid=\"2\"><value xid=\"0\">20.21</value><value xid=\"57\">58.30</value></graph></graphs></chart>";

		[Test]
		public void shouldBeAbleToMapHistoryValuesToAmChartData() {
			AutoMappingsBootStrapper.MapHistoryValuesContainerToAmChartData();

			Mapper.AssertConfigurationIsValid();
		}

		[Test]
		public void shouldContainTwoDataPointsAndGraphs() {
			var serializer = new XmlSerializer(typeof(AmChartData));
			var deserializedObject = (AmChartData) serializer.Deserialize(new StringReader(testXml));

			Assert.That(deserializedObject.DataPoints.Length, Is.EqualTo(2));

			Assert.That(deserializedObject.GraphRoot.Graphs.Length, Is.EqualTo(2), "Wrong graph count");
			Assert.That(deserializedObject.GraphRoot.Graphs[1].gid, Is.EqualTo("2"));
		}
	}
}