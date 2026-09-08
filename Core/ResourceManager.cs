using System;
using System.IO;

namespace Core
{
    public class ResourceManager : IDisposable
    {
        private StreamWriter _writer;
        private bool _disposed = false;

        public ResourceManager(string filePath)
        {
            _writer = new StreamWriter(filePath, append: true);
            Console.WriteLine("ResourceManager initialized: Log file opened.");
        }

        public void Log(string message)
        {
            if (_disposed) throw new ObjectDisposedException("ResourceManager");
            _writer.WriteLine($"[{DateTime.Now}] {message}");
            Console.WriteLine($"Logged to file: {message}");
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _writer.Close(); 
                _disposed = true;
                Console.WriteLine("ResourceManager disposed: Log file closed safely.");
            }
        }
    }
}