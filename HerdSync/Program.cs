using BLL.Services;
using BLL.Services.Implementation;
using BLL.Validators;
using DAL.Configuration.Database;
using DAL.Repositories;
using DAL.Repositories.Implementation;
using FluentValidation;
using HerdMark.Services;
using HerdSync.Components;
using HerdSync.Shared.DTO.Animal;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStaticWebAssets();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddMudServices();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<ReadMappingService>();

builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;
    });

builder.Services.AddDbContext<HerdsyncDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
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

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.MapFallback(() => Results.Redirect("/pagenotfound"));

app.Run();