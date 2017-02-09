using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVNAnalyser.Core;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class ZingChartTest
    {   
        /*This test should check that when a zing chart class is created, the class has the correct JSON definition */
        [TestMethod]
        public void ZingChart_ShouldCreateEmptyGraphsetJSONObject()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Element> elements = new List<ZingChart.Element>();
            ZingChart.Element element = new ZingChart.Element();

            elements.Add(element);

            zingchart.graphset = elements;

            ZingChart.Graphset graphset = zingchart;

            var result = javaScriptSerializer.Serialize(graphset);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[]}]}";

            Assert.AreEqual(expected, result);
        }

        /*This test should add 1 pie chart to the zingchart with the title set*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenChartCreatedWithTitleSet()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Element> elements = new List<ZingChart.Element>();
            ZingChart.Element element = new ZingChart.Element();

            ZingChart.Title title = new ZingChart.Title("RBF.CLA");

            element.title = title;

            elements.Add(element);

            zingchart.graphset = elements;

            ZingChart.Graphset graphset = zingchart;

            var result = javaScriptSerializer.Serialize(graphset);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":""RBF.CLA"",""fontsize"":10},""series"":[]}]}";

            Assert.AreEqual(expected, result);
        }
        /*This test should add 1 pie chart to the zingchart with 1 pie slice set*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenChartCreatedWith1PieSlice()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Element> elements = new List<ZingChart.Element>();
            ZingChart.Element element = new ZingChart.Element();

            ZingChart.Series piechart = new ZingChart.Series(50, "Developer 1");

            element.series.Add(piechart);

            elements.Add(element);

            zingchart.graphset = elements;

            ZingChart.Graphset graphset = zingchart;

            var result = javaScriptSerializer.Serialize(graphset);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[50],""text"":""Developer 1""}]}]}";

            Assert.AreEqual(expected, result);
        }
        /*This test should add 1 pie chart to the zingchart with 2 pie slices set*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenChartCreatedWith2PieSlices()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Element> elements = new List<ZingChart.Element>();
            ZingChart.Element element = new ZingChart.Element();

            ZingChart.Series pieslice1 = new ZingChart.Series(30, "Developer 1");
            ZingChart.Series pieslice2 = new ZingChart.Series(70, "Developer 2");

            element.series.Add(pieslice1);
            element.series.Add(pieslice2);

            elements.Add(element);

            zingchart.graphset = elements;

            ZingChart.Graphset graphset = zingchart;

            var result = javaScriptSerializer.Serialize(graphset);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]}]}";

            Assert.AreEqual(expected, result);
        }
        /*This test should add 2 pie charts to the zingchart with 2 pie slices each*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_When2ChartsCreatedWith2PieSlicesEach()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Element> elements = new List<ZingChart.Element>();
            ZingChart.Element chart1 = new ZingChart.Element();
            ZingChart.Element chart2 = new ZingChart.Element();

            ZingChart.Series pieslice1 = new ZingChart.Series(30, "Developer 1");
            ZingChart.Series pieslice2 = new ZingChart.Series(70, "Developer 2");

            chart1.series.Add(pieslice1);
            chart1.series.Add(pieslice2);

            chart2.series.Add(pieslice1);
            chart2.series.Add(pieslice2);

            elements.Add(chart1);
            elements.Add(chart2);

            zingchart.graphset = elements;

            ZingChart.Graphset graphset = zingchart;

            var result = javaScriptSerializer.Serialize(graphset);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]},{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]}]}";

            Assert.AreEqual(expected, result);
        }
        /*Should create a zingchart object via method and return expected json*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenMethodCalled()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart zingchart = new ZingChart();

            string title = "";

            zingchart.addPieChart(title);

            var result = javaScriptSerializer.Serialize(zingchart);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[]}]}";

            Assert.AreEqual(expected, result);
        }
        /*Should create a zingchart object via method and return expected json*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenMethodCalled2()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart zingchart = new ZingChart();
            string title = "RBF.CLA";            

            zingchart.addPieChart(title);
            zingchart.addPieChartSlice(30, "Developer 1");
            zingchart.addPieChartSlice(70, "Developer 2");

            var result = javaScriptSerializer.Serialize(zingchart);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]},{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]}]}";

            Assert.AreEqual(expected, result);
        }
    }
}
