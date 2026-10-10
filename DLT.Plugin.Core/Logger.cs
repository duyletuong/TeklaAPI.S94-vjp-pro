using System;
using System.IO;
using Tekla.Structures.Model;

namespace DLT.Plugin.Core
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string _logDirectory;
        private static readonly string _logFileName = "MY_Component_logger.log";
        private static readonly string _logFilePath;

        // Static constructor: chạy 1 lần duy nhất khi Logger được dùng lần đầu
        static Logger()
        {
            // Lấy đường dẫn từ Model
            _logDirectory = new Model().GetInfo().ModelPath + "\\logs";

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);

            // Ghép thành đường dẫn file log
            _logFilePath = Path.Combine(_logDirectory, _logFileName);
        }

        // Hàm ghi log chung
        private static void WriteLog(string level, string message)
        {
            string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";

            lock (_lock)
            {
                File.AppendAllText(_logFilePath, logLine + Environment.NewLine);
            }

            // Hiển thị ra console
            Console.WriteLine(logLine);
        }

        // Các mức log
        public static void Info(string message) => WriteLog("INFO", message);
        public static void Warning(string message) => WriteLog("WARN", message);
        public static void Error(string message) => WriteLog("ERROR", message);
    }
}
