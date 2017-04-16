using System;
using System.IO;
using System.Configuration;

namespace SVNAnalyser.Core
{

    public class NotSVNExecutableException : Exception
    {
        public NotSVNExecutableException()
        { }
    }

    public class SettingsManager
    {
        private string pathToSVNExe = string.Empty;
        private string outputPath = string.Empty;
        private string pathToAnalyse = string.Empty;
        public int ThreadPoolSize { get; set; }  = Int32.Parse(ConfigurationManager.AppSettings["threadPoolSize"].ToString());

        public string PathToSVNExe
        {
            get
            {
                return pathToSVNExe;
            }
            set
            {
                if (!(File.Exists(value)))
                    throw new FileNotFoundException();

                if (!(value.ToUpper().Contains("SVN.EXE")))
                    throw new NotSVNExecutableException();

                pathToSVNExe = value;
            }
        }

        public string OutputPath
        {
            get
            {
                return outputPath;
            }
            set
            {
                if (!(Directory.Exists(value)))
                    throw new FileNotFoundException();

                outputPath = value;
            }
        }

        public string PathToAnalyse
        {
            get
            {
                return pathToAnalyse;
            }
            set
            {
                if (! ( (Directory.Exists(value)) || ((File.Exists(value))) ))
                    throw new FileNotFoundException();

                pathToAnalyse = value;
            }
        }

        public SettingsManager(string pathToSVNExe, string pathToAnalyse, string outputPath)
        {
            this.PathToSVNExe = pathToSVNExe;
            this.PathToAnalyse = pathToAnalyse;
            this.OutputPath = outputPath;
        }

        public SettingsManager()
        { }
    }
}
