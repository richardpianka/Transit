using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Transit.Common.Model
{
    [Serializable]
    [ProtoContract]
    public sealed class Capture
    {
        [ProtoMember(1)]
        public string Device { get; set; }

        [ProtoMember(2)]
        public DateTime Date { get; set; }

        [ProtoMember(3)]
        public List<GpsRead> Reads { get; set; }

        public Capture() { }

        public Capture(string device, DateTime date, List<GpsRead> reads)
        {
            Device = device;
            Date = date;
            Reads = reads;
        }
    }
}
