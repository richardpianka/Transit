using System;
using ProtoBuf;

namespace Transit.Common.Model
{
    [Serializable]
    [ProtoContract]
    public class Stop
    {
        [ProtoMember(1)]
        public virtual string Id { get; set; }

        [ProtoMember(2)]
        public virtual string Trip { get; set; }

        [ProtoMember(3)]
        public virtual Point Point { get; set; }

        public Stop() { }

        public Stop(string id, string trip, Point point)
        {
            Id = id;
            Trip = trip;
            Point = point;
        }

        public bool Equals(Stop other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Stop)) return false;
            return Equals((Stop) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
