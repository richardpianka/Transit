using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProtoBuf;
using Transit.Reader;

namespace Transit.Analysis
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Transit Analysis";
            Console.WriteLine("Analyzing all gps captures...");
            Matcher matcher = new Matcher();
            DatabaseReader reader = new DatabaseReader();
            List<Match> matches = matcher.Match(new List<IReader> { reader }).ToList();
            ResultSet resultSet = new ResultSet(matches, reader.Shapes.ToDictionary(x => x.Id));

            using (Stream writer = File.OpenWrite("matches.transit"))
            {
                Serializer.Serialize(writer, resultSet);
                Serializer.FlushPool();
            }

            Console.WriteLine("done");
        }
    }
}
