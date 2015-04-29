using System.Linq;
using Microsoft.Framework.Logging;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.Query;

namespace UnicornStore.Logging
{
    public class SqlLoggerProvider : ILoggerProvider
    {
        private static readonly string[] _sqlGenerationComponents = new string[]
        {
            typeof(BatchExecutor).FullName,
            typeof(QueryContextFactory).FullName
        };

        public ILogger CreateLogger(string name)
        {
            // TODO Return our logger for compoents that do SQL generation

            return NullLogger.Instance;
        }
    }
}
