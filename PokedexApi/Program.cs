using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Services;
using PokedexApi.Gateways;
using Grpc.Net.Client;
using PokedexApi.Infrastructure.Grpc;
using TrainerService = PokedexApi.Services.TrainerService;

var builder = WebApplication.CreateBuilder(args);

// REST utiliza OpenApi/Swagger como contrato
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Va a generar de manera dinamica la documentacion
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonGateway, PokemonGateway>();
builder.Services.AddScoped<ITrainerGateway, TrainerGateway>();
builder.Services.AddScoped<ITrainerService, TrainerService>();

builder.Services.AddSingleton(services =>
{
    var channel = GrpcChannel.ForAddress(builder.Configuration.GetValue<string>("TrainerApiEndpoint"));
    return new PokedexApi.Infrastructure.Grpc.TrainerService.TrainerServiceClient(channel);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("Authentication:Authority");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidateActor = false,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = "pokedex-api",
            ValidateIssuerSigningKey = true
        };
        options.RequireHttpsMetadata = false;
    });
// Headers
// Authentication = Bearer Token64

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Read", policy => policy.RequireClaim("http://schemas.microsoft.com/identity/claims/scope", 
                    "read"));
        
        options.AddPolicy("Write", policy => policy.RequireClaim("http://schemas.microsoft.com/identity/claims/scope", 
                    "write"));

});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(); // En la misma api se levanta una ruta a la documentacion bonita

// app.UseAuthentication(); // Activamos la autenticacion para que funcione en el servicio
// app.UseAuthorization();

app.UseHttpsRedirection(); // Cuando hagamos una peticion HTTP lo va a redireccionar a HTTPS en automatico
app.MapControllers();

app.Run();