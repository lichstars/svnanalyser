using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVNAnalyser.Core;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class SVNAuthorTest
    {
        [TestMethod]
        public void SVNAuthor_TestEqualityOperator()
        {
            SVNAuthor author1 = new SVNAuthor("P698972", "Aaron Boyd");
            SVNAuthor author2 = new SVNAuthor("P698972", "Aaron Boyd");

            Assert.IsTrue((author1 == author2));
        }

        [TestMethod]
        public void SVNAuthor_TestInequalityOperator()
        {
            SVNAuthor author1 = new SVNAuthor("P698972", "Aaron Boyd");
            SVNAuthor author2 = new SVNAuthor("P698972", "Baron Lloyd");

            Assert.IsTrue((author1 != author2));
        }

        [TestMethod]
        public void SVNAuthor_TestEqualsMethod()
        {
            SVNAuthor author1 = new SVNAuthor("P698972", "Aaron Boyd");
            SVNAuthor author2 = new SVNAuthor("P698972", "Aaron Boyd");

            Assert.IsTrue(author1.Equals(author2));
        }
    }
}
