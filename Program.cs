using MetaSphere.Data;
using MetaSphere.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dialogflowJson = Environment.GetEnvironmentVariable("DIALOGFLOW_CREDENTIALS_JSON");
if (!string.IsNullOrWhiteSpace(dialogflowJson))
{
    try
    {
        var credPath = Path.Combine(Path.GetTempPath(), "dialogflow-credentials.json");
        File.WriteAllText(credPath, dialogflowJson);
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credPath);
    }
    catch
    {
        // ignore; ChatController will validate configuration and return a clear error
    }
}

builder.Services.AddControllersWithViews();

// Resolve app.db path relative to the app's base directory
// This ensures it works correctly on IIS / Somee hosting
var dbPath = Path.Combine(AppContext.BaseDirectory, "app.db");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// In containerized environments the reverse proxy (Railway) will handle TLS.
// Kestrel should listen on the PORT environment variable (Railway provides it).
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    // Listen on all network interfaces for the provided port
    app.Urls.Add($"http://0.0.0.0:{port}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// Auto-create and migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
