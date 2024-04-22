using DLHO.DiscountCodeGenerator.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DLHO.DiscountCodeGenerator.gRPC.Extensions;
public static class DatabaseServiceExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DiscountCodeDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DiscountCodeDatabase")));
    }
    public static void UseDatabaseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DiscountCodeDbContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
