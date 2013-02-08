using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using Transit.Analysis;
using Transit.Common.Model;

namespace Transit.Inspector
{
    internal static class Data
    {
        private static ResultSet _results;

        public static void Load(string file)
        {
            using (Stream reader = File.OpenRead(file))
            {
                _results = Serializer.Deserialize<ResultSet>(reader);
            }
        }

        public static IEnumerable<Match> Matches
        {
            get { return _results.Matches; }
        }

        public static Dictionary<string, Shape> Shapes
        {
            get { return _results.Shapes; }
        }
    }
}
