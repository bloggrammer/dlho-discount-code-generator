namespace DLHO.DiscountCodeGenerator.Common.Utilities;
public static class DiscountCodeUtil
{
    public static bool IsInValidCodeLength(int length)
    {
        return (length < 7 || length > 8);            
    }
}
