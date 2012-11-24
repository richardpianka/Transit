using System.IO;

namespace Transit.Importer
{
    public interface IImporter
    {
        int Total { get; }

        void Import(DirectoryInfo workingPath);
    }
}
