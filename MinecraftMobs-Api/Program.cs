using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using MinecraftMobs_Api.Contracts;
using MinecraftMobs_Api.Infrastructure;
using MinecraftMobs_Api.Services;
using SoapCore;

var builder = WebApplication.CreateBuilder(args); // Preparar todo para una app web
builder.Services.AddSoapCore();
builder.Services.AddScoped<IMobService, MobService>();

builder.Services.AddDbContext<RelationalDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();
app.UseSoapEndpoint<IMobService>("/MobService.svc", new SoapEncoderOptions());
app.Run();

