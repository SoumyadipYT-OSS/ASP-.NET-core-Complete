using System.Data;
using AutoWorks.Api.Repositories;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("AutoWorksDb")));

builder.Services.AddScoped<ManufacturerRepository>();
builder.Services.AddScoped<VehicleModelRepository>();
builder.Services.AddScoped<VehicleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
