using System;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations;

namespace UnicornStore.AspNet.Models
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            return !((IMigrator)((IAccessor<IServiceProvider>)context).Service.GetService(typeof(IMigrator))).GetUnappliedMigrations().Any();
        }
    }
}
