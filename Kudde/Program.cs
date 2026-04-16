using BCrypt.Net;
using BLL.Services;
using BLL.Services.Authorization;
using BLL.Services.Authorization.Implementation;
using BLL.Services.Implementation;
using BLL.Validators;
using DAL.Configuration.Database;
using DAL.Models.Authentication;
using DAL.Repositories;
using DAL.Repositories.Implementation;
using FluentValidation;
using HerdMark.Services;
using Kudde.Authorization;
using Kudde.Components;
using Kudde.Components.Auth;
using Kudde.Shared.DTO.Animal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStaticWebAssets();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IFarmInviteRepository, FarmInviteRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddSingleton<ReadMappingService>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("FarmOwner", p => p.RequireClaim("RoleCode", "OWNR"));
    options.AddPolicy("FarmManager", p => p.RequireClaim("RoleCode", "OWNR", "MNGR"));
    options.AddPolicy("SystemAdmin", p => p.RequireClaim("IsSystemAdmin", "true"));
});

builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;
    });
builder.Services.AddSingleton<LoginSessionStore>();
builder.Services.AddDbContext<KuddeDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<LiveSessionService>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IAnimalObservationRepository, AnimalObservationRepository>();
builder.Services.AddScoped<IAnimalObservationService, AnimalObservationService>();
builder.Services.AddScoped<IValidator<AnimalDTO>, SpeciesDTOValidator>();
builder.Services.AddScoped<IAnimalEventTypeRepository, AnimalEventTypeRepository>();
builder.Services.AddScoped<IAnimalTagRepository, AnimalTagRepository>();
builder.Services.AddScoped<IAnimalTypeRepository, AnimalTypeRepository>();
builder.Services.AddScoped<IPregnancyRepository, PregnancyRepository>();
builder.Services.AddScoped<IAnimalEventTypeService, AnimalEventTypeService>();
builder.Services.AddScoped<IAnimalTagService, AnimalTagService>();
builder.Services.AddScoped<IAnimalTypeService, AnimalTypeService>();
builder.Services.AddScoped<IPregnancyService, PregnancyService>();
builder.Services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();
builder.Services.AddScoped<ICalendarEventService, CalendarEventService>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<IFarmActivityRepository, FarmActivityRepository>();
builder.Services.AddScoped<IFarmActivityTypeRepository, FarmActivityTypeRepository>();
builder.Services.AddScoped<IFarmService, FarmService>();
builder.Services.AddScoped<IFarmActivityService, FarmActivityService>();
builder.Services.AddScoped<IFarmActivityTypeService, FarmActivityTypeService>();

builder.Services.AddScoped<IFarmUserRepository, FarmUserRepository>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IFarmUserService, FarmUserService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.AddScoped<IProgramRunRepository, ProgramRunRepository>();
builder.Services.AddScoped<IProgramRunAnimalRepository, ProgramRunAnimalRepository>();
builder.Services.AddScoped<IProgramRunObservationRepository, ProgramRunObservationRepository>();
builder.Services.AddScoped<IProgramRunTreatmentRepository, ProgramRunTreatmentRepository>();
builder.Services.AddScoped<IProgramRunService, ProgramRunService>();
builder.Services.AddScoped<IProgramRunAnimalService, ProgramRunAnimalService>();
builder.Services.AddScoped<IProgramRunObservationService, ProgramRunObservationService>();
builder.Services.AddScoped<IProgramRunTreatmentService, ProgramRunTreatmentService>();

builder.Services.AddScoped<IProgramTemplateRepository, ProgramTemplateRepository>();
builder.Services.AddScoped<IProgramTemplateRuleRepository, ProgramTemplateRuleRepository>();
builder.Services.AddScoped<IProgramTemplateRuleTreatmentRepository, ProgramTemplateRuleTreatmentRepository>();
builder.Services.AddScoped<IProgramTemplateService, ProgramTemplateService>();
builder.Services.AddScoped<IProgramTemplateRuleService, ProgramTemplateRuleService>();
builder.Services.AddScoped<IProgramTemplateRuleTreatmentService, ProgramTemplateRuleTreatmentService>();

builder.Services.AddScoped<ITreatmentRepository, TreatmentRepository>();
builder.Services.AddScoped<ITreatmentCategoryRepository, TreatmentCategoryRepository>();
builder.Services.AddScoped<ITreatmentProductRepository, TreatmentProductRepository>();
builder.Services.AddScoped<IConditionRepository, ConditionRepository>();
builder.Services.AddScoped<ITreatmentService, TreatmentService>();
builder.Services.AddScoped<ITreatmentCategoryService, TreatmentCategoryService>();
builder.Services.AddScoped<ITreatmentProductService, TreatmentProductService>();
builder.Services.AddScoped<IConditionService, ConditionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<KuddeDBContext>();
if (!db.UserAccounts.Any(u => u.IsSystemAdmin))
{
    db.UserAccounts.Add(new UserAccountModel
    {
        UserId = Guid.NewGuid(),
        Username = "admin",
        DisplayName = "System Admin",
        PasswordHash = Encoding.UTF8.GetBytes(BCrypt.Net.BCrypt.HashPassword("YourSecurePassword")),
        IsActive = true,
        IsSystemAdmin = true
    });
    db.SaveChanges();
}
// after var app = builder.Build()
app.MapGet("/auth/complete", async (string token, LoginSessionStore store, HttpContext ctx) =>
{
    var principal = store.Retrieve(token);
    if (principal == null) return Results.Redirect("/login");

    await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
        new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        });

    return Results.Redirect("/");
});

app.MapGet("/auth/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.MapFallback(() => Results.Redirect("/pagenotfound"));

app.Run();