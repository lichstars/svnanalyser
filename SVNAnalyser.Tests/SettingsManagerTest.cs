using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using SVNAnalyser.Core;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class SettingsManagerTest
    {
        [TestMethod]
        public void SettingsManger_WillReturnPathToAnalyse_AsString()
        {
            SettingsManager settingsManager = new SettingsManager();
            string svnPath = settingsManager.getSVNPathToAnalyse();
            // Will exception if svnPath is not a string type
        }

        [TestMethod]
        public void SettingsManger_WillReturnPathToAnalyse_AsValidPath()
        {
            SettingsManager settingsManager = new SettingsManager();
            string svnPath = settingsManager.getSVNPathToAnalyse();

            if (!((Directory.Exists(svnPath)) || File.Exists(svnPath)))
                Assert.Fail();
        }

        [TestMethod]
        public void SettingsManger_WillReturnOutputPath_AsString()
        {
            SettingsManager settingsManager = new SettingsManager();
            string outputPath = settingsManager.getOutputPath();
            // Will exception if outputPath is not a string type
        }

        [TestMethod]
        public void SettingsManger_WillReturnOutputPath_AsValidPath()
        {
            SettingsManager settingsManager = new SettingsManager();
            string outputPath = settingsManager.getOutputPath();
            Assert.IsTrue(Directory.Exists(outputPath));
        }

        [TestMethod]
        public void SettingsManger_WillReturnPathToAnalyse_AsSVNPath()
        {
            // Pass until later 
        }
    }
}
