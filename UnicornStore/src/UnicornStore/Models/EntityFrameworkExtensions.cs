using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using UnicornStore.AspNet.Models.Identity;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.Models
{
    public static class EntityFrameworkExtensions
    {
        public static void EnsureMigrationsApplied(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetService<UnicornStoreContext>().Database.Migrate();
            app.ApplicationServices.GetService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
