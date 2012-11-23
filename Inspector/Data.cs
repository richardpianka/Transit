using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using Transit.Analysis;

namespace Transit.Inspector
{
    internal static class Data
    {
        public static IEnumerable<Match> LoadMatches(string file)
        {
            using (Stream reader = File.OpenRead(file))
            {
                List<Match> matches = Serializer.Deserialize<List<Match>>(reader);
                //Serializer.Serialize(File.OpenWrite(@"c:\output.transit"), matches);
                return matches;
            }
        }
    }
}
