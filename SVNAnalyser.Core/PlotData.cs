using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVNAnalyser.Core
{
    public class PlotData
    {
        public string title;
        public List<Plot> plots = new List<Plot>();

        public class Plot
        {
            public double percentage;
            public string developer;

            public Plot(double values, string text)
            {
                this.percentage = values;
                this.developer = text;
            }
        }
        
    }
}
