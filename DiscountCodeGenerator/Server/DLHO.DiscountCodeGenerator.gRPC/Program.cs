using DLHO.DiscountCodeGenerator.gRPC.Extensions;
using DLHO.DiscountCodeGenerator.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddDiscountCodeServices();
builder.Services.AddGrpc(o =>{ 
    o.EnableDetailedErrors = true;
});

var app = builder.Build();
app.UseDatabaseMigration();

app.MapGrpcService<DiscountCodeService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
