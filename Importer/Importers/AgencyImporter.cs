using System.IO;
using System.Collections.Generic;

namespace Transit.Importer.Importers
{
    public sealed class AgencyImporter : IImporter
    {
        private readonly List<string> _columnNames = new List<string> { "AgencyId",
                                                                        "AgencyName", };

        public int Total
        {
            get { return 1; }
        }

        public void Import(DirectoryInfo workingDirectory)
        {
            CsvImporter.Ingest(workingDirectory, "agency.txt", "dbo.Agency", _columnNames);
        }
    }
}
