using FieldOps.Modules.Accounts.Api;
using FieldOps.Modules.Files.Api;
using FieldOps.Modules.Operators.Api;
using FieldOps.Modules.Technicians.Api;
using FieldOps.Shared.Infrastructure;
using FieldOps.Modules.Jobs.Api;
using FieldOps.Modules.Reports.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

builder.Services.AddAccountsModule();
builder.Services.AddOperatorsModule();
builder.Services.AddTechniciansModule();
builder.Services.AddFilesModule();
builder.Services.AddJobsModule();
builder.Services.AddReportsModule();

var app = builder.Build();

app.UseInfrastructure();

app.UseAccountsModule();
app.UseOperatorsModule();
app.UseTechniciansModule();
app.UseFilesModule();
app.UseJobsModule();
app.UseReportsModule();
app.Run();
