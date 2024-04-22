using DLHO.DiscountCodeGenerator.Common.Repositories;
using DLHO.DiscountCodeGenerator.DataAccess.Repositories;
using DLHO.DiscountCodeGenerator.gRPC.Services;

namespace DLHO.DiscountCodeGenerator.gRPC.Extensions;
public static class DiscountCodeServiceExtensions
{
    public static void AddDiscountCodeServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();
        services.AddScoped<ICodeGenerator, CodeGenerator>();
    }
}