using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVNAnalyser.Core;
using System.IO;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class ExporterTest
    {
        /* Create an empty Zing Chart object and export it to a file and check that the file length is expected */
        [TestMethod]
        public void Exporter_ShouldMatchFileLengthSize_WhenZingChartIsExportedToAFile()
        {
            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":""HELLO.CLA"",""fontsize"":10},""series"":[{""values"":[100],""text"":""Developer 1""}]}]}";
            int expectedLength = expected.Length;
            
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            Exporter exporter = new Exporter(); 
            List<PlotData> plots = new List<PlotData>();
            
            PlotData.Plot plot = new PlotData.Plot(100, "Developer 1");
            PlotData plotData = new PlotData();
            plotData.title = "HELLO.CLA";
            plotData.plots.Add(plot);

            plots.Add(plotData);

            string path = @"C:\Apps\export.json";

            exporter.asZingChart(path, plots);

            FileInfo f = new FileInfo(path);
            long actualLength = f.Length - 2;
            
            Assert.AreEqual(expectedLength, actualLength);
        }
    }
}

