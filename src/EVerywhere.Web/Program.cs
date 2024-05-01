using System.Reflection;
using EVerywhere.Balance.API;
using EVerywhere.Balance.Infrastructure.Data;
using EVerywhere.ModulesCommon;
using EVerywhere.ModulesCommon.Application.Interfaces;
using EVerywhere.Web.Infrastructure.Extensions;
using EVerywhere.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

builder.Services
    .AddCustomMediator()
    .AddModulesCommon()
    .AddBalanceModule(builder.Configuration);

builder.Services.AddHttpClient();
builder.Services.AddScoped<IUser, CurrentUserMock>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.InitialiseBalanceDatabaseAsync();

app.UseExceptionHandler(options => { });

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();