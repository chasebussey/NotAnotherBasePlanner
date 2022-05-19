using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NotAnotherBasePlanner.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential()
    );
}
else
{
    builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
}
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<PlannerContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddScoped<MaterialService>();
builder.Services.AddScoped<BuildingService>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<PriceService>();
builder.Services.AddScoped<MaterialRecipeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<PlannerContext>();
    dataContext.Database.Migrate();

    var priceService = scope.ServiceProvider.GetRequiredService<PriceService>();
    priceService.UpdateAllPricesFromFIO();
}

app.Run();
