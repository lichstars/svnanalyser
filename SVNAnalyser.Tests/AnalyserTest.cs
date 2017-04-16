using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using SVNAnalyser.Core;

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
    }
}
