using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FreeGames.Api.Extensions
{
    public static class SwaggerSetup
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Free Games API",
                    Description = "API para integração à Epic Games e Discord. \n\n" +
                    "Como utilizar: \n\n" +
                    "1. Crie um webhook no seu canal de texto do discord. \n\n" +
                    "2. Copie a url do webhook, faça login na api e cadastre a url do webhook no endpoint POST /api/DiscordConfiguration/CadastrarConfiguracao  \n\n" +
                    "ATENÇÃO: Agora os passos abaixo necessitam de autenticação por token. \n\n" +
                    "3. Utilize um bot do discord (Loritta) e mapeie um comando para que faça um requisição no endpoint GET api/EpicGames/GetFreeGames \n\n" +
                    "4. Agora sempre que a requisição GET for realizada, a API enviará uma mensagem para seu canal de texto do discord.",
                    Contact = new OpenApiContact
                    {
                        Name = "Wilian Simões Alexandre",
                        Url = new Uri("https://www.linkedin.com/in/wilian-simoes"),
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                Enter 'Bearer' [space] and then your token in the text input below. 
                                Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                        new List<string>()
                    }
                });
            });
        }
    }
}