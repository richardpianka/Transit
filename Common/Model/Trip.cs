using System;
using System.Collections.Generic;
using ProtoBuf;
using Transit.Common.Extensions;

namespace Transit.Common.Model
{
    [Serializable]
    [ProtoContract]
    public class Trip
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string Shape { get; set; }

        [ProtoMember(3)]
        public List<OrderedStop> Stops { get; set; }

        public Trip() { }

        public Trip(string id, string shape)
        {
            Id = id;
            Shape = shape;
        }

        public bool Equals(Trip other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.EqualsIgnoreCase(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Trip)) return false;
            return Equals((Trip) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
