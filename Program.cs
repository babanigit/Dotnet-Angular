using System.Text;
using Dotnet_Angular_Project.Data;
using Dotnet_Angular_Project.interfaces;
using Dotnet_Angular_Project.Models;
using Dotnet_Angular_Project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtSigningKey = Environment.GetEnvironmentVariable("JWT__SigningKey");

// Load .env variables
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();



// controller
builder.Services.AddControllers();

// cors
builder.Services.AddCors(
//     options =>
// {
//     options.AddPolicy("AllowFrontendApp", builder =>
//     {
//         builder
//             .WithOrigins("http://localhost:5030") // your Angular app's URL
//             .AllowAnyHeader()
//             .AllowAnyMethod()
//             .AllowCredentials(); // <- This is crucial
//     });
// }
);

builder.Services.AddRazorPages();


// dbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// addIdentity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();




builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // or your actual login path
    options.AccessDeniedPath = "/Account/AccessDenied";

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["jwt"]; // This is where your "jwt" cookie is being read
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token; // Setting the token for JWT Bearer Authentication
                }
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey))
        };
    });


builder.Services.AddAuthorization();


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

app.UseCors("AllowFrontendApp");

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
