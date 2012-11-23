using System;
using System.Linq;
using System.Collections.Generic;
using ProtoBuf;
using Transit.Common.Extensions;

namespace Transit.Common.Model
{
    [Serializable]
    [ProtoContract]
    public sealed class Shape
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string Route { get; set; }

        [ProtoMember(3)]
        public List<Trip> Trips { get; set; }

        public Shape() { }

        public Shape(string id, string route)
        {
            Id = id;
            Route = route;
        }

        public List<OrderedStop> Stops
        {
            get
            {
                if (Trips.Count == 0) return new List<OrderedStop>();
                return Trips.FirstOrDefault().Stops;
            }
        }

        private HashSet<string> _stopSet;
        private readonly object _lockStopSet = new object();
        public HashSet<string> StopSet
        {
            get
            {
                lock (_lockStopSet)
                {
                    if (_stopSet == null)
                    {
                        if (Trips.Count == 0) return new HashSet<string>();
                        _stopSet = new HashSet<string>(Trips.FirstOrDefault().Stops.Select(x => x.Id));
                    }
                }

                return _stopSet;
            }
        }

        public bool Equals(Shape other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.EqualsIgnoreCase(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Shape)) return false;
            return Equals((Shape) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
