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
            DbContext context = app.ApplicationServices.GetService<UnicornStoreContext>();
            context.Database.ApplyMigrations();

            // TODO Should be a migration but need to investigate issue with migration.Sql(...)
            // Note: This would be a good scenario for full text search but using LIKE so that we
            //       can target LocalDb (which doesn't support full text).
            var conn = context.Database.GetDbConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"IF OBJECT_ID(N'[dbo].[SearchProducts]') IS NULL 
                                EXEC('CREATE FUNCTION [dbo].[SearchProducts] ( @term nvarchar(200) )
	                                  RETURNS TABLE 
	                                  AS
	                                  RETURN 
	                                  (
		                                  SELECT * 
		                                  FROM dbo.Product 
		                                  WHERE Product.DisplayName LIKE ''%'' + @term + ''%''
		                                  OR Product.Description LIKE ''%'' + @term + ''%''
	                                  )')";
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            context = app.ApplicationServices.GetService<ApplicationDbContext>();
            context.Database.ApplyMigrations();
        }
    }
}
