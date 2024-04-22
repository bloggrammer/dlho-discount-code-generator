using DLHO.DiscountCodeGenerator.Common.Repositories;
using DLHO.DiscountCodeGenerator.gRPC.Services;

namespace DLHO.DiscountCodeGenerator.Tests.Services;

public class DiscountCodeGeneratorTests
{
    [Fact]
    public void GenerateUniqueCodes_Should_Return_Correct_Number_Of_Codes()
    {
        
        uint count = 5;
        uint length = 8;

        var repositoryMock = new Mock<IDiscountCodeRepository>();
        repositoryMock.Setup(repo => repo.Exists(It.IsAny<string>())).Returns(false);

        var discountCodeGenerator = new CodeGenerator(repositoryMock.Object);

        
        var result = discountCodeGenerator.GenerateUniqueCodes(count, length);

        
        Assert.Equal(count, (uint)result.Count());
    }

    [Fact]
    public void GenerateUniqueCodes_Should_Generate_No_Duplicate_Codes()
    {
        
        uint count = 100;
        uint length = 8;

        var repositoryMock = new Mock<IDiscountCodeRepository>();
        repositoryMock.Setup(repo => repo.Exists(It.IsAny<string>())).Returns(false);

        var discountCodeGenerator = new CodeGenerator(repositoryMock.Object);

        
        var result = discountCodeGenerator.GenerateUniqueCodes(count, length);

        
        var distinctCodes = new HashSet<string>();
        foreach (var code in result)
        {
            Assert.DoesNotContain(code.Code, distinctCodes);
            distinctCodes.Add(code.Code);
        }
    }

}

