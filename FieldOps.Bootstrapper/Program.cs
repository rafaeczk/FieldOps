using FieldOps.Modules.Accounts.Api;
using FieldOps.Modules.Operators.Api;
using FieldOps.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddAccountsModule();
builder.Services.AddOperatorsModule();

var app = builder.Build();

app.UseInfrastructure();
app.UseAccountsModule();
app.UseOperatorsModule();

app.Run();
