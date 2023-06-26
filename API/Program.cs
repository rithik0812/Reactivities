using Application;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using AutoMapper;
using API.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline. the order of operations matter
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// use the cors policy 
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // context is essentially an instance of the DB for this project, defined in the DataContext class 
    var context = services.GetRequiredService<DataContext>();
    // this Migrate method that is used every time we run the app, is the same as db.update for any changes we want in our db
    // every time it makes an record of the update in the Migration ID table 
    await context.Database.MigrateAsync();
    
    // add hardcoded seed data in DB
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>(); 
    logger.LogError(ex, "An rror occured during migration");
}

app.Run();
