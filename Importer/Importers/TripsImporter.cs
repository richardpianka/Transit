using System.IO;
using System.Collections.Generic;

namespace Transit.Importer.Importers
{
    public sealed class TripsImporter : IImporter
    {
        private readonly IEnumerable<string> _columnNames = new List<string> { "RouteId",
                                                                               "TripId",
                                                                               "ShapeId", };

        public void Import(DirectoryInfo workingDirectory)
        {
            CsvImporter.Ingest(workingDirectory, "trips.txt", "dbo.Trips", _columnNames);
        }
    }
}