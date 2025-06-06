using System.Text;
using Dotnet_Angular_Project.Data;
using Dotnet_Angular_Project.interfaces;
using Dotnet_Angular_Project.Models;
using Dotnet_Angular_Project.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Load .env variables
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();
var config = builder.Configuration;

var connStr = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
              ?? builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"✅ Connection String is: {connStr}");

// controller
builder.Services.AddControllers();

// cors
// builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // <--- this is critical
    });
});


builder.Services.AddRazorPages();

// dbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connStr));

// addIdentity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Cookie Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/account/login";
    options.AccessDeniedPath = "/api/account/accessdenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

    // 🔥 This is the critical part
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\": \"Unauthorized\"}");
        }
        context.Response.Redirect(context.RedirectUri); // fallback for non-API
        return Task.CompletedTask;
    };

});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization();

// DI
builder.Services.AddScoped<ITokenService, TokenService>();


// middleware pipelines
var app = builder.Build();

// Migrate database automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // db.Database.Migrate();  // <--- This line does the migration at startup
}


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


app.MapGet("/api/status", () => "API is live");


// This should come after all other route mappings
// This is important to serve index.html for Angular routing
app.MapFallbackToFile("index.html", new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(angularDistPath)
});

app.Run();
