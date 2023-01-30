using FreeGames.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder => builder
            .SetIsOriginAllowed(orign => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

app.MapControllers();

app.Run();