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
        public void SettingsManger_WillReturnPathToAnalyse_AsSVNPath()
        {
            Assert.Fail();
        }
    }
}
