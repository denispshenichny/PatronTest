using System.Threading.Tasks;

namespace PatronTest.FileReading
{
    public class FileReadListener
    {
        private bool _isContentSet;
        private string? _content;

        public FileReadListener(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public void SetContent(string content)
        {
            _isContentSet = true;
            _content = content;
        }

        public async Task<string?> GetContentAsync()
        {
            while (!_isContentSet)
                await Task.Delay(25);

            return _content;
        }
    }
}