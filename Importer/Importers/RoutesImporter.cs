using System.IO;
using System.Collections.Generic;

namespace Transit.Importer.Importers
{
    public sealed class RoutesImporter : IImporter
    {
        private readonly List<string> _columnNames = new List<string> { "AgencyId",
                                                                        "RouteId",
                                                                        "RouteShortName",
                                                                        "RouteType",
                                                                        "RouteColor",
                                                                        "RouteTextColor",
                                                                        "RouteLongName",
                                                                        "RouteDesc", };

        public void Import(DirectoryInfo workingDirectory)
        {
            CsvImporter.Ingest(workingDirectory, "routes.txt", "dbo.Routes", _columnNames);
        }
    }
}
