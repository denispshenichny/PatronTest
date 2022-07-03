using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PatronTest.FileReading;

namespace PatronTest.Tests
{
    [TestFixture]
    public class FileReaderTests
    {
        #region TestFileContentReader
        private class TestFileContentReader : IFileContentReader
        {
            public int CallsCount { get; private set; }

            public event EventHandler? Waiting;

            public Task<string?> ReadContentAsync(string path)
            {
                CallsCount++;
                Waiting?.Invoke(this, EventArgs.Empty);
                return Task.FromResult(path)!;
            }
        }
        #endregion

        private TestFileContentReader _contentReader = null!;
        private FileReader _reader = null!;

        [SetUp]
        public void Setup()
        {
            _contentReader = new TestFileContentReader();
            _reader = new FileReader(_contentReader);
        }

        [Test]
        public async Task Read_SameFileSimultaneously()
        {
            _contentReader.Waiting += async (_, _) => await _reader.ReadFileAsync("A");
            _reader.ItemPushed += (_, item) => item.SetContent("A"); //to avoid deadlock
            Assert.That(await _reader.ReadFileAsync("A"), Is.EqualTo("A"));
            Assert.That(_contentReader.CallsCount, Is.EqualTo(1));
        }

        [Test]
        public async Task Read_SameFileSequentially()
        {
            await _reader.ReadFileAsync("A");
            await _reader.ReadFileAsync("A");
            Assert.That(_contentReader.CallsCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Read_DifferentFileSimultaneously()
        {
            EventHandler? handler = null;
            handler = async (_, _) =>
            {
                _contentReader.Waiting -= handler;
                Assert.That(await _reader.ReadFileAsync("B"), Is.EqualTo("B"));
            };
            _contentReader.Waiting += handler;
            Assert.That(await _reader.ReadFileAsync("A"), Is.EqualTo("A"));
            Assert.That(_contentReader.CallsCount, Is.EqualTo(2));
        }

        [Test]
        public async Task Read_DifferentFilesSequentially()
        {
            Assert.That(await _reader.ReadFileAsync("A"), Is.EqualTo("A"));
            Assert.That(await _reader.ReadFileAsync("B"), Is.EqualTo("B"));
            Assert.That(_contentReader.CallsCount, Is.EqualTo(2));
        }
    }
}
