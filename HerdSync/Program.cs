using BLL.Configuration.Mapping;
using HerdSync.Shared.DTO;
using BLL.Services;
using BLL.Services.Implementation;
using BLL.Validators;
using DAL.Configuration.Database;
using DAL.Services;
using DAL.Services.Implementation;
using FluentValidation;
using HerdSync;
using HerdSync.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MudBlazor;
using MudBlazor.Services;
using Oracle.ManagedDataAccess.Client;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStaticWebAssets();

var configManager = builder.Configuration;
string dbEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "SSMS";
configManager.AddJsonFile($"appsettings.{dbEnvironment}.json", optional: true, reloadOnChange: true);
var connectionString = configManager.GetConnectionString(dbEnvironment == "SSMS" ? "SSMSConnection" : "OracleConnection");
builder.Services.AddMudServices();

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;
    });

// Register business services and AutoMapper
builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IValidator<spd_Species_Detail_DTO>, SpeciesDTOValidator>();
builder.Services.AddAutoMapper(typeof(SpeciesMappingProfile).Assembly);
OracleConfiguration.WalletLocation = "./wwwroot/Wallet_HerdSync";

// Register DbContext with provider-specific configuration
if (dbEnvironment == "SSMS")
{
    builder.Services.AddDbContext<HerdsyncDBContext>(options =>
      options.UseSqlServer(connectionString));
}
else if (dbEnvironment == "Oracle")
{
    builder.Services.AddDbContext<HerdsyncDBContext>(options =>
    {
        options.UseOracle(connectionString);
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsSSMS() || app.Environment.IsOracle())
{
    // The code that loads your services goes here
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
  .AddInteractiveServerRenderMode();

app.Run();