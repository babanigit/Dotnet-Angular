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
// builder.Services.AddDbContext(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// middleware pipeline
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
