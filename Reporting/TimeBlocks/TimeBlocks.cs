using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Reporting;
using Transit.Analysis;
using Transit.Common.Extensions;
using Transit.Common.Model;

namespace Transit.Reporting.TimeBlocks
{
    public sealed class TimeBlocks
    {
        public string Build()
        {
            Data.Load(@"C:\Users\pianka\Desktop\matches.transit");
            List<Match> results = Data.Matches.ToList();
            Dictionary<Tuple<string, string>, HourBucket> hourBuckets = new Dictionary<Tuple<string, string>, HourBucket>();

            // TT1-N_A
            foreach (Match match in results)
            {
                if (!match.ShapeId.Equals("TT1-N_A")) continue;

                List<OrderedStop> stops = match.Shape.Stops.ToList();
                for (int i = 0; i < stops.Count - 1; i++) // skip last stop
                {
                    try
                    {
                        MatchRead start = match.Points[stops[i]].First(read => read.Included && read.Read.Distance < 50);
                        MatchRead stop = match.Points[stops[i + 1]].Last(read => read.Included && read.Read.Distance < 50);

                        foreach (int offset in start.Read.Date.Hour.To(stop.Read.Date.Hour))
                        {
                            var key = new Tuple<string, string>(match.ShapeId, stops[i].Id);
                            if (!hourBuckets.ContainsKey(key))
                            {
                                hourBuckets.Add(key, new HourBucket());
                            }
                            HourBucket hourBucket = hourBuckets[key];
                            hourBucket.Put(offset, TimeByHour(start, stop, offset));
                        }
                    }
                    catch (Exception exception)
                    {
                            
                    }
                }
            }


            foreach (Match match in results)
            {
                if (!match.ShapeId.Equals("TT1-N_A")) continue;

                List<OrderedStop> stops = match.Shape.Stops.ToList();
                for (int i = 0; i < stops.Count - 1; i++) // skip last stop
                {
                    StringBuilder candlestickdata = new StringBuilder();
                    StringBuilder scatterdata = new StringBuilder();
                    StringBuilder linedata = new StringBuilder();
                    int lowest = 24;
                    int highest = 0;
                    double vmin = double.MaxValue;
                    double vmax = double.MinValue;

                    for (int offset = 6; offset < 24; offset++)
                    {
                        string hourDisplayed = Convert.ToString(offset % 12 == 0 ? 12 : offset % 12);
                        var key = new Tuple<string, string>(match.ShapeId, stops[i].Id);
                        Hour hour = hourBuckets[key][offset];
                        if (hour != null)
                        {
                            if (offset > highest) highest = offset;
                            if (offset < lowest) lowest = offset;

                            foreach (WeightedSeconds weightedSeconds in hour)
                            {
                                if (weightedSeconds.Scalar > vmax) vmax = weightedSeconds.Scalar;
                                if (weightedSeconds.Scalar < vmin) vmin = weightedSeconds.Scalar;

                                scatterdata.Append("[{0}, {1}],\n".Format(hour.Offset, weightedSeconds.Scalar));
                            }

                            linedata.Append(String.Format("['{0}', {1}],\n", hourDisplayed, hour.Average));


                            if (hour.Max + hour.StandardDeviation > vmax) vmax = hour.Max + hour.StandardDeviation;
                            if (hour.Min - hour.StandardDeviation < vmin) vmin = hour.Min - hour.StandardDeviation;

                            candlestickdata.Append(String.Format("['{0}', {1}, {2}, {3}, {4}],\n",
                                hourDisplayed,
                                Convert.ToInt32(hour.Min - hour.StandardDeviation),
                                hour.Min,
                                hour.Max,
                                Convert.ToInt32(hour.Max + hour.StandardDeviation)));
                        }
                        else
                        {
                            candlestickdata.Append(String.Format("['{0}', null, null, null, null],\n", hourDisplayed));
                            linedata.Append(String.Format("['{0}', null],\n", hourDisplayed));
                        }
                    }

                    string html = Report.Replace("<%%CANDLESTICKDATA%%>", candlestickdata.ToString())
                                        .Replace("<%%SCATTERDATA%%>", scatterdata.ToString())
                                        .Replace("<%%LINEDATA%%>", linedata.ToString())
                                        .Replace("<%%SHAPE%%>", match.ShapeId)
                                        .Replace("<%%FROM%%>", stops[i].Id)
                                        .Replace("<%%TO%%>", stops[i + 1].Id)
                                        .Replace("<%%VMIN%%>", (vmin).ToString())
                                        .Replace("<%%VMAX%%>", (vmax).ToString())
                                        .Replace("<%%HMIN%%>", (lowest - 1).ToString())
                                        .Replace("<%%HMAX%%>", (highest + 1).ToString())
                                        .Replace("<%%COUNT%%>", (highest - lowest + 1).ToString());

                    File.WriteAllText(String.Format(@"C:\Users\pianka\Desktop\transit\time_blocks\{0}_{1}.html", match.ShapeId, stops[i].Id), html);
                }
            }

            return "";
        }

        private static WeightedSeconds TimeByHour(MatchRead start, MatchRead end, int offset)
        {
            int startSecondsIntoDay = start.Read.Date.SecondsIntoDay();
            int endSecondsIntoDay = end.Read.Date.SecondsIntoDay();

            int startSeconds = Math.Max(startSecondsIntoDay, offset*60*60);
            int endSeconds = Math.Min(endSecondsIntoDay, (offset + 1)*60*60);

            // double for lossless division
            double actual = endSecondsIntoDay - startSecondsIntoDay;
            int measured = endSeconds - startSeconds;

            return new WeightedSeconds(measured, measured/actual);
        }

        private string Report =
            @"
<html>
  <head>
	<style>
		#scatter rect {
			fill: rgba(0,0,0,0);
		}
		
		#line rect {
			fill: rgba(0,0,0,0);
		}
	</style>

    <script type='text/javascript' src='https://www.google.com/jsapi'></script>
    <script type='text/javascript'>
      google.load('visualization', '1', {packages:['corechart']});
      google.setOnLoadCallback(drawChart);
      function drawChart() {
        var candlestickdata = google.visualization.arrayToDataTable([
['Hour', 'Range', '', '', ''],
<%%CANDLESTICKDATA%%>
        ]);

        var scatterdata = google.visualization.arrayToDataTable([
<%%SCATTERDATA%%>
        ], true);

        var linedata = google.visualization.arrayToDataTable([
<%%LINEDATA%%>
        ], true);

        var scatterOptions = {
          title : '<%%SHAPE%%>: <%%FROM%%> to <%%TO%%>',
          hAxis: {title: '', textPosition: 'none', minValue: 6, maxValue: 23},
          vAxis: {title: '', textPosition: 'none', minValue: <%%VMIN%%>, maxValue: <%%VMAX%%>},
          legend: 'none',
		  chartArea: {left: 145, width: 642},
		  series: {0: {color: '#6699dd'}}
        };
		        
		var candleStickOptions = {
          title : '',
          hAxis: {title: '', gridlines: {count:<%%COUNT%%>}},
          vAxis: {title: '', minorGridlines: {count: 5, color: '#f5f5f5'}, minValue: <%%VMIN%%>, maxValue: <%%VMAX%%>},
          legend: 'none',
		  series: {0: {color: '#aaaaaa'}}
        };
		
		var lineOptions = {
          title : '',
          hAxis: {title: 'Hour', gridlines: {count:<%%COUNT%%>}},
          vAxis: {title: 'Seconds', minValue: <%%VMIN%%>, maxValue: <%%VMAX%%>},
          legend: 'none',
          interpolateNulls: true,
          curveType: 'function'
        };

        var candlestickchart = new google.visualization.CandlestickChart(document.getElementById('candlestick'));
        candlestickchart.draw(candlestickdata, candleStickOptions);

        var scatterchart = new google.visualization.ScatterChart(document.getElementById('scatter'));
        scatterchart.draw(scatterdata, scatterOptions);

        var linechart = new google.visualization.LineChart(document.getElementById('line'));
        linechart.draw(linedata, lineOptions);
      }
    </script>
  </head>
  <body>
    <div id='candlestick' style='width: 900px; height: 500px; position: absolute; left: 0; top: 0;'></div>
    <div id='line' style='width: 900px; height: 500px; position: absolute; left: 0; top: 0;'></div>
    <div id='scatter' style='width: 900px; height: 500px; position: absolute; left: 0; top: 0;'></div>
  </body>
</html>
";
    }
}
