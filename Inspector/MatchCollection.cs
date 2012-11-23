using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using Transit.Analysis;

namespace Transit.Inspector
{
    [Serializable]
    [ProtoContract]
    public class MatchCollection
    {
        [ProtoMember(1)]
        public List<Match> Matches { get; set; }

        [ProtoMember(2)]
        public Dictionary<Match, bool> InclusionMap { get; set; }

        public MatchCollection() { }

        public MatchCollection(IEnumerable<Match> matches)
        {
            InclusionMap = new Dictionary<Match, bool>();
            Matches = matches.ToList();
            Matches.ForEach(x => InclusionMap.Add(x, true));
        }

        public MatchCollection SetInclusion(Match match, bool include)
        {
            InclusionMap[match] = include;
            return this;
        }

        public IEnumerable<Match> IncludedMatches
        {
            get { return Matches.Where(x => InclusionMap[x]); }
        }
    }
}
