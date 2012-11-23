using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Transit.Common.Model
{
    [Serializable]
    [ProtoContract]
    public sealed class OrderedStopComparer : IComparer<OrderedStop>
    {
        public int Compare(OrderedStop x, OrderedStop y)
        {
            return x.Order.CompareTo(y.Order);
        }
    }

    [Serializable]
    [ProtoContract]
    public sealed class OrderedStop : Stop, IComparable<OrderedStop>
    {
        [ProtoMember(1)]
        public int Order { get; set; }

        [ProtoMember(2)]
        public override string Id { get; set; }

        [ProtoMember(3)]
        public override string Trip { get; set; }

        [ProtoMember(4)]
        public override Point Point { get; set; }

        public Stop Unordered
        {
            get
            {
                return new Stop(Id, Trip, Point);
            }
        }

        public OrderedStop() { }

        public OrderedStop(string id, int order, string trip, Point point) :
            base(id, trip, point)
        {
            Order = order;
        }

        public int CompareTo(OrderedStop other)
        {
            return Order.CompareTo(other.Order);
        }
    }
}
