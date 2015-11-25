using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace UnicornStore.Models.Identity
{
    public static class IdentityExtensions
    {
        public static void EnsureRolesCreated(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<ApplicationDbContext>();
            if (context.AllMigrationsApplied())
            {
                var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
                foreach (var role in Roles.All)
                {
                    if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole { Name = role });
                    }
                }
            }
        }
    }
}