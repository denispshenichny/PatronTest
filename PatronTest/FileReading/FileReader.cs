using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatronTest.FileReading
{
    public class FileReader
    {
        private readonly IFileContentReader _fileContentReader;
        private readonly FileListenerHash _listeners = new();

        public FileReader(IFileContentReader fileContentReader)
        {
            _fileContentReader = fileContentReader;
        }

        public async Task<string?> ReadFileAsync(string path)
        {
            var listener = new FileReadListener(path);
            _listeners.Add(listener);
            try
            {
                IReadOnlyCollection<FileReadListener> listeners = _listeners.GetListeners(path);
                if (listeners.Count == 1)
                {
                    string result = await _fileContentReader.ReadContentAsync(path);
                    foreach (FileReadListener l in listeners)
                        l.SetContent(result);
                }

                return await listener.GetContentAsync();
            }
            finally
            {
                _listeners.Remove(listener);
            }
        }
    }
}
