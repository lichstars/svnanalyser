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
            List<ZingChart.Chart> elements = new List<ZingChart.Chart>();
            ZingChart.Chart element = new ZingChart.Chart();

            elements.Add(element);

            zingchart.graphset = elements;

            var result = javaScriptSerializer.Serialize(zingchart);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[]}]}";

            Assert.AreEqual(expected, result);
        }

        /*This test should add 1 pie chart to the zingchart with the title set*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenChartCreatedWithTitleSet()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Chart> elements = new List<ZingChart.Chart>();
            ZingChart.Chart element = new ZingChart.Chart("RBF.CLA");
            
            elements.Add(element);

            zingchart.graphset = elements;
            
            var result = javaScriptSerializer.Serialize(zingchart);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":""RBF.CLA"",""fontsize"":10},""series"":[]}]}";

            Assert.AreEqual(expected, result);
        }
        /*This test should add 1 pie chart to the zingchart with 1 pie slice set*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenChartCreatedWith1PieSlice()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Chart> elements = new List<ZingChart.Chart>();
            ZingChart.Chart element = new ZingChart.Chart();

            ZingChart.Series piechart = new ZingChart.Series(50, "Developer 1");

            element.series.Add(piechart);

            elements.Add(element);

            zingchart.graphset = elements;

            var result = javaScriptSerializer.Serialize(zingchart);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[50],""text"":""Developer 1""}]}]}";

            Assert.AreEqual(expected, result);
        }
        /*This test should add 1 pie chart to the zingchart with 2 pie slices set*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenChartCreatedWith2PieSlices()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Chart> elements = new List<ZingChart.Chart>();
            ZingChart.Chart element = new ZingChart.Chart();

            ZingChart.Series pieslice1 = new ZingChart.Series(30, "Developer 1");
            ZingChart.Series pieslice2 = new ZingChart.Series(70, "Developer 2");

            element.series.Add(pieslice1);
            element.series.Add(pieslice2);

            elements.Add(element);

            zingchart.graphset = elements;

            var result = javaScriptSerializer.Serialize(zingchart);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]}]}";

            Assert.AreEqual(expected, result);
        }
        /*This test should add 2 pie charts to the zingchart with 2 pie slices each*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_When2ChartsCreatedWith2PieSlicesEach()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart.Graphset zingchart = new ZingChart.Graphset();
            List<ZingChart.Chart> elements = new List<ZingChart.Chart>();
            ZingChart.Chart chart1 = new ZingChart.Chart();
            ZingChart.Chart chart2 = new ZingChart.Chart();

            ZingChart.Series pieslice1 = new ZingChart.Series(30, "Developer 1");
            ZingChart.Series pieslice2 = new ZingChart.Series(70, "Developer 2");

            chart1.series.Add(pieslice1);
            chart1.series.Add(pieslice2);

            chart2.series.Add(pieslice1);
            chart2.series.Add(pieslice2);

            elements.Add(chart1);
            elements.Add(chart2);

            zingchart.graphset = elements;

            var result = javaScriptSerializer.Serialize(zingchart);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]},{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]}]}";

            Assert.AreEqual(expected, result);
        }
        /*Should create a zingchart object via method and return expected json*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenPieChartAdded()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart zingchart = new ZingChart();
            ZingChart.Chart chart = new ZingChart.Chart();

            zingchart.addChart(chart);

            var graphset = zingchart.getGraphSet();

            var result = javaScriptSerializer.Serialize(graphset);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[]}]}";

            Assert.AreEqual(expected, result);
        }
        /*Should create a zingchart object via method and return expected json*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_WhenPieChartSlicesAdded()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart zingchart = new ZingChart();
            ZingChart.Chart chart = new ZingChart.Chart("RBF.CLA");

            chart.addSlice(30, "Developer 1");
            chart.addSlice(70, "Developer 2");

            zingchart.addChart(chart);

            var graphset = zingchart.getGraphSet();

            var result = javaScriptSerializer.Serialize(graphset);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":""RBF.CLA"",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]}]}";

            Assert.AreEqual(expected, result);
        }
        /*Should create a zingchart object via method and return expected json*/
        [TestMethod]
        public void ZingChart_ShouldReturnJSON_When2PieChartsAdded()
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ZingChart zingchart = new ZingChart();
            string title = "RBF.CLA";

            ZingChart.Chart chart = new ZingChart.Chart(title);
            chart.addSlice(30, "Developer 1");
            chart.addSlice(70, "Developer 2");
            zingchart.addChart(chart);

            title = "HELLO.CLA";
            chart = new ZingChart.Chart(title);
            chart.addSlice(20, "Developer 1");
            chart.addSlice(80, "Developer 2");
            zingchart.addChart(chart);

            var graphset = zingchart.getGraphSet();

            var result = javaScriptSerializer.Serialize(graphset);

            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":""RBF.CLA"",""fontsize"":10},""series"":[{""values"":[30],""text"":""Developer 1""},{""values"":[70],""text"":""Developer 2""}]},{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":""HELLO.CLA"",""fontsize"":10},""series"":[{""values"":[20],""text"":""Developer 1""},{""values"":[80],""text"":""Developer 2""}]}]}";

            Assert.AreEqual(expected, result);
        }       
    }
}
