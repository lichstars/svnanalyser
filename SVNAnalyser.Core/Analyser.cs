﻿using System;
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
        private SettingsManager Settings { get; set; }

        public Analyser(SettingsManager settings)
        {
            this.Settings = settings;
        }

        public Analyser()
        {

        }

        public bool analyse()
        {
            bool success = true;
            try
            {                
                string arguments = "blame " + Settings.PathToAnalyse + " --xml";

                Process pProcess = new Process();
                pProcess.StartInfo.FileName = Settings.PathToSVNExe;
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

                // If the <blame> tag is complete the action succeeded
                if (!(result.Contains("<blame>") && result.Contains("</blame>")))
                    success = false;
            }
            catch (Exception e)
            {
                success = false;
            }

			return success;
        }
    }
}
