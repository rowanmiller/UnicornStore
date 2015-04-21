using System;
using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity.Relational.Migrations;

namespace UnicornStore.AspNet.Models.Identity
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

        public static void ProcessPreApprovedAdmin(this IApplicationBuilder app, params string[] adminEmails)
        {
            var context = app.ApplicationServices.GetService<ApplicationDbContext>();
            if (context.AllMigrationsApplied())
            {
                var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
                var db = app.ApplicationServices.GetService<ApplicationDbContext>();

                foreach (var admin in adminEmails)
                {
                    var user = userManager.FindByEmailAsync(admin).Result;
                    if (user != null)
                    {
                        if (!userManager.IsInRoleAsync(user, Roles.Admin).Result)
                        {
                            userManager.AddToRoleAsync(user, Roles.Admin).Wait();
                        }
                    }
                    else
                    {
                        if (!db.PreApprovals.Any(a => a.UserEmail == admin && a.Role == Roles.Admin))
                        {
                            db.PreApprovals.Add(new PreApproval
                            {
                                UserEmail = admin,
                                Role = Roles.Admin,
                                ApprovedBy = "System",
                                ApprovedOn = DateTime.Now
                            });

                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}