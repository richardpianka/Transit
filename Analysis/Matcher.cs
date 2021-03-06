﻿using System;
using System.Linq;
using System.Collections.Generic;
using log4net;
using Transit.Common.Model;
using Transit.Reader;

namespace Transit.Analysis
{
    public sealed class Matcher
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<Match> Match(IEnumerable<IReader> readers)
        {
            long time = Environment.TickCount;

            List<Match> allMatches = new List<Match>();
            List<Shape> shapes = readers.SelectMany(x => x.Shapes).ToList();

            foreach (IReader reader in readers)
            {
                int progress = 0;

                foreach (Capture capture in reader.Captures)
                {
                    progress++;
                    string captureLabel = string.Format("{0} {1}", capture.Device, capture.Date.ToString("yyyy-MM-dd"));

                    List<Match> matches = MatchCapture(shapes, capture).ToList();
                    allMatches.AddRange(matches);

                    Display.Render(captureLabel, progress, reader.CaptureCount, Environment.TickCount - time);
                    Log.InfoFormat("{0} matches", captureLabel);
                }
            }

            return allMatches;
        }

        private static IEnumerable<Match> MatchCapture(IEnumerable<Shape> shapes, Capture capture)
        {
            //O(n*log(n) * m)
            HashSet<string> readSet = new HashSet<string>(capture.Reads.Select(x => x.ClosestStop));

            //O(?)
            foreach (Shape shape in shapes)
            {
                if (!shape.StopSet.IsSubsetOf(readSet)) continue;

                List<OrderedStop> stopsToMatch = shape.Trips.First().Stops.ToList();
                Dictionary<string, Stop> stopsMap = stopsToMatch.Select(x => x.Unordered).Distinct().ToDictionary(x => x.Id);
                int searchIndex = 0;

                var readsPerStop = new SortedDictionary<OrderedStop, List<MatchRead>>(new OrderedStopComparer());
                List<MatchRead> matchReads = new List<MatchRead>();
                OrderedStop readingStop = null;

                //match shape to reads
                foreach (GpsRead read in capture.Reads)
                {
                    if (!stopsMap.ContainsKey(read.ClosestStop)) continue;
                    Stop currentStop = stopsMap[read.ClosestStop];
                    
                    if (searchIndex < stopsToMatch.Count && stopsToMatch[searchIndex].Equals(currentStop))
                    {
                        if (readingStop != null)
                        {
                            // we've finished a consecutive match of reads to the search stop
                            readsPerStop[readingStop] = matchReads;
                        }

                        readingStop = stopsToMatch[searchIndex];
                        matchReads = new List<MatchRead> { new MatchRead(read) };

                        searchIndex++;
                    }
                    else if (currentStop.Equals(readingStop))
                    {
                        // we're in the middle of a consecutive match of reads to the search stop
                        matchReads.Add(new MatchRead(read));
                    }
                    else if (searchIndex == stopsToMatch.Count)
                    {
                        // we have a full match
                        readsPerStop[readingStop] = matchReads.OrderBy(x => x.Read.Date).ToList();
                        searchIndex = Int32.MaxValue;

                        yield return new Match(capture.Device, shape.Id, readsPerStop);
                    }
                }
            }
        }
    }
}