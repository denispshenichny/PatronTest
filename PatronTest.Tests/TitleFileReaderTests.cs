using System.Threading.Tasks;
using NUnit.Framework;
using PatronTest.FileReading;

namespace PatronTest.Tests
{
    [TestFixture]
    public class TitleFileReaderTests
    {
        private TitleFileReader _fileReader = null!;

        [SetUp]
        public void Setup()
        {
            _fileReader = new TitleFileReader();
        }

        [Test]
        public async Task ReadFileAsync()
        {
            Assert.That(await _fileReader.ReadContentAsync("filename.txt"), Is.EqualTo("filename.txt"));
            Assert.That(await _fileReader.ReadContentAsync(@"catalog\filename.txt"), Is.EqualTo("filename.txt"));
        }
    }
}
