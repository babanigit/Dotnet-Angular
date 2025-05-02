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

<<<<<<< HEAD
app.MapGet("/", () =>
{
    return "api is running";
}).WithName("gethome");


app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
=======
app.UseRouting();
app.MapControllers();

// This is important to serve index.html for Angular routing
app.MapFallbackToFile("index.html", new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(angularDistPath)
});
>>>>>>> 541749af3e9466c4efd5510e4775e7684be2876e

app.Run();
