using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using log4net;
using Transit.Importer.Importers;

namespace Transit.Importer
{
    public sealed class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly IEnumerable<IImporter> Importers = new List<IImporter> { new GpsImporter(),
                                                                                         new StopsImporter(),
                                                                                         new ShapesImporter(),
                                                                                         new TripsImporter(),
                                                                                         new StopTimesImporter(),
                                                                                         new RoutesImporter(),
                                                                                         new AgencyImporter(), };

        public static void Main(string[] args)
        {
            Console.Title = "Transit Importer";
            Console.WriteLine("Starting all import processes...");
            Log.Info("Starting all import processes...");
            DirectoryInfo workingDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["files"]);

            foreach (IImporter importer in Importers)
            {
                importer.Import(workingDirectory);
            }

            Log.Info("done");
        }
    }
}
