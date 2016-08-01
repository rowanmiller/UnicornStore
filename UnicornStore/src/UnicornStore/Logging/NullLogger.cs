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

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        { }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return null;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
