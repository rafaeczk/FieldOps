using FieldOps.Modules.Accounts.Api;
using FieldOps.Modules.Files.Api;
using FieldOps.Modules.Operators.Api;
using FieldOps.Modules.Technicians.Api;
using FieldOps.Shared.Infrastructure;
using FieldOps.Modules.Jobs.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

builder.Services.AddAccountsModule();
builder.Services.AddOperatorsModule();
builder.Services.AddTechniciansModule();
builder.Services.AddFilesModule();
builder.Services.AddJobsModule();

var app = builder.Build();

app.UseInfrastructure();

app.UseAccountsModule();
app.UseOperatorsModule();
app.UseTechniciansModule();
app.UseFilesModule();
app.UseJobsModule();

app.Run();
