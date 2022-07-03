using System.Threading.Tasks;
using NUnit.Framework;
using PatronTest.FileReading;

namespace PatronTest.Tests
{
    [TestFixture]
    public class FileReaderTests
    {
        private FileReader _fileReader = null!;

        [SetUp]
        public void Setup()
        {
            _fileReader = new FileReader();
        }

        [Test]
        [Ignore("Todo")]
        public async Task ReadFileAsync()
        {
            Assert.That(await _fileReader.ReadFileAsync("filename.txt"), Is.EqualTo("filename.txt"));
            Assert.That(await _fileReader.ReadFileAsync(@"catalog\filename.txt"), Is.EqualTo("filename.txt"));
        }
    }
}
