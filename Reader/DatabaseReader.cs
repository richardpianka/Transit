using System;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Transit.Common.Model;

namespace Transit.Reader
{
    public sealed class DatabaseReader : IReader
    {
        private readonly IEnumerable<Route> _routes;

        public IEnumerable<Route> Routes
        {
            get { return _routes; }
        }

        public IEnumerable<Shape> Shapes
        {
            get { return Routes.SelectMany(x => x.Shapes); }
        }

        public IEnumerable<Stop> Stops
        {
            get { return Shapes.SelectMany(x => x.Trips.SelectMany(y => y.Stops)).Distinct(); }
        }

        public int CaptureCount
        {
            get { return GetDateDevicePairs().Count; }
        }

        public IEnumerable<Capture> Captures
        {
            get
            {
                return GetDateDevicePairs().AsParallel()
                                           .WithDegreeOfParallelism(1) //TODO: parallelize this correctly
                                           .Select(pair => new Capture(pair.Item2, pair.Item1, GetPoints(pair.Item2, pair.Item1, pair.Item1.AddDays(1))));
            }
        }

        public DatabaseReader()
        {
            _routes = GetRoutes();
            _dateDevicePairs = new Lazy<List<Tuple<DateTime, string>>>(() =>
                                                                           {
                                                                               List<Tuple<DateTime, string>> pairs = new List<Tuple<DateTime, string>>();

                                                                               const string query = "SELECT DISTINCT DATEADD(dd, DATEDIFF(dd, 0, Date), 0) AS [Date], DeviceName FROM Gps ORDER BY DATEADD(dd, DATEDIFF(dd, 0, Date), 0)";
                                                                               DataTable dateTable = Database.SelectQuery(query);

                                                                               foreach (DataRow row in dateTable.Rows)
                                                                               {
                                                                                   string date = Convert.ToString(row["Date"]);
                                                                                   string device = Convert.ToString(row["DeviceName"]);

                                                                                   pairs.Add(new Tuple<DateTime, string>(DateTime.Parse(date), device));
                                                                               }

                                                                               return pairs;
                                                                           });
        }

        private static IEnumerable<Route> GetRoutes()
        {
            List<Route> routes = new List<Route>();

            const string query = "select * from dbo.Routes";
            DataTable routeTable = Database.SelectQuery(query);

            foreach (DataRow row in routeTable.Rows)
            {
                string id = Convert.ToString(row["RouteId"]);
                Route route = new Route(id);
                route.Shapes = GetShapes(id, route).ToList();

                routes.Add(route);
            }
            
            return routes;
        }

        private static IEnumerable<Shape> GetShapes(string id, Route route)
        {
            List<Shape> shapes = new List<Shape>();

            string query = string.Format("select distinct ShapeId from (select *, (select top 1 t.RouteId from dbo.Trips t where t.ShapeId = s.ShapeId) as RouteId from dbo.Shapes s) z where z.RouteId = '{0}'", id);
            DataTable shapeTable = Database.SelectQuery(query);

            foreach (DataRow row in shapeTable.Rows)
            {
                string shapeId = Convert.ToString(row["ShapeId"]);
                Shape shape = new Shape(shapeId, route.Id);
                shape.Trips = GetTrips(shapeId, shape).ToList();

                shapes.Add(shape);
            }

            return shapes;
        }

        private static IEnumerable<Trip> GetTrips(string id, Shape shape)
        {
            List<Trip> trips = new List<Trip>();

            string query = string.Format("select distinct TripId from dbo.Trips where ShapeId = '{0}'", id);
            DataTable tripTable = Database.SelectQuery(query);

            foreach (DataRow row in tripTable.Rows)
            {
                string tripId = Convert.ToString(row["TripId"]);
                Trip trip = new Trip(tripId, shape.Id);
                trip.Stops =  GetStops(tripId, trip).ToList();
                trip.Stops.Sort(new OrderedStopComparer());

                trips.Add(trip);
            }

            return trips;
        }

        private static IEnumerable<OrderedStop> GetStops(string id, Trip trip)
        {
            List<OrderedStop> stops = new List<OrderedStop>();

            string query = string.Format("select s.StopId, s.StopLat, s.StopLon from dbo.StopTimes st left join dbo.Stops s on s.StopId = st.StopId where TripId = '{0}' order by StopSequence asc", id);
            DataTable tripTable = Database.SelectQuery(query);

            for (int i = 0; i < tripTable.Rows.Count; i++)
            {
                DataRow row = tripTable.Rows[i];
                string stopId = Convert.ToString(row["StopId"]);
                double latitude = Convert.ToDouble(row["StopLat"]);
                double longitude = Convert.ToDouble(row["StopLon"]);
                OrderedStop stop = new OrderedStop(stopId, i, trip.Id, new Point(latitude, longitude));

                stops.Add(stop);
            }

            return stops;
        }

//        private const string DataPointQuery = "SELECT * FROM dbo.Gps " +
//                                              "WHERE Date BETWEEN '{1}' AND '{2}' " +
//                                              "	AND DeviceName = '{0}' " +
//                                              "ORDER BY Date";
//
//        private const string TimeFormat = "MM/dd/yy H:mm:ss";

        private Lazy<List<Tuple<DateTime, string>>> _dateDevicePairs;
        public List<Tuple<DateTime, string>> GetDateDevicePairs()
        {
            return _dateDevicePairs.Value;
        }

        public List<GpsRead> GetPoints(string device, DateTime start, DateTime end)
        {
            List<GpsRead> points = new List<GpsRead>();

            DataTable pointTable = Database.StoredProcedure("GetGpsCapture", new SqlParameter("DeviceName", device),
                                                                             new SqlParameter("StartDate", start),
                                                                             new SqlParameter("EndDate", end));

            foreach (DataRow row in pointTable.Rows)
            {
                DateTime date = Convert.ToDateTime(row["Date"]);
                string stopName = row["StopId"].ToString();
                double distance = Convert.ToDouble(row["Distance"]);
                double latitude = Convert.ToDouble(row["Latitude"]);
                double longitude = Convert.ToDouble(row["Longitude"]);

                GpsRead point = new GpsRead(date, stopName, distance, new Point(latitude, longitude));
                points.Add(point);
            }

            return points;
        }
    }
}
