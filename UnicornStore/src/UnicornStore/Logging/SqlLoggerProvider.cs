using System.Linq;
using Microsoft.Framework.Logging;

namespace UnicornStore.Logging
{
    public class SqlLoggerProvider : ILoggerProvider
    {
        private static readonly string[] _whitelist = new string[]
        {
                "Microsoft.Data.Entity.Relational.Update.BatchExecutor",
                "Microsoft.Data.Entity.Query.QueryContextFactory"
        };

        public ILogger CreateLogger(string name)
        {
            if(_whitelist.Contains(name))
            {
                return new SqlLogger();
            }

            return NullLogger.Instance;
        }
    }
}
