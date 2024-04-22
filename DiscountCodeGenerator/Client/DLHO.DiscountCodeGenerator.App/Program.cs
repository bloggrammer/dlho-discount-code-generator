
using DLHO.DiscountCodeGenerator.gRPC.Protos;
using Grpc.Net.Client;

var channel = GrpcChannel.ForAddress("http://localhost:5139");

var discountCodeClient = new DiscountCodeServiceGRPC.DiscountCodeServiceGRPCClient(channel);
while (true)
{
    try
    {
        ProcessRequest(discountCodeClient);

        Console.WriteLine("Enter 'EXIT' to terminate the program, or press any key to continue...");
        var exitCommand = Console.ReadLine();

        if (exitCommand?.Trim().ToUpper() == "EXIT")
            break;
    }
    catch (Exception ex)
    {

        Console.WriteLine($"Something went wrong, {ex.Message}");
    }
}
channel.Dispose();
await channel.ShutdownAsync();



static void ProcessRequest(DiscountCodeServiceGRPC.DiscountCodeServiceGRPCClient  client)
{
   

    Console.WriteLine("----Select Option---");
    Console.WriteLine("GENERATE: 1");
    Console.WriteLine("USECODE: 2");
    Console.WriteLine("VIEW GENERATED CODES: 3");
    var command = Console.ReadLine();

    if (command == "1")
    {
        Console.WriteLine("----Enter Code Length---");
        var length = Convert.ToUInt32(Console.ReadLine());

        Console.WriteLine("----Enter Code Count---");
        var count = Convert.ToUInt32(Console.ReadLine());

        var response = client.GenerateCodesAsync(new GenerateRequest() { Count = (uint)count, Length = (uint)length });
        var result = response.Result ? "SUCCESS" : "FAIL TO GENERATE CODE";

        Console.WriteLine(result);
    }
    else if (command == "2")
    {
        Console.WriteLine("----Enter Code---");
        var code = Console.ReadLine();

        if (string.IsNullOrEmpty(code))
        {
            Console.WriteLine("INVALID COMMAND");
        }
        else
        {
            var response = client.UseCodeAsync(new UseCodeRequest() { Code = code });
           var result = response.Result ? "CODE IS VALID" : "INVALID CODE";

            Console.WriteLine(result);
        }
    }
    else if (command == "3")
    {
        var response = client.GetCodesAsync(new GetCodesRequest());
        foreach (var item in response.Items)
        {
            Console.WriteLine(item.Code);
        }
    }
    else
    {
        Console.WriteLine("INVALID COMMAND");
    }
}
