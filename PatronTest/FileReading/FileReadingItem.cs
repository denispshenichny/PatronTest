using System.Threading;

namespace PatronTest.FileReading
{
    public class FileReadingItem
    {
        private readonly ManualResetEvent _waitHandle = new(false);
        private string? _content;

        public FileReadingItem(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public void SetContent(string? content)
        {
            _content = content;
            _waitHandle.Set();
        }

        public string? GetContent()
        {
            _waitHandle.WaitOne();
            return _content;
        }
    }
}