using DLHO.DiscountCodeGenerator.Common.Models;

namespace DLHO.DiscountCodeGenerator.Common.Repositories;
public interface IDiscountCodeRepository
{

    bool Exists(string code); 
    Task SaveRangeAsync(IEnumerable<DiscountCode> codes);
    Task DeleteAsync(DiscountCode code);
    Task<List<DiscountCode>> GetAllAsync();
    Task<DiscountCode?> GetByCodeAsync(string code);

}