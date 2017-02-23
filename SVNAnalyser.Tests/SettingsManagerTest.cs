using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SVNAnalyser.Core;
using System.Configuration;
using System.IO;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class SettingsManagerTest
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SettingsManager_WillException_WhenSettingPathToSVNExe_FileNotFound()
        {
            //arrange
            SettingsManager settingsManager = new SettingsManager();
            //act
            settingsManager.PathToSVNExe = @"C:\Temp\Bananas.exe";
            //assert handled by the exception
        }

        [TestMethod]
        [ExpectedException(typeof(NotSVNExecutableException))]
        public void SettingsManager_WillException_WhenSettingPathToSVNExe_NotContainSVNExe()
        {
            //arrange
            SettingsManager settingsManager = new SettingsManager();
            //act
            settingsManager.PathToSVNExe = @"C:\Windows\explorer.exe";
            //assert handled by the exception
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SettingsManager_WillException_WhenSettingOutputPath_InvalidFolder()
        {
            //arrange
            SettingsManager settingsManager = new SettingsManager();
            //act
            settingsManager.OutputPath = @"C:\Bananas";
            //assert handled by the exception
        }
        [TestMethod]
        public void SettingsManager_SetAndRetrieveOutputPath_WhenValidFolder()
        {
            //arrange
            SettingsManager settingsManager = new SettingsManager();
            //act
            settingsManager.OutputPath = @"C:\Windows";
            //assert
            Assert.AreEqual(settingsManager.OutputPath, @"C:\Windows");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SettingsManager_WillException_WhenSettingPathToAnalyse_InvalidFileOrFolder()
        {
            //arrange
            SettingsManager settingsManager = new SettingsManager();
            //act
            settingsManager.PathToAnalyse = @"C:\Bananas";
            //assert handled by the exception
        }

    }
}
