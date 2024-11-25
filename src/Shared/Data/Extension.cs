using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Shared.Data
{
    public static class Extension
    {
        public static IApplicationBuilder UseMigration<TContext>
            (this IApplicationBuilder app)
            where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();
            SeedDatabaseAsync(app.ApplicationServices).GetAwaiter().GetResult();
            return app;
        }

        private static async Task MigrateDatabaseAsync<TContext>
            (IServiceProvider applicationServices)
            where TContext : DbContext
        {
            using var scope = applicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            await context.Database.MigrateAsync();
        }

        private static async Task SeedDatabaseAsync
            (IServiceProvider applicationServices)
        {
            using var scope = applicationServices.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
            foreach (var seeder in seeders)
            {
                await seeder.SeedAllAsync();
            }
        }
    }
}
