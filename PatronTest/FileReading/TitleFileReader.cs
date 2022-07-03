using System.IO;
using System.Threading.Tasks;

namespace PatronTest.FileReading
{
    public class TitleFileReader : IFileContentReader
    {
        public virtual Task<string?> ReadContentAsync(string path)
        {
            string fileName = Path.GetFileName(path);
            return Task.FromResult(fileName)!;
        }
    }
}
