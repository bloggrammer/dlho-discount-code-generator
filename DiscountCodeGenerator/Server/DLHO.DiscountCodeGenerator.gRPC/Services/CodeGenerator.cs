using DLHO.DiscountCodeGenerator.Common.Models;
using DLHO.DiscountCodeGenerator.Common.Repositories;
using DLHO.DiscountCodeGenerator.Common.Utilities;

namespace DLHO.DiscountCodeGenerator.gRPC.Services;
public class CodeGenerator(IDiscountCodeRepository repository) : ICodeGenerator
{

    public IEnumerable<DiscountCode> GenerateUniqueCodes(uint count, uint length)
    {
       var discountCodes = new HashSet<DiscountCode>(new DiscountCodeComparer());
       int maxAttempts = 1000;
       int attempts = 0;

        if(DiscountCodeUtil.IsInValidCodeLength((int)length))
            return discountCodes;

        while (discountCodes.Count < count)
        {
            var code = GenerateRandomString(length);
            attempts++;
            if (!string.IsNullOrWhiteSpace(code) && !discountCodes.Contains(new DiscountCode { Code = code }) && !repository.Exists(code))
            {
                discountCodes.Add(new DiscountCode() { Code = code});
                attempts = 0;
            }
            if (attempts > maxAttempts) 
            {
                break;
            }
        }
        return discountCodes;
    }
    private string GenerateRandomString(uint length)
    {
        if(DiscountCodeUtil.IsInValidCodeLength((int)length)) return string.Empty;

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, (int)length)
          .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    private readonly Random _random = new();
}