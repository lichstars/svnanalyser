using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Xml;

namespace SVNAnalyser.Core
{
    class AnalyserThreadWorker
    {
        private ManualResetEvent completedEvent;
        private string FilePathToProcess { get; set; }
        public List<SVNBlame> Blames { get; set; } = new List<SVNBlame>();
        private SettingsManager Settings { get; set; }

        public AnalyserThreadWorker(string file, ManualResetEvent e, SettingsManager settings)
        {
            FilePathToProcess = file;
            completedEvent = e;
            Settings = settings;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;

            string logPrefix = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " [thread " + threadIndex + "] : ";

            Console.WriteLine(logPrefix + "started processing " + FilePathToProcess);
            blameFile();
            Console.WriteLine(logPrefix + "completed processing " + FilePathToProcess);

            completedEvent.Set();
        }
        private void blameFile()
        {
            string logPrefix = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " : ";
            string arguments = "blame " + FilePathToProcess + " --xml";
            string target = "";

            Process pProcess = new Process();
            pProcess.StartInfo.FileName = Settings.PathToSVNExe;
            pProcess.StartInfo.Arguments = arguments; //argument
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //System.Diagnostics.ProcessWindowStyle.Normal
            pProcess.StartInfo.CreateNoWindow = true; //not diplay a windows
            pProcess.Start();
            string output = pProcess.StandardOutput.ReadToEnd(); //The output result
            pProcess.WaitForExit();

            StringReader sr = new StringReader(output);
            XmlReader reader = XmlReader.Create(sr);

            while (reader.Read())
            {
                if (reader.NodeType != XmlNodeType.EndElement)
                {
                    switch (reader.Name)
                    {
                        case "target":

                            target = reader.GetAttribute("path").ToUpper();
                            //Console.WriteLine(logPrefix + "Target found in XML: " + target);
                            break;

                        case "author":

                            string author = reader.ReadElementContentAsString();

                            SVNBlame blame = new SVNBlame();
                            SVNAuthor svnAuthor = new SVNAuthor(author.ToUpper(), "unknown");
                            blame.author = svnAuthor;
                            blame.path = target;

                            int index = Blames.FindIndex(r => r.Equals(blame));

                            if (index == -1)
                            {
                                blame.linesChanged++;
                                Blames.Add(blame);
                            }
                            else
                            {
                                Blames[index].linesChanged++;
                            }
                            break;
                    }
                }
            }
        }
    }

    public class Analyser
    {

        private const string NOT_A_WORKING_COPY_ERROR = "E15507";

        private SettingsManager Settings { get; set; }
        public List<SVNBlame> Blames { get; set; } = new List<SVNBlame>();
        public List<PlotData> PlotDataList { get; set; } = new List<PlotData>();

        public Analyser(SettingsManager settings)
        {
            this.Settings = settings;
        }

        public Analyser()
        {
        }

        // Uses SVN LS -R [recursive] command to show SVN files
        // for a given folder (Settings.PathToAnalyse)
        private string getListOfFilesFromFolder()
        {
            string arguments = "ls -R " + Settings.PathToAnalyse;
            
            Process pProcess = new Process();
            pProcess.StartInfo.FileName = Settings.PathToSVNExe;
            pProcess.StartInfo.Arguments = arguments;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //System.Diagnostics.ProcessWindowStyle.Normal
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();

            string fileList = pProcess.StandardOutput.ReadToEnd();

            pProcess.WaitForExit();

            if (fileList.Equals(String.Empty) || (fileList.Contains(NOT_A_WORKING_COPY_ERROR)))
                throw new InvalidOperationException("Error - " + Settings.PathToAnalyse + " is not a SVN working copy");

            return fileList;
        }

        public bool analyse()
        {
            bool success = true;
            string logPrefix = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " : ";

            try
            {
                StringReader stringReader;
                bool singleFileOnly = false;

                // If the path is a directoy, then find all the files
                // under that folder to analyse
                if (Directory.Exists(Settings.PathToAnalyse))
                {
                    stringReader = new StringReader(getListOfFilesFromFolder());
                }
                else
                // Otherwise, just analyse the one file
                {
                    stringReader = new StringReader(Settings.PathToAnalyse);
                    singleFileOnly = true;
                }

                ManualResetEvent[] completedEvents = new ManualResetEvent[Settings.ThreadPoolSize];
                AnalyserThreadWorker[] threadWorkers = new AnalyserThreadWorker[Settings.ThreadPoolSize];

                string line = stringReader.ReadLine();

                while (line != null)
                {
                    for (int i = 0; i < Settings.ThreadPoolSize; i++)
                    {
                        AnalyserThreadWorker worker;
                        completedEvents[i] = new ManualResetEvent(false);
                        if (!singleFileOnly)
                        {
                            Console.WriteLine(logPrefix + "Parsing " + Settings.PathToAnalyse + "\\" + line);
                            worker = new AnalyserThreadWorker(Settings.PathToAnalyse + "\\" + line, completedEvents[i], Settings);
                        }
                        else
                        {
                            Console.WriteLine(logPrefix + "Parsing " + line);
                            worker = new AnalyserThreadWorker(line, completedEvents[i], Settings);
                        }
                       
                        threadWorkers[i] = worker;
                        ThreadPool.QueueUserWorkItem(worker.ThreadPoolCallback, i);

                        line = stringReader.ReadLine();
                        if (line == null)
                            break;
                    }

                    // Cull null events from waiting list if the number of items < threadPoolSize
                    // (Or we have reached the final iteration)
                    List<ManualResetEvent> waitForEvents = new List<ManualResetEvent>();
                    for (int j=0; j< completedEvents.Length; j++)
                    {
                        if (!(completedEvents[j] == null))
                            waitForEvents.Add(completedEvents[j]);
                    }
                   
                    Console.WriteLine(logPrefix + " waiting for workers to finish...");
                    //WaitHandle.WaitAll(waitForEvents.ToArray()); -- Cannot waitAll with MTA threads

                    foreach (ManualResetEvent waitEvent in waitForEvents)
                        waitEvent.WaitOne();

                    Console.WriteLine(logPrefix + " all workers finished!");

                    foreach (AnalyserThreadWorker worker in threadWorkers)
                    {
                        if (worker != null && worker.Blames != null)
                            Blames.AddRange(worker.Blames);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine(logPrefix + " exception : " + e.Message);
            }
			return success;
        }

        public bool calculatePlotData()
        {
            bool success = true;
            string logPrefix = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " : ";

            try
            {
                List<SVNAuthor> SVNAuthorList = new List<SVNAuthor>();

                IEnumerable<string> distinctPaths = Blames.Select(x => x.path).Distinct();

                foreach (var file in distinctPaths)
                {
                    List<SVNBlame> blamesForThisFile = Blames.FindAll(x => x.path == file);
                    int linesChangedThisFile = blamesForThisFile.Sum(blame => blame.linesChanged);
                    double ratioRemaining = 100.0;

                    Console.Write(logPrefix + "Processing " + file + " : ");

                    foreach (var svnBlame in blamesForThisFile)
                    {
                        // 1) Add to the list of authors if we haven't already for this author. 
                        // The list of authors must be in the same order regardless of their input to any file, so they get the same pie chart colour in all displays

                        if (SVNAuthorList.FindIndex(r => r.svnName == svnBlame.author.svnName) == -1)
                            SVNAuthorList.Add(svnBlame.author);

                        // 2) Calculate ratio
                        svnBlame.calculateAndSetRatio(linesChangedThisFile, ratioRemaining);

                        // 3) Log
                        //Console.WriteLine(logPrefix + "processed SVN blame author: " + svnBlame.author.svnName + ", linesChanged: " + svnBlame.linesChanged + ", ratio : " + svnBlame.ratioForThisPath);
                        Console.Write("|");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Calculated ratios for " + distinctPaths.Count() + " files");

                // 4) Group again and create PlotData
                foreach (var file in distinctPaths)
                {
                    PlotData plotData = new PlotData();
                    plotData.title = file;
                    plotData.plots = new List<PlotData.Plot>();

                    List<SVNBlame> blamesForThisFile = Blames.FindAll(x => x.path == file);

                    foreach (SVNAuthor author in SVNAuthorList)
                    {
                        List<SVNBlame> blamesForThisAuthor = blamesForThisFile.FindAll(x => x.author == author);

                        // There should only be one max author per file 
                        foreach (SVNBlame blame in blamesForThisAuthor)
                            plotData.plots.Add(new PlotData.Plot(blame.ratioForThisPath, blame.author.svnName));

                        // But if the author doesn't exist for this file, add in a plot with 0% with this author's name
                        if (blamesForThisAuthor.Count == 0)
                            plotData.plots.Add(new PlotData.Plot(0, author.svnName));
                    }
                    PlotDataList.Add(plotData);
                }

                Console.WriteLine("Plot data created.");
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine(logPrefix + " exception : " + e.Message);
            }
            return success;
        }
    }
}
