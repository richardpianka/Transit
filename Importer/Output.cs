using System;

namespace Transit.Importer
{
    public static class Output
    {
        public static void Write(string text)
        {
            Console.Write(text);
        }

        public static void WriteLine(string text)
        {
            //TODO: write to a log file or something
            //TODO: write to a centralized database log also
            Console.WriteLine(text);
        }
    }
}
