using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVNAnalyser.Core;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class ExporterTest
    {
        /* Create an empty Zing Chart object and export it to a file and check that the file length is expected */
        [TestMethod]
        public void Exporter_ShouldMatchFileLengthSize_WhenZingChartIsExportedToAFile()
        {
            string expected = @"{""graphset"":[{""type"":""pie"",""plot"":{""tooltip"":{""text"":""%t""},""value-box"":{""font-size"":0,""placement"":""in"",""text"":""%t"",""offsetR"":""30%"",""rules"":[{""rule"":""%v === 0"",""text"":""""}]}},""title"":{""text"":""HELLO.CLA"",""font-size"":10},""series"":[{""values"":[100],""text"":""Developer 1""}]}]}";
            int expectedLength = expected.Length;
            
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            Exporter exporter = new Exporter(); 
            List<PlotData> plots = new List<PlotData>();
            
            PlotData.Plot plot = new PlotData.Plot(100, "Developer 1");
            PlotData plotData = new PlotData();
            plotData.title = "HELLO.CLA";
            plotData.plots.Add(plot);

            plots.Add(plotData);

            string path = ConfigurationManager.AppSettings["unitTest_outputFolder"].ToString() + "export.json";

            exporter.asZingChart(path, plots);

            FileInfo f = new FileInfo(path);
            long actualLength = f.Length-2;
            
            Assert.AreEqual(expectedLength, actualLength);
        }

        //purely a sample of how to use Exporter.asZingChart
        [TestMethod]
        public void Exporter_ShouldCreateJSONfile_ForTWOClasses()
        {
            List<PlotData> plotDatas = new List<PlotData>();

            PlotData plotData = new PlotData();

            plotData.title = "Developers Legend";
            PlotData.Plot plot1 = new PlotData.Plot(0.4, "Developer 1");
            PlotData.Plot plot2 = new PlotData.Plot(0.15, "Developer 2");
            PlotData.Plot plot3 = new PlotData.Plot(0.125, "Developer 3");
            PlotData.Plot plot4 = new PlotData.Plot(0.075, "Developer 4");
            PlotData.Plot plot5 = new PlotData.Plot(0.25, "Developer 5");

            plotData.plots.Add(plot1);
            plotData.plots.Add(plot2);
            plotData.plots.Add(plot3);
            plotData.plots.Add(plot4);
            plotData.plots.Add(plot5);

            plotDatas.Add(plotData);


            PlotData plotData1 = new PlotData();

            plotData1.title = "Class 1";
            plot1 = new PlotData.Plot(30, "Developer 1");
            plot2 = new PlotData.Plot(10, "Developer 2");
            plot3 = new PlotData.Plot(10, "Developer 3");
            plot4 = new PlotData.Plot(10, "Developer 4");
            plot5 = new PlotData.Plot(40, "Developer 5");

            plotData1.plots.Add(plot1);
            plotData1.plots.Add(plot2);
            plotData1.plots.Add(plot3);
            plotData1.plots.Add(plot4);
            plotData1.plots.Add(plot5);

            plotDatas.Add(plotData1);

            
            PlotData plotData2 = new PlotData();
            plotData2.title = "Class 2";
            plot1 = new PlotData.Plot(50, "Developer 1");
            plot2 = new PlotData.Plot(20, "Developer 2");
            plot3 = new PlotData.Plot(15, "Developer 3");
            plot4 = new PlotData.Plot(5, "Developer 4");
            plot5 = new PlotData.Plot(10, "Developer 5");

            plotData2.plots.Add(plot1);
            plotData2.plots.Add(plot2);
            plotData2.plots.Add(plot3);
            plotData2.plots.Add(plot4);
            plotData2.plots.Add(plot5);

            plotDatas.Add(plotData2);

            Exporter exporter = new Exporter();
            string path = ConfigurationManager.AppSettings["unitTest_outputFolder"].ToString() + "export.json";

            exporter.asZingChart(path, plotDatas);            
        }
    }
}

