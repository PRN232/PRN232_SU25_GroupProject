using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.DataAccess.Context;

public static class HostExtensions
{
    public static IHost ApplyMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<SchoolMedicalDbContext>();

        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("HostExtensions");

        var pending = context.Database.GetPendingMigrations().ToList();
        if (pending.Any())
        {
            logger.LogInformation("Found {Count} pending migrations: {Migrations}",
                pending.Count, string.Join(", ", pending));

            try
            {
                context.Database.Migrate();
                logger.LogInformation("Applied all pending migrations successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while applying migrations");
                throw;
            }
        }
        else
        {
            logger.LogInformation("No pending migrations.");
        }

        return host;
    }
}
