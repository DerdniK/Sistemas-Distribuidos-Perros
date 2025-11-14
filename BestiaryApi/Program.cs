using BestiaryApi.Services;
using BestiaryApi.Gateways;

var builder = WebApplication.CreateBuilder(args);

// REST utiliza OpenApi/Swagger como contrato
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Va a generar de manera dinamica la documentacion
builder.Services.AddControllers();
builder.Services.AddScoped<IMobService, MobService>();
builder.Services.AddScoped<IMobGateway, MobGateway>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(); // En la misma api se levanta una ruta a la documentacion bonita

app.UseHttpsRedirection(); // Cuando hagamos una peticion HTTP lo va a redireccionar a HTTPS en automatico
app.MapControllers();

app.Run();