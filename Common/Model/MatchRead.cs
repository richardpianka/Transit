using System;
using ProtoBuf;
using Transit.Common.Model;

namespace Transit.Analysis
{
    [Serializable]
    [ProtoContract]
    public sealed class MatchRead
    {
        [ProtoMember(1)]
        public bool Included { get; set; }

        [ProtoMember(2)]
        public GpsRead Read { get; set; }

        public MatchRead() { }

        public MatchRead(GpsRead read)
        {
            Included = true;
            Read = read;
        }
    }
}