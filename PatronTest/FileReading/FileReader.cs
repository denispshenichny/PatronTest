using System;
using System.Threading.Tasks;

namespace PatronTest.FileReading
{
    public class FileReader
    {
        private readonly FileQueueProcessor _filesProcessor;
        
        public FileReader(IFileContentReader fileContentReader)
        {
            _filesProcessor = new FileQueueProcessor(fileContentReader);
        }

        public event EventHandler<FileReadingItem>? ItemPushed;

        public async Task<string?> ReadFileAsync(string path)
        {
            var item = new FileReadingItem(path);
            await _filesProcessor.PushFileReadingItem(item);
            ItemPushed?.Invoke(this, item);
            return item.GetContent();
        }
    }
}
