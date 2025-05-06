using Dotnet_Angular_Project.Data;
using Dotnet_Angular_Project.interfaces;
using Dotnet_Angular_Project.Models;
using Dotnet_Angular_Project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Load .env variables
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

// cors
builder.Services.AddCors();

// controller
builder.Services.AddControllers();

// dbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// addIdentity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// DI
builder.Services.AddScoped<ITokenService, TokenService>();


// middleware pipelines
var app = builder.Build();

var angularDistPath = Path.Combine(Directory.GetCurrentDirectory(), "client", "angular-app1", "dist", "angular-app1", "browser");
Console.WriteLine($"Serving Angular from: {angularDistPath}");

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(angularDistPath),
    RequestPath = ""
});

app.UseHttpsRedirection();

app.UseCors();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// app.MapControllers().RequireHost("*").RequirePathStartsWithSegments("/api");

// Basic route for /
// app.MapGet("/", () => "api is live");
app.MapGet("/api/status", () => "API is live");


// This should come after all other route mappings
// This is important to serve index.html for Angular routing
app.MapFallbackToFile("index.html", new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(angularDistPath)
});

app.Run();
