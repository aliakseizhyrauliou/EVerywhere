using System.Reflection;
using EVerywhere.Balance.API;
using EVerywhere.Balance.Application.Features.PaymentFeature.Commands;
using EVerywhere.Balance.Infrastructure.Data;
using EVerywhere.ChargerPoint.API;
using EVerywhere.ModulesCommon;
using EVerywhere.ModulesCommon.Application.Interfaces;
using EVerywhere.Web;
using EVerywhere.Web.Infrastructure.Extensions;
using EVerywhere.Web.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services
    .AddCustomMediator(ModulesAssemblies.Application)
    .AddModulesCommon()
    .AddBalanceModule(builder.Configuration)
    .AddChargerPointModule(builder.Configuration);

builder.Services.AddAutoMapper(ModulesAssemblies.Application);

builder.Services.AddValidatorsFromAssemblies(ModulesAssemblies.Application);

builder.Services.AddHttpClient();
builder.Services.AddScoped<IUser, CurrentUserMock>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.SeedAllDatabases();

app.UseExceptionHandler(options => { });

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();