using DLHO.DiscountCodeGenerator.Common.Models;
using DLHO.DiscountCodeGenerator.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DLHO.DiscountCodeGenerator.DataAccess.Repositories;
public class DiscountCodeRepository(DiscountCodeDbContext context) : IDiscountCodeRepository
{
    public bool Exists(string code)
    {
        return context.DiscountCodes.Any(e => e.Code == code);
    }
    public async Task<DiscountCode?> GetByCodeAsync(string code)
    {
        return await context.DiscountCodes
                            .AsNoTracking()
                            .Where(x => x.Code == code)
                            .FirstOrDefaultAsync();
    }

    public async Task SaveRangeAsync(IEnumerable<DiscountCode> codes)
    {
        await context.DiscountCodes.AddRangeAsync(codes);
        await context.SaveChangesAsync();
    }
    public async Task DeleteAsync(DiscountCode code)
    {
        if (code != null)
        {
            context.DiscountCodes.Remove(code);
            await context.SaveChangesAsync();
        }
    }
    public async Task<List<DiscountCode>> GetAllAsync()
    {
        return await context.DiscountCodes
                            .AsNoTracking()
                            .ToListAsync();
    }
}