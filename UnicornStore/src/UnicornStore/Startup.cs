using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UnicornStore.Models;
using UnicornStore.Services;
using UnicornStore.Models.Identity;
using UnicornStore.Models.UnicornStore;
using UnicornStore.Logging;

namespace UnicornStore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("secrets.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<UnicornStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UnicornStore")));

            services.AddSingleton<CategoryCache>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UnicornStore")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddProvider(new SqlLoggerProvider());
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<UnicornStoreContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<UnicornStoreContext>().EnsureSeedData();
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();
            app.EnsureRolesCreated();

            // See comments in config.json for info on enabling Facebook auth
            var facebookId = Configuration["Auth:Facebook:AppId"];
            var facebookSecret = Configuration["Auth:Facebook:AppSecret"];
            if (!string.IsNullOrWhiteSpace(facebookId) && !string.IsNullOrWhiteSpace(facebookSecret))
            {
                app.UseFacebookAuthentication(new FacebookOptions
                {
                    AppId = facebookId,
                    AppSecret = facebookSecret
                });
            }

            // See comments in config.json for info on enabling Google auth
            var googleId = Configuration["Auth:Google:ClientId"];
            var googleSecret = Configuration["Auth:Google:ClientSecret"];
            if (!string.IsNullOrWhiteSpace(googleId) && !string.IsNullOrWhiteSpace(googleSecret))
            {
                app.UseGoogleAuthentication(new GoogleOptions
                {
                    ClientId = googleId,
                    ClientSecret = googleSecret
                });
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
