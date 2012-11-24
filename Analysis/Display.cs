using System;
using Transit.Common.Utilities;

namespace Transit.Analysis
{
    internal static class Display
    {
        public static void Render(string file, int progress, int total, long time)
        {
            Console.Clear();

            PrintHeader();
            PrintFile(file);
            PrintTime(time);
            PrintProgress(progress, total);
        }

        private static void PrintHeader()
        {
            Console.Write("Transit Analysis");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" (richardpianka.com)");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void PrintFile(string file)
        {
            Console.Write("Capture ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(file);
            Console.ResetColor();
            Console.WriteLine("...");
        }

        private static void PrintTime(long time)
        {
            Console.Write("Time    ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(FormatUtilities.Duration(time));
            Console.ResetColor();
            Console.WriteLine(" elapsed");
            Console.WriteLine();
        }

        private static void PrintProgress(int progress, int total)
        {
            double percent = Convert.ToDouble(progress) / Convert.ToDouble(total);

            Console.Write(progress + "/" + total + " (");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(string.Format("{0:0}", percent * 100.0) + "%");
            Console.ResetColor();
            Console.WriteLine(")");
        }
    }
}
