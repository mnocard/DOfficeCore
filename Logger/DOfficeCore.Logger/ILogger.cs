using System.Runtime.CompilerServices;

namespace DOfficeCore.Logger
{
    public interface ILogger
    {
        void WriteLog(Logger.LogLevel level, string message, [CallerMemberName] string memberName = null, [CallerFilePath] string sourceFilePath = null, [CallerLineNumber] int sourceLineNumber = 0);
    }
}