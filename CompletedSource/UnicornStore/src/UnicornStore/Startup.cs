using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using UnicornStore.AspNet.Models.Identity;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile("secrets.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add EF services to the services container.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<UnicornStoreContext>(options => options.UseSqlServer(Configuration.Get("ConnectionStrings:UnicornStore")))
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.Get("ConnectionStrings:UnicornStore")));

            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureFacebookAuthentication(options =>
            {
                options.AppId = "1593240960890768";
                options.AppSecret = Configuration.Get("secrets:facebook:appSecret");
            });

            services.ConfigureGoogleAuthentication(options =>
            {
                options.ClientId = "140672572048-92ggg4tb5ihr7ffats86pk4cgecg0cn4.apps.googleusercontent.com";
                options.ClientSecret = Configuration.Get("secrets:google:clientSecret");
            });

            // Add MVC services to the services container.
            services.AddMvc();

            // Uncomment the following line to add Web API servcies which makes it easier to port Web API 2 controllers.
            // You need to add Microsoft.AspNet.Mvc.WebApiCompatShim package to project.json
            // services.AddWebApiConventions();

        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
                app.EnsureMigrationsApplied();
                app.EnsureSampleData();
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();
            app.UseFacebookAuthentication();
            app.UseGoogleAuthentication();
            app.EnsureRolesCreated();
            app.ProcessPreApprovedAdmin(Configuration.Get("secrets:preApprovedAdmin"));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
