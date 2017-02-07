using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVNAnalyser.Core
{
    public class Exporter
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
                   "plot": {"tooltip":{"text":"%t"}, "value-box" : {"font-size" : 0, "placement":"in", "text":"%t", "offsetR": "30%", "rules": [{"rule": "%v === 0","text": ""}]}},"title":{"font-size" : 10,"text":"/NavCoreServices/AusmaqImportTRTBatchProcessHandler.CLA"},"series":[{"values":[0],"text":"p694486"},{"values":[0],"text":"P695079"},{"values":[0],"text":"p695089"},{"values":[0],"text":"p696253"},{"values":[0.1],"text":"P696328"},{"values":[0],"text":"p696693"},{"values":[0.1],"text":"p697038"},{"values":[0],"text":"P697043"},{"values":[0],"text":"P698972"},{"values":[0.8],"text":"p699912"},{"values":[0],"text":"p700246"}]
                 },
                 etc...
        */
        public void toZingChart(string outputPath)
        {
            throw new NotImplementedException();
        }
    }
}
