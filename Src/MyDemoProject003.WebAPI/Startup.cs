using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyDemoProject003.Application;
using MyDemoProject003.Application.Common.Interfaces;
using MyDemoProject003.Application.Common.Models;
using MyDemoProject003.Infrastructure;
using MyDemoProject003.Infrastructure.Services;
using MyDemoProject003.WebAPI.Extensions;
using FluentValidation;
using System.Text;
using MyDemoProject003.Application.Common.Behaviours;

namespace MyDemoProject003
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "CorsPolicy";
        readonly string LogFileForTracing = "Logs/WEBAPI-{Date}.txt";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddApplicationInsightsTelemetry();
            // Configuration Options           
           // services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));

            // Configuration Settings
            services.Configure<ServiceEndPoints>(Configuration.GetSection(nameof(ServiceEndPoints)));
            //services.Configure<ServiceEndPoints>(
            //    o =>
            //    {
            //        o.ClientServiceEndpoint = Configuration.GetValue<String>("ClientServiceEndpoint");
            //        o.ClientServiceEndpointToken = Configuration.GetValue<String>("ClientServiceEndpointToken");      
            //    });

            services.AddScoped<ICurrentUserService, CurrentUserService>();
          
            services.AddSingleton<IServiceEndPoints>(x => x.GetRequiredService<IOptions<ServiceEndPoints>>().Value);



            // Add JWT Bearer Authentication Scheme
            var secretKey = Configuration.GetSection("JwtSecretKey").Value;
            var key = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(secretKey));            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>{
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false , //must be true in production
                        ValidateAudience = false, // must be true in production
                        IssuerSigningKey = key
                    };
                });

            services.AddHttpContextAccessor();

            //services.AddSingleton //         
            //services.AddTransient //                  
            //services.AddScoped //

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        //.AllowCredentials()
                        );
            });
                        
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "My Demo Project For Zubair Demo Project";
                configure.Description = "ZubairDemoProject003";
                configure.Version = "v1";

                configure.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "DemoProject003 API";
                    document.Info.Description = "A Demp ASP.NET Core web API (NET 💜 Azure)";
                    document.Info.TermsOfService = "This project is just to demonstrate skills";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Zubair Mubarik",
                        Email = "zubair.mubarik@outlook.com",
                        Url = "au.linkedin.com/in/zubairmubarik/"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // By Zubair             
            // Dev environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();         

                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                // Non Dev environemnt
                app.UseExceptionHandler("/error");
            }

            // Run in both Dev & prod enironment only 
            app.UseOpenApi();
            app.UseSwaggerUi3();
          
           

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins); //Use Cors

            app.UseAuthentication();// For JWT Bearer Authentication Scheme

            app.UseAuthorization();

            loggerFactory.AddFile(LogFileForTracing); // Log File with Date for Tracing

            // By Zubair             
            // New v1 router
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("api", "api/v1/{controller}/{action}/");
                endpoints.MapControllers();
            });            
        }
    }
}
