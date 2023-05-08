using Microsoft.OpenApi.Models;

namespace DatabaseMigrationSystem.SwaggerConfig
{
    public static class SwaggerConfig
    {
        /// <summary>
        /// Включает мидлварь Swagger-a.
        /// </summary>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {

                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // config.IncludeXmlComments(xmlPath);
                config.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Title",
                    });

                    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme.  
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    config.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

        /// <summary>
        /// Настройки использования Swagger-a.
        /// </summary>
        public static void UseProjectSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(config =>
            {
                config.SerializeAsV2 = false;
                config.RouteTemplate = $"api-docs/{{documentname}}/swagger.json";
            });

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("api-docs/v1/swagger.json", "API");
                config.RoutePrefix = string.Empty;
                config.DisplayRequestDuration();
            });
        }
    }
}