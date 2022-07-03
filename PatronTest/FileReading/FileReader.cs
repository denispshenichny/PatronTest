using System.Threading.Tasks;

namespace PatronTest.FileReading
{
    public class FileReader
    {
        public Task<string> ReadFileAsync(string path)
        {
            return Task.FromResult(string.Empty);
        }
    }
}
