using DLHO.DiscountCodeGenerator.Common.Models;
using DLHO.DiscountCodeGenerator.Common.Repositories;
using DLHO.DiscountCodeGenerator.gRPC.Protos;
using DLHO.DiscountCodeGenerator.Common.Utilities;
using Grpc.Core;

namespace DLHO.DiscountCodeGenerator.gRPC.Services
{
    public class DiscountCodeService(IDiscountCodeRepository repository, ICodeGenerator generator, ILogger<DiscountCodeService> logger) : Protos.DiscountCodeServiceGRPC.DiscountCodeServiceGRPCBase
    {
        public async override Task<GenerateResponse> GenerateCodesAsync(GenerateRequest request, ServerCallContext context)
        {
            try
            {
                var codes = generator.GenerateUniqueCodes(request.Count, request.Length);
                await repository.SaveRangeAsync(codes);
               return new GenerateResponse { Result = true };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                return new GenerateResponse { Result = false };
            }
        }

        public override async Task<GetCodeResponse> GetCodesAsync(GetCodesRequest request, ServerCallContext context)
        {
            var codes = await  repository.GetAllAsync();
            var response = new GetCodeResponse();

            foreach (var item in codes)
            {
                response.Items.Add(new Protos.DiscountCode() { Code = item.Code, Id= item.Id});
            }
           
            return response;
        }

        public async override Task<UseCodeResponse> UseCodeAsync(UseCodeRequest request, ServerCallContext context)
        {
             if(DiscountCodeUtil.IsInValidCodeLength(request.Code.Length)) return new UseCodeResponse { Result = false };
            var discountCode = await repository.GetByCodeAsync(request.Code);
            if (discountCode != null)
            {
                //Do something with the code - like applying discount

                //Delete code from the database
                await repository.DeleteAsync(discountCode);

                return new UseCodeResponse { Result = true };
            }
            return new UseCodeResponse { Result = false };
        }
    }
}
