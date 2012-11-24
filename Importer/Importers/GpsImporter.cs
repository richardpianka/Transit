using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace Transit.Importer.Importers
{
    public sealed class GpsImporter : IImporter
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<string> _columnNames = new List<string> { "Record",
                                                                        "Date",
                                                                        "Latitude",
                                                                        "Longitude",
                                                                        "Altitude",
                                                                        "Temp",
                                                                        "Status",
                                                                        "Course",
                                                                        "GpsFix",
                                                                        "Signal",
                                                                        "MapLink",
                                                                        "Name",
                                                                        "DeviceName", };

        public int Total
        {
            get { return 1; }
        }

        public void Import(DirectoryInfo workingDirectory)
        {
            Log.Info("Starting Gps import...");
            Database.TruncateTable("dbo.Imported");
            Database.TruncateTable("dbo.Gps");

            long start = Environment.TickCount;

            // rough here, probably not safe
            DirectoryInfo importDirectory = workingDirectory.GetDirectories("*CSV*")[0];
            List<FileInfo> files = importDirectory.GetFiles().ToList();
            int progress = 0;

            foreach (FileInfo file in files)
            {
                progress++;
                Display.Render(file.Name, progress, files.Count, Environment.TickCount - start);

                if (Database.HasBeenImported(file.Name))
                {
                    Log.Info(string.Format("Already imported {0}", file.Name));
                    continue;
                }

                Log.Info(string.Format("Importing {0}...", file.Name));

                List<string> contents = File.ReadAllLines(file.FullName).ToList();
                IEnumerable<string> rawColumns = contents[0].Split(',');
                IEnumerable<string> cleanColumns = rawColumns.Select(Formatter.CleanColumn);
                // remove the header before passing it to import (we aren't importing the header obviously)
                contents.RemoveAt(0);

                Database.ImportFile(file.Name, cleanColumns, contents, "dbo.Gps", _columnNames);
            }
        }
    }
}
