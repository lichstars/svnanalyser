using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVNAnalyser.Core;
using System.Configuration;

namespace SVNAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            SettingsManager settings = new SettingsManager();
            settings.PathToSVNExe = ConfigurationManager.AppSettings["unitTest_pathToSVN"].ToString();
            settings.PathToAnalyse = ConfigurationManager.AppSettings["unitTest_workingCopyFolder"].ToString();
            settings.OutputPath = ConfigurationManager.AppSettings["unitTest_outputFolder"].ToString();

            Analyser analyser = new Analyser(settings);
            Exporter exporter = new Exporter();

            bool success = analyser.analyse();

            if (success)
            {
                Console.WriteLine("Analysis complete. Press any key to continue.");
                Console.ReadKey();

                success = analyser.calculatePlotData();

                if (success)
                {
                    Console.WriteLine("Calculating plot data complete. Press any key to continue.");
                    Console.ReadKey();

                    exporter.asZingChart(settings.OutputPath + "export.json", analyser.PlotDataList);
                    Console.WriteLine("Complete. Press any key to exit.");
                }
                else
                    Console.WriteLine("Error during calculation of plot data. Press any key to continue.");
            }
            else
                Console.WriteLine("Error during analysis. Press any key to continue.");

            Console.ReadKey();		
        }
    }
}
