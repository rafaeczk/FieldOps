using FieldOps.Modules.Users.Api;
using FieldOps.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddUsersModule();

builder.Services.AddCors();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseInfrastructure();
app.UseUsersModule();

app.Run();
