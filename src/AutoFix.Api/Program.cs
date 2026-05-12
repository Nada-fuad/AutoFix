using System.Text;
using AutoFix.Application;
using AutoFix.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoFix.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});





builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowReact");


//######

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    var user = await userManager.FindByEmailAsync("test@test.com");

    if (user is null)
    {
        var newUser = new AppUser
        {
            UserName = "test@test.com",
            Email = "test@test.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newUser, "Test123");

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
