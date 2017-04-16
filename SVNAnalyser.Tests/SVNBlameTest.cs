using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVNAnalyser.Core;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class SVNBlameTest
    {
        [TestMethod]
        public void SVNBlame_TestEqualityOperator()
        {
            SVNAuthor author1 = new SVNAuthor("P698972", "Aaron Boyd");
            SVNBlame blame1 = new SVNBlame();
            blame1.author = author1;
            blame1.linesChanged = 100;
            blame1.path = @"C:\Temp";
            blame1.ratioForThisPath = .50;

            SVNBlame blame2 = new SVNBlame();
            blame2.author = author1;
            blame2.linesChanged = 100;
            blame2.path = @"C:\Temp";
            blame2.ratioForThisPath = .50;

            Assert.IsTrue(blame1 == blame2);
        }

        [TestMethod]
        public void SVNBlame_TestInequalityOperator()
        {
            SVNAuthor author1 = new SVNAuthor("P698972", "Aaron Boyd");
            SVNBlame blame1 = new SVNBlame();
            blame1.author = author1;
            blame1.linesChanged = 100;
            blame1.path = @"C:\Temp";
            blame1.ratioForThisPath = .50;

            SVNBlame blame2 = new SVNBlame();
            blame2.author = author1;
            blame2.linesChanged = 100;
            blame2.path = @"C:\Bananas";
            blame2.ratioForThisPath = .50;

            Assert.IsTrue(blame1 != blame2);
        }

        [TestMethod]
        public void SVNBlame_TestEqualsMethod()
        {
            SVNAuthor author1 = new SVNAuthor("P698972", "Aaron Boyd");
            SVNBlame blame1 = new SVNBlame();
            blame1.author = author1;
            blame1.linesChanged = 100;
            blame1.path = @"C:\Temp";
            blame1.ratioForThisPath = .50;

            SVNBlame blame2 = new SVNBlame();
            blame2.author = author1;
            blame2.linesChanged = 100;
            blame2.path = @"C:\Temp";
            blame2.ratioForThisPath = .50;

            Assert.IsTrue(blame1.Equals(blame2));
        }
    }
}
