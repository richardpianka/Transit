using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using ProtoBuf;
using Transit.Reader;

namespace Transit.Analysis
{
    public static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            Console.Title = "Transit Analysis";
            Console.WriteLine("Analyzing all gps captures...");
            Log.Info("Analyzing all gps captures...");
            Matcher matcher = new Matcher();
            DatabaseReader reader = new DatabaseReader();
            List<Match> matches = matcher.Match(new List<IReader> { reader }).ToList();
            ResultSet resultSet = new ResultSet(matches, reader.Shapes.ToDictionary(x => x.Id));

            Directory.CreateDirectory("results");
            string file = Path.Combine("results", DateTime.Now.ToString("yyyy-MM-dd-hh-mm") + ".transit");

            using (Stream writer = File.OpenWrite(file))
            {
                Serializer.Serialize(writer, resultSet);
                Serializer.FlushPool();
            }

            Log.Info("done");
        }
    }
}
