using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GMap.NET;
using GMap.NET.WindowsForms;
using Point = Transit.Common.Model.Point;

namespace Transit.Common.Utilities
{
    public static class MapUtilities
    {
        public static void DrawMap(GMapControl map, List<Point> points, Color color, bool includeMarkers)
        {
            Pen pen = new Pen(color, 3.0f);

            List<PointLatLng> allMapPoints = points.Select(point => new PointLatLng(point.Latitude, point.Longitude)).ToList();
            GMapRoute route = new GMapRoute(allMapPoints, "Route");
            route.Stroke = pen;
            map.ZoomAndCenterRoute(route);

            GMapOverlay overlay = new GMapOverlay(map, "Overlay");
            if (includeMarkers)
            {
                overlay.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng(allMapPoints.First().Lat, allMapPoints.First().Lng)));
                overlay.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleRed(new PointLatLng(allMapPoints.Last().Lat, allMapPoints.Last().Lng)));
            }
            overlay.Routes.Add(route);
            map.Overlays.Add(overlay);
        }

        public static void DrawPoint(GMapControl map, decimal latitude, decimal longitude)
        {
            GMapOverlay overlay = new GMapOverlay(map, "Stop");
            overlay.Markers.Add(new GMap.NET.WindowsForms.Markers.GMapMarkerGoogleGreen(new PointLatLng((double)latitude, (double)longitude)));
            map.Overlays.Add(overlay);
            map.ZoomAndCenterMarkers(overlay.Id);
            map.Zoom -= 2;
        }
    }
}
