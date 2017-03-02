using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SVNAnalyser.Core
{
    public class Exporter
    {
        /* This function takes 2 arguments.
         * 1. the output file name where the file will be saved to
         * 2. an object/list of data which contains the class names and a list of developers that have touched that file with their % of code changes
         * For each file/class that it finds in the list, 
         * this method create a zingchart sending it the class name as the title and the list of developers with their respective %
         * On completion, the zingchart gets converted to a graphset and serialised
         * This serialised string is then written to the output file saved in outputPath
         */
        public void asZingChart(string outputPath, List<PlotData> plotData)
        {
            ZingChart zingchart = new ZingChart();

            foreach (PlotData data in plotData)
            {
                ZingChart.Chart chart = new ZingChart.Chart(data.title);

                foreach (PlotData.Plot d in data.plots)
                    chart.addSlice(d.percentage, d.developer);

                zingchart.addChart(chart);
            }

            ZingChart.Graphset graphset = zingchart.getGraphSet();
            
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(graphset);

            result = zingchart.renderJSON(result);
            using (StreamWriter sr = new StreamWriter(outputPath))
            {
                sr.WriteLine(result);
            }
        }

    }
}
