using FieldOps.Modules.Accounts.Api;
using FieldOps.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddAccountsModule();

var app = builder.Build();

app.UseInfrastructure();
app.UseAccountsModule();

app.Run();
