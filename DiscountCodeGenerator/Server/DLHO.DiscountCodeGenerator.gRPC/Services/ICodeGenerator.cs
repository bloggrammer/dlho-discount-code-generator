using DLHO.DiscountCodeGenerator.Common.Models;

namespace DLHO.DiscountCodeGenerator.gRPC.Services;
public interface ICodeGenerator
{
    IEnumerable<DiscountCode> GenerateUniqueCodes(uint count, uint length);
}
