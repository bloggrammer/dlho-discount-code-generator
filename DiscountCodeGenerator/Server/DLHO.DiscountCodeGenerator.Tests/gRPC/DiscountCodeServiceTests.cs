using DLHO.DiscountCodeGenerator.Common.Models;
using DLHO.DiscountCodeGenerator.Common.Repositories;
using DLHO.DiscountCodeGenerator.gRPC.Protos;
using DLHO.DiscountCodeGenerator.gRPC.Services;
using Microsoft.Extensions.Logging;

namespace DLHO.DiscountCodeGenerator.Tests.gRPC;

public class DiscountCodeServiceTests
{
    [Fact]
    public async Task GenerateCodesAsync_Success()
    {
        
        var request = new GenerateRequest { Count = 5, Length = 8 };
        var generatorMock = new Mock<ICodeGenerator>();
        generatorMock.Setup(g => g.GenerateUniqueCodes(request.Count, request.Length))
                     .Returns(new List<Common.Models.DiscountCode> { new() { Code = "TEST1234" } });

        var repositoryMock = new Mock<IDiscountCodeRepository>();
        repositoryMock.Setup(r => r.SaveRangeAsync(It.IsAny<IEnumerable<Common.Models.DiscountCode>>()))
                      .Returns(Task.CompletedTask);

        var loggerMock = new Mock<ILogger<DiscountCodeService>>();

        var discountCodeService = new DiscountCodeService(repositoryMock.Object, generatorMock.Object, loggerMock.Object);

        
        var result = await discountCodeService.GenerateCodesAsync(request, null);

        
        Assert.True(result.Result);
    }

    [Fact]
    public async Task GenerateCodesAsync_ExceptionHandling()
    {
        
        var request = new GenerateRequest { Count = 5, Length = 8 };
        var generatorMock = new Mock<ICodeGenerator>();
        generatorMock.Setup(g => g.GenerateUniqueCodes(request.Count, request.Length))
                     .Throws(new Exception("Test exception"));

        var repositoryMock = new Mock<IDiscountCodeRepository>();
        var loggerMock = new Mock<ILogger<DiscountCodeService>>();

        var discountCodeService = new DiscountCodeService(repositoryMock.Object, generatorMock.Object, loggerMock.Object);

        
        var result = await discountCodeService.GenerateCodesAsync(request, null);

        
        Assert.False(result.Result);
    }

    [Fact]
    public async Task UseCodeAsync_CodeExists_Success()
    {
        
        var request = new UseCodeRequest { Code = "TEST1234" };
        var discountCode = new Common.Models.DiscountCode { Code = "TEST1234" };

        var repositoryMock = new Mock<IDiscountCodeRepository>();
        repositoryMock.Setup(r => r.GetByCodeAsync(request.Code))
                      .Returns(Task.FromResult(discountCode));
        repositoryMock.Setup(r => r.DeleteAsync(discountCode))
                      .Returns(Task.CompletedTask);

        var loggerMock = new Mock<ILogger<DiscountCodeService>>();

        var discountCodeService = new DiscountCodeService(repositoryMock.Object, null, loggerMock.Object);

        
        var result = await discountCodeService.UseCodeAsync(request, null);

        
        Assert.True(result.Result);
    }

    [Fact]
    public async Task UseCodeAsync_CodeDoesNotExist_Failure()
    {
        
        var request = new UseCodeRequest { Code = "NONEXISTENT" };

        var repositoryMock = new Mock<IDiscountCodeRepository>();
        repositoryMock.Setup(r => r.GetByCodeAsync(request.Code))
                      .Returns(Task.FromResult<Common.Models.DiscountCode>(null));

        var loggerMock = new Mock<ILogger<DiscountCodeService>>();

        var discountCodeService = new DiscountCodeService(repositoryMock.Object, null, loggerMock.Object);

        
        var result = await discountCodeService.UseCodeAsync(request, null);

        
        Assert.False(result.Result);
    }

    [Theory]
    [InlineData("123456", false)] 
    [InlineData("123456789", false)]
    [InlineData("1234567", true)]
    [InlineData("12345678", true)]
    public async Task UseCodeAsync_LengthCheck(string code, bool expectedResult)
    {
        
        var repositoryMock = new Mock<IDiscountCodeRepository>();
        var discountCode = new Common.Models.DiscountCode { Code = "TEST1234" };

        repositoryMock.Setup(r => r.GetByCodeAsync(It.IsAny<string>())).Returns(Task.FromResult<Common.Models.DiscountCode>(discountCode));

        var discountCodeService = new DiscountCodeService(repositoryMock.Object, null, null);

        var request = new UseCodeRequest { Code = code };

        
        var result = await discountCodeService.UseCodeAsync(request, null);

        
        Assert.Equal(expectedResult, result.Result);
    }
}
