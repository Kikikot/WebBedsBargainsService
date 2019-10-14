using System;

namespace Externals.LoggSystem
{
    public interface ILogger
    {
        void Log(string message, Exception e = null);
    }
}
