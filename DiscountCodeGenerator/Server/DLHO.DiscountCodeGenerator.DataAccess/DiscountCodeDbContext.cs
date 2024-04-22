using DLHO.DiscountCodeGenerator.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DLHO.DiscountCodeGenerator.DataAccess;
public class DiscountCodeDbContext(DbContextOptions<DiscountCodeDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<DiscountCode> DiscountCodes { get; set; }
}