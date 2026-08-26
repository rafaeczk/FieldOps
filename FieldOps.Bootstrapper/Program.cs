using FieldOps.Modules.Accounts.Api;
using FieldOps.Modules.Operators.Api;
using FieldOps.Modules.Technicians.Api;
using FieldOps.Modules.WorkOrders.Api;
using FieldOps.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddAccountsModule();
builder.Services.AddOperatorsModule();
builder.Services.AddTechniciansModule();
builder.Services.AddWorkOrdersModule();


var app = builder.Build();

app.UseInfrastructure();
app.UseAccountsModule();
app.UseOperatorsModule();
app.UseTechniciansModule();
app.UseWorkOrdersModule();
app.Run();
