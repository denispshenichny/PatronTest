using System;
using System.Collections.Generic;

namespace PatronTest.FileReading
{
    public class FileItemHash
    {
        private readonly Dictionary<string, HashSet<FileReadingItem>> _items = new();

        public int ListenersCount => _items.Count;

        public IReadOnlyCollection<FileReadingItem> GetItems(string path)
        {
            return _items.ContainsKey(path) ? _items[path] : Array.Empty<FileReadingItem>();
        }

        public void Add(FileReadingItem item)
        {
            if (!_items.ContainsKey(item.Path))
                _items.Add(item.Path, new HashSet<FileReadingItem>());
            _items[item.Path].Add(item);
        }

        public void Remove(FileReadingItem item)
        {
            if (!_items.ContainsKey(item.Path))
                return;

            _items[item.Path].Remove(item);
            if (_items[item.Path].Count == 0)
                _items.Remove(item.Path);
        }
    }
}
