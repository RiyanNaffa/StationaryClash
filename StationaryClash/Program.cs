using StationaryClash.Components;
using Microsoft.EntityFrameworkCore;
using StationaryClash.Data;
using Blazored.SessionStorage;
using StationaryClash.Interfaces.Repositories;
using StationaryClash.Interfaces.Services;
using StationaryClash.Services;
using StationaryClash.Repositories;
using StationaryClash.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<StationaryClashContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StationaryClashContext") ?? throw new InvalidOperationException("Connection string 'StationaryClashContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddBlazoredSessionStorage();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add HTTP Client
builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5104/")
});

// Add API controller
builder.Services.AddControllers();

// Add singleton injections
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGachaService, GachaService>();
builder.Services.AddScoped<IGachaRepository, GachaRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<RandomRarity>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
