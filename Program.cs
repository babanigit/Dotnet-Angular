using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

var angularDistPath = Path.Combine(Directory.GetCurrentDirectory(), "client", "angular-app1", "dist", "angular-app1", "browser");
Console.WriteLine($"Serving Angular from: {angularDistPath}");

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(angularDistPath),
    RequestPath = ""
});

app.UseRouting();
app.MapControllers();

// This is important to serve index.html for Angular routing
app.MapFallbackToFile("index.html", new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(angularDistPath)
});

app.Run();
