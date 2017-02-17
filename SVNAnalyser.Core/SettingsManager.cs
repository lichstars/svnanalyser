using System;
using System.IO;


namespace SVNAnalyser.Core
{
    public class SettingsManager
    {
        public string getSVNPathToAnalyse()
        {
			string svnPath = @"C:\";
			if ((Directory.Exists(svnPath)) || File.Exists(svnPath))
				return svnPath;
			else
				throw new FileNotFoundException();
        }

        public string getOutputPath()
        {
			string outputPath = @"C:\";
			if ((Directory.Exists(outputPath)))
				return outputPath;
			else
				throw new FileNotFoundException();
        }
    }
}
