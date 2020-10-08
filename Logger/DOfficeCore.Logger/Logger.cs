using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DOfficeCore.Logger
{
    public class Logger : ILogger
    {
        private const string ext = ".log";
        private readonly string dateTimeFormat;
        private readonly string logFileName;
        private readonly object lockObject;
        private string logBuffer = string.Empty;
        private int Lines;

        public Logger()
        {
            dateTimeFormat = "dd.MM.yyyy HH:mm:ss.fff";
            logFileName = Assembly.GetExecutingAssembly().GetName().Name + ext;
            lockObject = new object();

            string logHEader = logFileName + " is created.";
            if (File.Exists(logFileName))
            {
                WriteLine(LogLevel.INFO, logHEader);
            }
        }

        [Flags]
        public enum LogLevel
        {
            TRACE,
            INFO,
            DEBUG,
            WARNING,
            ERROR,
            FATAL
        }

        private void WriteLine(LogLevel level,
            string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string sourceFilePath = null,
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "message is empty";
            }

            if (Lines < 20 && level != LogLevel.FATAL)
            {
                logBuffer += DateTime.Now.ToString(dateTimeFormat) + $" -- [{level}] -- " + message + "\n" +
                    "Member name: " + memberName + "\n" +
                    "source file path: " + sourceFilePath + "\n" +
                    "source line number: " + sourceLineNumber + "\n";
                Lines++;
            }
            else
            {
                lock (lockObject)
                {
                    using (StreamWriter writer = new StreamWriter(logFileName, true, System.Text.Encoding.UTF8))
                    {
                        writer.WriteLine(logBuffer);
                        writer.WriteLine(DateTime.Now.ToString(dateTimeFormat) + $" -- [{level}] -- " + message);
                        writer.WriteLine("Member name: " + memberName);
                        writer.WriteLine("source file path: " + sourceFilePath);
                        writer.WriteLine("source line number: " + sourceLineNumber + "\n");
                    }
                    Lines = 0;
                }
            }
        }

        public void WriteLog(LogLevel level, string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string sourceFilePath = null,
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            WriteLine(level, message, memberName, sourceFilePath, sourceLineNumber);
        }
    }
}
