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
            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""valuebox"":{""fontsize"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":"""",""fontsize"":10},""series"":[]}]}";
            int expectedLength = expected.Length;
            
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            Exporter exporter = new Exporter(); 
            List<PlotData> plots = new List<PlotData>();
            string path = @"C:\Apps";

            exporter.asZingChart(path, plots);

            FileInfo f = new FileInfo(path);
            long actualLength = f.Length;
            
            Assert.AreEqual(expectedLength, actualLength);
        }
    }
}
