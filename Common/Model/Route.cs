using System;
using System.Collections.Generic;
using ProtoBuf;
using Transit.Common.Extensions;

namespace Transit.Common.Model
{
    [Serializable]
    [ProtoContract]
    public sealed class Route
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public List<Shape> Shapes { get; set; }

        public Route() { }

        public Route(string id)
        {
            Id = id;
        }

        public bool Equals(Route other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.EqualsIgnoreCase(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Route)) return false;
            return Equals((Route) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
