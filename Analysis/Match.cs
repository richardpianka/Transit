using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ProtoBuf;
using Transit.Common.Extensions;
using Transit.Common.Model;

namespace Transit.Analysis
{
    [Serializable]
    [ProtoContract]
    public sealed class Match
    {
        [ProtoMember(1)]
        public string Device { get; set; }

        [ProtoMember(2)]
        public string ShapeId { get; set; }

        [XmlIgnore]
        public Shape Shape { get; private set; }

        [ProtoMember(3)]
        public SortedDictionary<OrderedStop, List<MatchRead>> Points { get; set; }

        private Lazy<OrderedStop> _firstStop;
        public OrderedStop FirstStop
        {
            get
            {
                AssertInitialized();
                return _firstStop.Value;
            }
        }

        private Lazy<OrderedStop> _lastStop;
        public OrderedStop LastStop
        {
            get
            {
                AssertInitialized(); 
                return _lastStop.Value;
            }
        }

        private Lazy<DateTime> _startTime;
        public DateTime StartTime
        {
            get
            {
                AssertInitialized();
                return _startTime.Value;
            }
        }

        private Lazy<DateTime> _endTime;
        public DateTime EndTime
        {
            get
            {
                AssertInitialized();
                return _endTime.Value;
            }
        }

        private Lazy<TimeSpan> _duration;
        public TimeSpan Duration
        {
            get
            {
                AssertInitialized();
                return _duration.Value;
            }
        }

        public Match() {}

        public Match(string device, string shape, SortedDictionary<OrderedStop, List<MatchRead>> points)
        {
            Device = device;
            ShapeId = shape;
            Points = points;
        }

        public void AssertInitialized()
        {
            if (Shape == null)
            {
                throw new Exception("The shape has not been initialized!");
            }
        }

        public void Initialize(Shape shape)
        {
            if (Shape != null) return;

            Shape = shape;
            _firstStop = new Lazy<OrderedStop>(() => shape.Stops.First());
            _lastStop = new Lazy<OrderedStop>(() => shape.Stops.Last());
            _startTime = new Lazy<DateTime>(() => Points[_firstStop.Value].First().Read.Date);
            _endTime = new Lazy<DateTime>(() => Points[_lastStop.Value].Last().Read.Date);
            _duration = new Lazy<TimeSpan>(() => _endTime.Value - _startTime.Value);
        }

        public bool Equals(Match other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Device.EqualsIgnoreCase(Device) && Equals(other.ShapeId, ShapeId) && Equals(other.Points, Points);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Match)) return false;
            return Equals((Match) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Device != null ? Device.GetHashCode() : 0);
                result = (result*397) ^ (ShapeId != null ? ShapeId.GetHashCode() : 0);
                result = (result*397) ^ (Points != null ? Points.GetHashCode() : 0);
                return result;
            }
        }
    }
}
