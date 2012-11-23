using System.IO;
using System.Collections.Generic;

namespace Transit.Importer.Importers
{
    public sealed class StopsImporter : IImporter
    {
        private readonly List<string> _columnNames = new List<string> { "StopId",
                                                                        "StopName",
                                                                        "StopLat",
                                                                        "StopLon", };

        public void Import(DirectoryInfo workingDirectory)
        {
            CsvImporter.Ingest(workingDirectory, "stops.txt", "dbo.Stops", _columnNames);
        }
    }
}
