using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVNAnalyser.Core
{
    public class ZingChart
    {
        /* export a json definition that is recognised by the ZingChart library
         
         * ZingChart JSON definition:
         
            "graphset": [{
                   "type":"pie",
                   "plot": {
                            "tooltip": {"text":"%t"}, 
                            "value-box": { "font-size" : 0, 
                                           "placement":"in", 
                                           "text":"%t", 
                                           "offsetR": "30%", 
                                           "rules": [{"rule": "%v === 0","text": ""}]
                                         }
                           },
                    "title":{"font-size" : 10,"text":"dummy"},
                    "series":[{"values":[0.1],"text":"p694486"},{"values":[0.1],"text":"P695079"},{"values":[0.1],"text":"p695089"},{"values":[0.1],"text":"p696253"},{"values":[0.1],"text":"P696328"},{"values":[0.1],"text":"p696693"},{"values":[0.1],"text":"p697038"},{"values":[0.1],"text":"P697043"},{"values":[0.1],"text":"P698972"},{"values":[0.1],"text":"p699912"},{"values":[0.1],"text":"p700246"}]
                 },  
                 { "type":"pie",
                   "plot": {
                            "tooltip": {"text":"%t"}, 
                            "value-box" : { "font-size" : 0, 
                                            "placement":"in", 
                                            "text":"%t", 
                                            "offsetR": "30%", 
                                            "rules": [{"rule": "%v === 0","text": ""}]
                                          }
                            },
                    "title":{"font-size" : 10, "text":"/NavCoreServices/AusmaqImportTRTBatchProcessHandler.CLA"},
                    "series":[{"values":[0],"text":"p694486"},{"values":[0],"text":"P695079"},{"values":[0],"text":"p695089"},{"values":[0],"text":"p696253"},{"values":[0.1],"text":"P696328"},{"values":[0],"text":"p696693"},{"values":[0.1],"text":"p697038"},{"values":[0],"text":"P697043"},{"values":[0],"text":"P698972"},{"values":[0.8],"text":"p699912"},{"values":[0],"text":"p700246"}]
                 },
                 etc...
        */
        private List<Chart> charts = new List<Chart>();
        public class Graphset
        {
            public List<Chart> graphset;

            public Graphset()
            {
                graphset = new List<Chart>();
            }
        }
        public class Chart
        {
            public string type;
            public Plot plot;
            public Title title;
            public List<Series> series;

            public Chart(string title = "")
            {
                this.type = "pie";
                this.plot = new Plot();
                this.title = new Title(title);
                this.series = new List<Series>();
            }

            public void addSlice(double percentage, string name)
            {
                Series serie = new Series(percentage, name);

                this.series.Add(serie);
            }
        }
        public class Plot
        {
            public Tooltip tooltip;
            public ValueBox valuebox;

            public Plot()
            {
                this.tooltip = new Tooltip();
                this.valuebox = new ValueBox();
            }
        }
        public class Tooltip
        {
            public string text;
            public Tooltip()
            {
                this.text = "%t";
            }
        }
        public class ValueBox
        {
            public double fontsize;
            public string placement;
            public string text;
            public string offsetR;
            public Rules[] rules = new Rules[1];

            public ValueBox()
            {
                fontsize = 0;
                placement = "in";
                text = "%t";
                offsetR = "30%";
                rules[0] = new Rules();
            }
        }
        public class Rules
        {
            public string rule;
            public string text;

            public Rules()
            {
                rule = "%v === 0";
                text = "";
            }
        }
        public class Title
        {
            public string text;
            public double fontsize;

            public Title(string text)
            {
                this.text = text;
                this.fontsize = 10;
            }
        }
        public class Series
        {
            public double[] values = new double[1];
            public string text;

            public Series(double values, string text)
            {
                this.values[0] = values;
                this.text = text;
            }
        }

        public void addChart(Chart chart)
        {  
            this.charts.Add(chart);
        }
        public List<Chart> getCharts()
        {
            return this.charts;
        }
        public Graphset getGraphSet()
        {
            ZingChart.Graphset graphset = new ZingChart.Graphset();
            graphset.graphset = getCharts();
            return graphset;
        }

        // replace any json elements that required a hyphen but was not allowed when setting up class structure
        public string renderJSON(string text)
        {
            text = text.Replace("fontsize", "font-size");
            text = text.Replace("valuebox", "value-box");

            return text;
        }
    }
}
