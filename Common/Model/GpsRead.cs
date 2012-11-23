using System;
using ProtoBuf;

namespace Transit.Common.Model
{
    [Serializable]
    [ProtoContract]
    public sealed class GpsRead
    {
        [ProtoMember(1)]
        public DateTime Date { get; set; }

        [ProtoMember(2)]
        public string ClosestStop { get; set; }

        [ProtoMember(3)]
        public double Distance { get; set; }

        [ProtoMember(4)]
        public Point Point { get; set; }

        public GpsRead() { }

        public GpsRead(DateTime date, string closestStop, double distance, Point point)
        {
            Date = date;
            ClosestStop = closestStop;
            Distance = distance;
            Point = point;
        }
    }
}
