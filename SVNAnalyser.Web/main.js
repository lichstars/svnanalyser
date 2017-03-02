
const COLUMNS_ON_PAGE = 12;
const CLASS_DIV = 'piecharts';
const LEGEND_DIV = 'legend';
let ELEMENT_ID = 0;

function loadData(json) {
	$.getJSON(json, function(data) {
		createLegendChart(data.graphset[0], LEGEND_DIV);
		data.graphset.splice(0,1);	
		createClassCharts(data);
		createRank(data);
	});
}

function createLegendChart(plots, location) {
	let extraJSON = {"value-box" : {"font-size" : 10, "placement":"in", "text":"%t", "offsetR": '30%', "rules": [{ "rule": '%v === 0',"text": ''}]}};
	let plot = $.extend(plots.plot, extraJSON);
	let data = {"type": "pie", "series": plots.series,"plot": plot};
	zingchart.render({ id : location, 
					   data : data, 
					   height: 400, 
					   width: "100%" 
					 });
}

function createClassCharts(data) {
	for(i=0; i<data.graphset.length; i++) {
		let location = "CHART-" + ELEMENT_ID;
		createDivElement(location);
		let element = data.graphset[i];
		zingchart.render({ 
			id : location, 
			data : element, 
			height: 200, 
			width: "100%" 
		});
		ELEMENT_ID+=1;
	}
}

function createRank(data) {
	let hash = new Array();
	let factor = 0;
	let html = "";
	for(i=0; i<data.graphset.length; i++) {
		factor+=1;
		let developers = data.graphset[i].series;
		for(j=0; j<developers.length; j++) {
			hash[ developers[j].text] = (isNaN(hash[ developers[j].text]) ? 0 : hash[ developers[j].text]) + developers[j].values[0];
		}
	}
	sortedHashKeys = getSortedKeys(hash);	
	for(i=0; i<sortedHashKeys.length; i++) {
		hash[sortedHashKeys[i]] = hash[sortedHashKeys[i]]/factor;
	    html += sortedHashKeys[i] + ": " + hash[sortedHashKeys[i]] + "%<br />";
 	}		
	document.getElementById("rank").innerHTML = html;
}
function getSortedKeys(obj) {
    var keys = []; for(var key in obj) keys.push(key);
    return keys.sort(function(a,b){return obj[b]-obj[a]});
}
function createDivElement(elementId) {
    let newElement = document.createElement('graph');
    let ColumnWidth = 12/COLUMNS_ON_PAGE;
    let className = "col-md-" + ColumnWidth;
	newElement.innerHTML = "<div class=\"" + className + "\" id=" + elementId + "></div>";
	document.getElementById(CLASS_DIV).appendChild(newElement);
}

loadData("export.json");
