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
        private FileInfo logFile;
        public Logger()
        {
            dateTimeFormat = "dd.MM.yyyy HH:mm:ss.fff";
            logFileName = Assembly.GetExecutingAssembly().GetName().Name + ext;
            lockObject = new object();

            string logHEader = logFileName + " is created.";
            if (File.Exists(logFileName))
            {
                WriteLine("INFO", logHEader);
            }
        }

        private void WriteLine(string Message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string sourceFilePath = null,
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (Lines < 20 && !Message.Equals("FATAL") && !Message.Equals("EXIT") && !Message.Equals("DONE"))
            {
                logBuffer += DateTime.Now.ToString(dateTimeFormat) + $" -- {Message}" + "\n" +
                    "Member name: " + memberName + "\n" +
                    "source file path: " + sourceFilePath + "\n" +
                    "source line number: " + sourceLineNumber + "\n";
                Lines++;
            }
            else if (Message.Equals("DONE")) logBuffer += DateTime.Now.ToString(dateTimeFormat) + $" -- {Message}\n\n";
            else
            {
                lock (lockObject)
                {
                    logFile = new FileInfo(logFileName);

                    if (logFile.Length > 500000)
                    {
                        using (StreamWriter writer = new StreamWriter(logFileName, false, System.Text.Encoding.UTF8))
                        {
                            writer.WriteLine(logBuffer);
                            writer.WriteLine(DateTime.Now.ToString(dateTimeFormat) + $" -- {Message}");
                            writer.WriteLine("Member name: " + memberName);
                            writer.WriteLine("source file path: " + sourceFilePath);
                            writer.WriteLine("source line number: " + sourceLineNumber + "\n");
                        }
                    }
                    else
                    {
                        using (StreamWriter writer = new StreamWriter(logFileName, true, System.Text.Encoding.UTF8))
                        {
                            writer.WriteLine(logBuffer);
                            writer.WriteLine(DateTime.Now.ToString(dateTimeFormat) + $" -- {Message}");
                            writer.WriteLine("Member name: " + memberName);
                            writer.WriteLine("source file path: " + sourceFilePath);
                            writer.WriteLine("source line number: " + sourceLineNumber + "\n");
                        }
                    }
                    Lines = 0;
                }
            }
        }

        public void WriteLog(string message,
            [CallerMemberName] string memberName = null,
            [CallerFilePath] string sourceFilePath = null,
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            WriteLine(message, memberName, sourceFilePath, sourceLineNumber);
        }
    }
}
