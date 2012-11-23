using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using Transit.Common.Extensions;
using Transit.Common.Model;
using Transit.Common.Utilities;

namespace Transit.Analysis
{
    [Serializable]
    [ProtoContract]
    public sealed class Match
    {
        [ProtoMember(1)]
        public string Device { get; set; }

        [ProtoMember(2)]
        public Shape Shape { get; set; }

        [ProtoMember(3)]
        public SortedDictionary<OrderedStop, List<MatchRead>> Points { get; set; }

        private Lazy<OrderedStop> _firstStop;
        public OrderedStop FirstStop
        {
            get { return _firstStop.Value; }
        }

        private Lazy<OrderedStop> _lastStop;
        public OrderedStop LastStop
        {
            get { return _lastStop.Value; }
        }

        private Lazy<DateTime> _startTime;
        public DateTime StartTime
        {
            get { return _startTime.Value; }
        }

        private Lazy<DateTime> _endTime;
        public DateTime EndTime
        {
            get { return _endTime.Value; }
        }

        private Lazy<TimeSpan> _duration;
        public TimeSpan Duration
        {
            get { return _duration.Value; }
        }

        public Match()
        {
            Initialize();
        }

        public Match(string device, Shape shape, SortedDictionary<OrderedStop, List<MatchRead>> points)
        {
            Device = device;
            Shape = shape;
            Points = points;
            Initialize();
        }

        public void Initialize()
        {
            _firstStop = new Lazy<OrderedStop>(() => Shape.Stops.First());
            _lastStop = new Lazy<OrderedStop>(() => Shape.Stops.Last());
            _startTime = new Lazy<DateTime>(() => Points[_firstStop.Value].First().Read.Date);
            _endTime = new Lazy<DateTime>(() => Points[_lastStop.Value].Last().Read.Date);
            _duration = new Lazy<TimeSpan>(() => _endTime.Value - _startTime.Value);
        }

        public bool Equals(Match other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Device.EqualsIgnoreCase(Device) && Equals(other.Shape, Shape) && Equals(other.Points, Points);
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
                result = (result*397) ^ (Shape != null ? Shape.GetHashCode() : 0);
                result = (result*397) ^ (Points != null ? Points.GetHashCode() : 0);
                return result;
            }
        }
    }
}
