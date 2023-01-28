using FreeGames.Api.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen();

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Free Games API",
        Description = "API para integração à Epic Games e Discord. \n\n" +
        "Como utilizar: \n\n" +
        "1. Crie um webhook no seu canal de texto do discord. \n\n" +
        "2. Copie a url do webhook para o appsettings.json \"Discord:UrlWebhook\" \n\n" +
        "3. Utilize um bot do discord (Loritta) e mapeie um comando para que faça um requisição no método GET (api/EpicGames/GetFreeGames) \n\n" +
        "4. Agora sempre que a requisição GET for realizada, a API enviará uma mensagem para seu canal de texto do discord.",
        Contact = new OpenApiContact
        {
            Name = "Wilian Simões Alexandre",
            Url = new Uri("https://www.linkedin.com/in/wilian-simoes"),
        }
    });
});

// Services
builder.Services.AddScoped<EpicGames_Service>();
builder.Services.AddScoped<Discord_Service>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
