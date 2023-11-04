using JsonApiEndpoints.Entities;
using Microsoft.EntityFrameworkCore;

namespace JsonApiEndpoints;

/// <summary>
/// Provides extension methods for database migrations.
/// </summary>
public static class MigrationExtension
{
    /// <summary>
    /// Applies any pending EF Core migrations to the database.
    /// </summary>
    /// <param name="application">The web application to configure.</param>
    /// <remarks>
    /// This method checks for pending migrations and applies them.
    /// It also sets the command timeout to 300 seconds to ensure that long-running migrations do not fail.
    /// </remarks>
    public static void UseDatabaseMigrations(this WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var sp = scope.ServiceProvider;
            var logger = sp.GetRequiredService<ILogger<Program>>();

            var dbContext = sp.GetRequiredService<ApplicationDbContext>();
            try
            {
                dbContext.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));
                logger.LogInformation("Getting pending migrations...");
                var migrationsToApply = dbContext.Database.GetPendingMigrations();
                logger.LogInformation("Getting pending migrations - Done!");
                if (migrationsToApply.Any())
                {
                    logger.LogInformation("Applying migrations: " + String.Join(",", migrationsToApply));
                    dbContext.Database.Migrate();
                    logger.LogInformation("Migration is completed");
                }
                else
                {
                    logger.LogInformation("The database is up to date :)");
                }

            }
            catch (Exception ex)
            {
                logger.LogCritical("DATABASE ERROR: " + ex.Message);
            }
        }

    }
}