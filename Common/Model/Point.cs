using System;
using ProtoBuf;
using Transit.Common.Extensions;

namespace Transit.Common.Model
{
    [Serializable]
    [ProtoContract]
    public sealed class Point
    {
        [ProtoMember(1)]
        public double Latitude { get; set; }

        [ProtoMember(2)]
        public double Longitude { get; set; }

        public Point() { }

        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double HaversineDistance(Point b)
        {
            //O(1)
            const double radius = 3956.6 * 5280; // in feet
            double latitude = ((b.Latitude - Latitude) / 2).ToRadian();
            double longitude = ((b.Longitude - Longitude) / 2).ToRadian();

            double x = Math.Sin(latitude) * Math.Sin(latitude) + Math.Sin(longitude) * Math.Sin(longitude) *
                                                                 Math.Cos(Latitude.ToRadian()) * Math.Cos(b.Latitude.ToRadian());
            double y = 2 * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1 - x));

            return radius * y;
        }

        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Latitude == Latitude && other.Longitude == Longitude;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Point)) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Latitude.GetHashCode();
                result = (result*397) ^ Longitude.GetHashCode();
                return result;
            }
        }
    }
}
