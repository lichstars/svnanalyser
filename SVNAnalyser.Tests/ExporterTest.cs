using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVNAnalyser.Core;

namespace SVNAnalyser.Tests
{
    [TestClass]
    public class ExporterTest
    {
        [TestMethod]
        public void Exporter_ShouldCreateEmptyGraphsetJSONObject()
        {           
            Exporter exporter = new Exporter();
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var result = javaScriptSerializer.Serialize(exporter.toZingChart());

            string expected = "{\"graphset\":[]}";

            Assert.AreEqual(expected, result);
        }
    }
}
