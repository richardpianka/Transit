using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Transit.Importer.Importers
{
    public sealed class GpsImporter : IImporter
    {
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

        public void Import(DirectoryInfo workingDirectory)
        {
            Output.WriteLine("Starting Gps import...");
            //Database.TruncateTable("dbo.Gps");

            // rough here, probably not safe
            DirectoryInfo importDirectory = workingDirectory.GetDirectories("*CSV*")[0];
            IEnumerable<FileInfo> files = importDirectory.GetFiles();

            foreach (FileInfo file in files)
            {
                if (Database.HasBeenImported(file.Name))
                {
                    Output.WriteLine(string.Format("Already imported {0}", file.Name));
                    continue;
                }

                Output.Write(string.Format("Importing {0}...", file.Name));

                List<string> contents = File.ReadAllLines(file.FullName).ToList();
                IEnumerable<string> rawColumns = contents[0].Split(',');
                IEnumerable<string> cleanColumns = rawColumns.Select(Formatter.CleanColumn);
                // remove the header before passing it to import (we aren't importing the header obviously)
                contents.RemoveAt(0);

                Database.ImportFile(file.Name, cleanColumns, contents, "dbo.Gps", _columnNames);
                Output.WriteLine("done");
            }
        }
    }
}
