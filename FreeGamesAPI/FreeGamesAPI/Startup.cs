using FreeGamesAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System;

namespace FreeGamesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Swagger
            services.AddSwaggerGen();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
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

            // services
            services.AddScoped<EpicGames_Service>();
            services.AddScoped<Discord_Service>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}