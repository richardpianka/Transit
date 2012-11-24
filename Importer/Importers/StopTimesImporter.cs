using System.IO;
using System.Collections.Generic;

namespace Transit.Importer.Importers
{
    public sealed class StopTimesImporter : IImporter
    {
        private readonly IEnumerable<string> _columnNames = new List<string> { "TripId",
                                                                               "StopId",
                                                                               "StopSequence",
                                                                               "Timepoint", };

        public int Total
        {
            get { return 1; }
        }
        
        public void Import(DirectoryInfo workingDirectory)
        {
            CsvImporter.Ingest(workingDirectory, "stop_times.txt", "dbo.StopTimes", _columnNames);
        }
    }
}
