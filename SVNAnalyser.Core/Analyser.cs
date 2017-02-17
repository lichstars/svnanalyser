using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Configuration;

namespace SVNAnalyser.Core
{
    public class Analyser
    {
        public bool analyse(string path)
        {
			//string logPrefix = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " : ";

			bool success = true;
			string arguments = "blame " + path + " --xml";

			Process pProcess = new Process();
			pProcess.StartInfo.FileName = @"C:\Program Files\TortoiseSVN\bin\svn.exe";
			pProcess.StartInfo.Arguments = arguments;
			pProcess.StartInfo.UseShellExecute = false;
			pProcess.StartInfo.RedirectStandardOutput = true;
			pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //System.Diagnostics.ProcessWindowStyle.Normal
			pProcess.StartInfo.CreateNoWindow = true;
			pProcess.Start();
			string result = pProcess.StandardOutput.ReadToEnd();
			pProcess.WaitForExit();

			if (!(result.Length > 0))
				success = false;

			if (!(result.Contains("<blame>") && result.Contains("</blame>")))
				success = false;

			return success;
        }
    }
}
