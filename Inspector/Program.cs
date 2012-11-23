using System;
using System.Windows.Forms;

namespace Transit.Inspector
{
    public class StartupParameters
    {
        public static StartupParameters Default = new StartupParameters(string.Empty);
        public static StartupParameters New(string file)
        {
            return new StartupParameters(file);
        }

        public bool FileProvided { get; private set; }
        public string File { get; private set; }

        private StartupParameters(string file)
        {
            FileProvided = !string.IsNullOrEmpty(file);
            File = file;
        }
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main(string[] arguments)
        {
            StartupParameters startupParameters = arguments.Length == 1
                                                ? StartupParameters.New(arguments[0])
                                                : StartupParameters.Default;
                                                                                

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window(startupParameters));
        }
    }
}
