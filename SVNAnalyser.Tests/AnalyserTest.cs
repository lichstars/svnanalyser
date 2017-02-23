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
            string pathToAnalyse = ConfigurationManager.AppSettings["unitTest_workingCopyFolder"].ToString();
			Analyser analyser = new Analyser();
			bool success = analyser.analyse(pathToAnalyse);
			Assert.AreEqual(success, true);
        }

		[TestMethod]
		public void Analyser_WillReturnFalse_WhenAnalyseNotAWorkingCopy()
		{
            string pathToAnalyse = @"C:\Temp";
			Analyser analyser = new Analyser();
			bool success = analyser.analyse(pathToAnalyse);
			Assert.AreEqual(success, false);
		}

        [TestMethod]
        public void Analyser_WillReturnFalse_WhenAnalyseNotAnExistingFolder()
        {
            string pathToAnalyse = @"C:\Bananas";
            Analyser analyser = new Analyser();
            bool success = analyser.analyse(pathToAnalyse);
            Assert.AreEqual(success, false);
        }
    }
}
