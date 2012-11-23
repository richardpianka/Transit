using System.Collections.Generic;
using System.IO;

namespace Transit.Importer.Importers
{
    public sealed class ShapesImporter : IImporter
    {
        private readonly List<string> _columnNames = new List<string> { "ShapeId",
                                                                        "ShapePtLat",
                                                                        "ShapePtLon",
                                                                        "ShapePtSequence", };

        public void Import(DirectoryInfo workingDirectory)
        {
            CsvImporter.Ingest(workingDirectory, "shapes.txt", "dbo.Shapes", _columnNames);
        }
    }
}
