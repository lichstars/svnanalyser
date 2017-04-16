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
            SettingsManager settingsManager = new SettingsManager();
            settingsManager.PathToSVNExe = ConfigurationManager.AppSettings["unitTest_pathToSVN"].ToString();
            settingsManager.PathToAnalyse = ConfigurationManager.AppSettings["unitTest_workingCopyFolder"].ToString();
            settingsManager.OutputPath = ConfigurationManager.AppSettings["unitTest_outputFolder"].ToString();

            Analyser analyser = new Analyser(settingsManager);
            Exporter exporter = new Exporter();

            analyser.analyse();
            exporter.asZingChart(settingsManager.OutputPath, null);
            
			Console.WriteLine("Complete. Press any key to exit.");

			Console.ReadKey();		
        }
    }
}
