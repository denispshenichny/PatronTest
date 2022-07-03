using System;
using System.Collections.Generic;

namespace PatronTest.FileReading
{
    public class FileListenerHash
    {
        private readonly Dictionary<string, HashSet<FileReadListener>> _listeners = new();

        public int ListenersCount => _listeners.Count;

        public IReadOnlyCollection<FileReadListener> GetListeners(string path)
        {
            return _listeners.ContainsKey(path) ? _listeners[path] : Array.Empty<FileReadListener>();
        }

        public void Add(FileReadListener listener)
        {
            if (!_listeners.ContainsKey(listener.Path))
                _listeners.Add(listener.Path, new HashSet<FileReadListener>());
            _listeners[listener.Path].Add(listener);
        }

        public void Remove(FileReadListener listener)
        {
            if (!_listeners.ContainsKey(listener.Path))
                return;

            _listeners[listener.Path].Remove(listener);
            if (_listeners[listener.Path].Count == 0)
                _listeners.Remove(listener.Path);
        }
    }
}
