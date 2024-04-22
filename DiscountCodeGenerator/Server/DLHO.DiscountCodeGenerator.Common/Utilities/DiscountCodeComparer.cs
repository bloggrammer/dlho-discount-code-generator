using DLHO.DiscountCodeGenerator.Common.Models;

namespace DLHO.DiscountCodeGenerator.Common.Utilities;
public class DiscountCodeComparer : IEqualityComparer<DiscountCode>
{
    public bool Equals(DiscountCode? x, DiscountCode? y)
    {
        return x != null && y != null && x.Code == y.Code;
    }
    public int GetHashCode(DiscountCode obj)
    {
        return obj?.Code?.GetHashCode() ?? 0;
    }
}
