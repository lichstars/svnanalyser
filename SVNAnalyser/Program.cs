using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVNAnalyser.Core;

namespace SVNAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\\Apps\\Dev\\Code\\2017_Q1_0\\db\\stored_objects\\star_oracle\\ongoing_fees_process";
            
            Analyser analyser = new Analyser();

			analyser.analyse(path);

			Console.WriteLine("Complete. Press any key to exit.");

			Console.ReadKey();		
        }
    }
}
