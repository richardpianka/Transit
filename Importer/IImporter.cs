using System.IO;

namespace Transit.Importer
{
    public interface IImporter
    {
        void Import(DirectoryInfo workingPath);
    }
}
