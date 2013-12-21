using System;
using System.Collections.Generic;
using ProtoBuf;
using Transit.Analysis;

namespace Transit.Common.Model
{
    [ProtoContract]
    [Serializable]
    public sealed class ResultSet
    {
        [ProtoMember(1, DataFormat = DataFormat.Group)]
        public List<Match> Matches { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.Group)]
        public Dictionary<string, Shape> Shapes { get; set; }

        public ResultSet() { }

        public ResultSet(List<Match> matches, Dictionary<string, Shape> shapes)
        {
            Matches = matches;
            Shapes = shapes;
        }
    }
}
