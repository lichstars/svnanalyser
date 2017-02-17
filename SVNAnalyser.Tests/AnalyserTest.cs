using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVNAnalyser.Core;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class AnalyserTest
    {
        [TestMethod]
        public void Analyser_WillReturnTrue_WhenAnalyseWorkingCopy()
        {
			string pathToAnalyse = @"C:\Apps\Dev\Code\star_tools\";
			Analyser analyser = new Analyser();
			bool success = analyser.analyse(pathToAnalyse);
			Assert.AreEqual(success, true);
        }

		[TestMethod]
		public void Analyser_WillReturnFalse_WhenAnalyseNotAWorkingCopy()
		{
			string pathToAnalyse = @"C:\";
			Analyser analyser = new Analyser();
			bool success = analyser.analyse(pathToAnalyse);
			Assert.AreEqual(success, false);
		}
    }
}
