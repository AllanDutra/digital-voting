using System.Reflection;
using Microsoft.OpenApi.Models;

namespace DigitalVoting.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DigitalVoting.API", Version = "v1", Description = "The project developed is a digital vote in which the voter can vote in several ballots but only in one option per vote. The following technologies/patterns were used in this project: C#, ASP.NET Core, .NET 7.0, Entity Framework Core, Dapper, PostgreSQL, JWT authentication, FluentValidation, CQRS and Repository Pattern. No \"position\" restrictions were required for the existing methods, just the authentication restriction so that the system can keep track of the people who have already voted." });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // ? ADICIONA DOCUMENTAÇÃO EM PROPRIEDADES DE OBJETO QUERY
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new AssemblyName[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                    .Where(f => File.Exists(f)).ToArray();
                Array.ForEach(xmlDocs, (d) => { c.IncludeXmlComments(d); });

                // ? ADICIONA SERVIÇO DE UTILIZAÇÃO DE TOKEN JWT NO HEADER AUTHORIZATION
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "Bearer Authorization Header"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}