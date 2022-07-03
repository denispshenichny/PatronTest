using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatronTest.FileReading
{
    public class FileQueueProcessor
    {
        private readonly FileItemHash _items = new();
        private readonly IFileContentReader _reader;

        public FileQueueProcessor(IFileContentReader reader)
        {
            _reader = reader;
        }

        public async Task PushFileReadingItem(FileReadingItem item)
        {
            _items.Add(item);
            await ReadFileIfNecessary(item.Path);
        }

        public void RemoveFileReadingItem(FileReadingItem item)
        {
            _items.Remove(item);
        }

        private async Task ReadFileIfNecessary(string path)
        {
            IReadOnlyCollection<FileReadingItem> listeners = _items.GetItems(path);
            if (listeners.Count > 1)
                return;

            string? result = await _reader.ReadContentAsync(path);
            foreach (FileReadingItem l in listeners)
                l.SetContent(result);
        }
    }
}
