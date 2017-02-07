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
    }
}
