using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVNAnalyser.Core;

namespace SVNAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            SettingsManager settingsManager = new SettingsManager();
            Analyser analyser = new Analyser();
            Exporter exporter = new Exporter();

            string svnPath = settingsManager.getSVNPathToAnalyse();
            string outputPath = settingsManager.getOutputPath();

            analyser.analyse(svnPath);

            exporter.toZingChart(outputPath);

			Console.WriteLine("Complete. Press any key to exit.");

			Console.ReadKey();		
        }
    }
}
