using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Transit.Analysis
{
    [ProtoContract]
    [Serializable]
    public sealed class ResultSet
    {
        [ProtoMember(1, DataFormat = DataFormat.Group)]
        public List<Match> Matches { get; set; }

        public ResultSet() { }

        public ResultSet(List<Match> matches)
        {
            Matches = matches;
        }
    }
}
