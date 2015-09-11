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
            var applied = ((IHistoryRepository)((IAccessor<IServiceProvider>)context).Service.GetService(typeof(IHistoryRepository)))
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = ((IMigrationsAssembly)((IAccessor<IServiceProvider>)context).Service.GetService(typeof(IMigrationsAssembly)))
                .Migrations
                .Select(m => m.Id);

            return !total.Except(applied).Any();
        }
    }
}
