﻿using System.Collections.Generic;
using System.Linq;
using System.IO;
using log4net;

namespace Transit.Importer.Importers
{
    public static class CsvImporter
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Ingest(DirectoryInfo workingDirectory, string fileName, string table, IEnumerable<string> columnNames)
        {
            Database.TruncateTable(table);

            DirectoryInfo transitDirectory = workingDirectory.GetDirectories("Google_transit")[0];
            FileInfo file = transitDirectory.GetFiles(fileName)[0];

            Log.Info(string.Format("Importing {0}...", file.Name));

            List<string> contents = File.ReadAllLines(file.FullName).ToList();
            IEnumerable<string> rawColumns = contents[0].Split(',');
            IEnumerable<string> cleanColumns = rawColumns.Select(Formatter.CleanColumn);

            contents.RemoveAt(0);
            Database.ImportFile(fileName, cleanColumns, contents, table, columnNames);
        }
    }
}
