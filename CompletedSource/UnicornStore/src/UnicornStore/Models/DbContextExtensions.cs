using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations;

namespace UnicornStore.AspNet.Models
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            return !((IAccessor<Migrator>)context.Database.AsRelational()).Service.GetUnappliedMigrations().Any();
        }
    }
}
