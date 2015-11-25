using System;
using Microsoft.Extensions.Logging;

namespace UnicornStore.Logging
{
    public class NullLogger : ILogger
    {
        private static NullLogger _instance = new NullLogger();

        public static NullLogger Instance
        {
            get { return _instance; }
        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        { }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return null;
        }
    }
}
