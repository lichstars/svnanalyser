using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using SVNAnalyser.Core;
using System.Collections.Generic;
using System.Linq;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class AnalyserTest
    {  
        [TestMethod]
        public void Analyser_WillReturnTrue_WhenAnalyseWorkingCopy()
        {
            SettingsManager settings = new SettingsManager();
            // This path must be an actual SVN working copy folder for this unit test to be valid
            settings.PathToAnalyse = ConfigurationManager.AppSettings["unitTest_workingCopyFolder"].ToString();
            settings.PathToSVNExe = ConfigurationManager.AppSettings["unitTest_pathToSVN"].ToString();
            Analyser analyser = new Analyser(settings);

			bool success = analyser.analyse();

            Assert.AreEqual(success, true);
        }

		[TestMethod]
		public void Analyser_WillReturnFalse_WhenAnalyseNotAWorkingCopy()
		{
            SettingsManager settings = new SettingsManager();
            settings.PathToAnalyse = "C:\\Temp";
            settings.PathToSVNExe = ConfigurationManager.AppSettings["unitTest_pathToSVN"].ToString();
            Analyser analyser = new Analyser(settings);

			bool success = analyser.analyse();

			Assert.AreEqual(success, false);
		}

        [TestMethod]
        public void Analyser_CanGetBlamesList_AfterAnalysisOfWorkingCopy()
        {
            SettingsManager settings = new SettingsManager();
            // This path must be an actual SVN working copy folder for this unit test to be valid
            settings.PathToAnalyse = ConfigurationManager.AppSettings["unitTest_workingCopyFolder"].ToString();
            settings.PathToSVNExe = ConfigurationManager.AppSettings["unitTest_pathToSVN"].ToString();
            Analyser analyser = new Analyser(settings);

            bool success = analyser.analyse();
            bool nonEmptyList = (analyser.Blames.Count > 0);

            Assert.IsTrue(nonEmptyList);
        }

        [TestMethod]
        public void Analyser_BlamesListAreAllValid_AfterAnalysisWorkingCopy()
        {
            SettingsManager settings = new SettingsManager();
            // This path must be an actual SVN working copy folder for this unit test to be valid
            settings.PathToAnalyse = ConfigurationManager.AppSettings["unitTest_workingCopyFolder"].ToString();
            settings.PathToSVNExe = ConfigurationManager.AppSettings["unitTest_pathToSVN"].ToString();
            Analyser analyser = new Analyser(settings);

            bool success = analyser.analyse();
            bool invalidBlameExists = false;

            foreach (SVNBlame blame in analyser.Blames)
            {
                if (!blame.isValid())
                    invalidBlameExists = true;
            }

            Assert.IsFalse(invalidBlameExists);
        }

        [TestMethod]
        public void Analyser_CalculatePlotData_CompletesSuccessfullyOnTestBlames()
        {
            Analyser analyser = new Analyser();
            List<SVNBlame> testBlameList = new List<SVNBlame>();

            SVNAuthor author1 = new SVNAuthor("AUTHOR1", "Author One");
            SVNAuthor author2 = new SVNAuthor("AUTHOR2", "Author Two");
            SVNAuthor author3 = new SVNAuthor("AUTHOR3", "Author Three");

            SVNBlame blame1 = new SVNBlame();
            blame1.author = author1;
            blame1.path = "PATH 1";
            blame1.linesChanged = 100;

            SVNBlame blame2 = new SVNBlame();
            blame2.author = author2;
            blame2.path = "PATH 1";
            blame2.linesChanged = 100;

            SVNBlame blame3 = new SVNBlame();
            blame3.author = author3;
            blame3.path = "PATH 1";
            blame3.linesChanged = 100;

            testBlameList.Add(blame1);
            testBlameList.Add(blame2);
            testBlameList.Add(blame3);

            analyser.Blames = testBlameList;
            bool success = analyser.calculatePlotData();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Analyser_CalculatePlotData_BlameRatioEquals100()
        {
            Analyser analyser = new Analyser();
            List<SVNBlame> testBlameList = new List<SVNBlame>();

            SVNAuthor author1 = new SVNAuthor("AUTHOR1", "Author One");
            SVNAuthor author2 = new SVNAuthor("AUTHOR2", "Author Two");
            SVNAuthor author3 = new SVNAuthor("AUTHOR3", "Author Three");

            SVNBlame blame1 = new SVNBlame();
            blame1.author = author1;
            blame1.path = "PATH 1";
            blame1.linesChanged = 400;

            SVNBlame blame2 = new SVNBlame();
            blame2.author = author2;
            blame2.path = "PATH 1";
            blame2.linesChanged = 400;

            SVNBlame blame3 = new SVNBlame();
            blame3.author = author3;
            blame3.path = "PATH 1";
            blame3.linesChanged = 200;

            testBlameList.Add(blame1);
            testBlameList.Add(blame2);
            testBlameList.Add(blame3);

            analyser.Blames = testBlameList;
            bool success = analyser.calculatePlotData();

            double developerRatio = 0;
            foreach (PlotData plotData in analyser.PlotDataList)
                foreach (PlotData.Plot plot in plotData.plots)
                    developerRatio = developerRatio + plot.percentage;

            Assert.AreEqual(100, developerRatio);
        }
    }
}

