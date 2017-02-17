using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SVNAnalyser.Core;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class SettingsManagerTest
    {
        [TestMethod]
        public void SettingsManger_WillReturnPathToAnalyse()
        {
            SettingsManager settingsManager = new SettingsManager();
            string svnPath = settingsManager.getSVNPathToAnalyse();
        }

        [TestMethod]
        public void SettingsManger_WillReturnOutputPath()
        {
            SettingsManager settingsManager = new SettingsManager();
            string outputPath = settingsManager.getOutputPath();
        }
    }
}
