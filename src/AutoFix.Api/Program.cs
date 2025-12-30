
using AutoFix.Application.Features.Customers.Commands.CreateCustomer;
using AutoFix.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommandHandler).Assembly));
var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
