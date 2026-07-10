
using Microsoft.OpenApi.Models;
using System.Reflection;
using BAL.DependencyResolver;

namespace API.Configurations
{
    public static class ConfigureExtensions
    {
        static readonly string _authTokenPolicy = "_@JwtAuthPolicy";
        public static void CorsConfiguration(this IServiceCollection services, IConfiguration configuration, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder =>
                    builder
                    //.WithOrigins(configuration.GetSection("JWTSettings:Issuer").Value)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowAnyOrigin()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials());
            });
        }

        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = " 2.0 R3 API", Version = "v1" });
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerAuth" }
                        },
                        new string[] {}
                    }
                });

                //#region Enable XML Comments (Required for query parameter descriptions)

                    // Gets the XML filename based on assembly (e.g.,API.xml)
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                    // Builds the absolute path to where the XML file will be located at runtime
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    // Informs Swagger to load and use the XML comments from that file
                    c.IncludeXmlComments(xmlPath);

                //#endregion

            });
        }

        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.DIBALResolver();
        }


    }
}
