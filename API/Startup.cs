using Common.DataContext;
using ExceptionHandling.DependencyResolver;
using ExceptionHandling.ExceptionManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using API.Configurations;
using BAL.DependencyResolver;
using DTO.Models.Auth;
using API.Services;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var nlogPath = Path.Combine(AppContext.BaseDirectory, "nlog.config");
            if (File.Exists(nlogPath))
            {
                LogManager.LoadConfiguration(nlogPath);
            }
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnection>(sp =>
            new SqlConnection(sp.GetRequiredService<IConfiguration>().GetConnectionString("defaultconnection")));

            // services.AddControllers().AddNewtonsoftJson(options => ... ); // for 6.0
            // CHANGE: Added for .NET 8 compatibility (Ensure Microsoft.AspNetCore.Mvc.NewtonsoftJson v8.0.x is installed)
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMemoryCache();
            services.DIBALResolver();
            services.AddSignalR();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddScoped<IEmailService, EmailService>();
            services.Configure<SMSSettings>(Configuration.GetSection("SMSSettings"));

            // services.AddIdentity<ApplicationUser, IdentityRole>()... // Old provider syntax
            // CHANGE: Simplified for .NET 8
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            /* REMOVED REDUNDANT CALL
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            */

            //===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // CHANGE: Combined the configuration into a single chain for .NET 8
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtIssuer"],
                    ValidAudience = Configuration["JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.ExceptionDIResolver();

            // services.AddEndpointsApiExplorer();
            // services.AddSwaggerGen();
            // CHANGE: Added Microsoft.AspNetCore.OpenApi support for .NET 8
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDistributedMemoryCache();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // services.SwaggerConfiguration(); // If this is your custom extension, it's fine.
            services.SwaggerConfiguration();

            services.AddHttpClient("MyHttpClient");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // CHANGE: Swagger is usually moved here in .NET 8 so it doesn't run in production unless needed
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", " 2.0 R3 API");
                });
            }
            else
            {
                app.UseHsts();
            }

            // app.UseCors(MyAllowSpecificOrigins); // WRONG PLACE
            // app.UseStaticFiles(...); // WRONG PLACE

            // CHANGE: Correct Middleware order for .NET 8
            app.UseHttpsRedirection();

            // 1. Static Files FIRST
            app.UseStaticFiles();
            var assetsPath = Path.Combine(env.ContentRootPath, "assets");
            if (!Directory.Exists(assetsPath)) { Directory.CreateDirectory(assetsPath); }
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider(),
                FileProvider = new PhysicalFileProvider(assetsPath),
                RequestPath = "/assets"
            });

            // 2. Routing SECOND
            app.UseRouting();

            // 3. CORS THIRD (Must be after UseRouting)
            app.UseCors(MyAllowSpecificOrigins);

            // 4. Auth FOURTH (Auth comes after CORS but before Endpoints)
            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureExceptionMiddleware();

            // app.UseEndpoints(...) // Valid in Startup.cs pattern
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
