using System.Threading.Tasks;

namespace PatronTest.FileReading
{
    public interface IFileContentReader
    {
        public Task<string> ReadContentAsync(string path);
    }
}