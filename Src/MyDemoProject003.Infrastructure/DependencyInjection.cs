using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDemoProject003.Application.Common.Helpers;
using MyDemoProject003.Application.Common.Interfaces;
using MyDemoProject003.Infrastructure.Persistence;
using MyDemoProject003.Infrastructure.Repositories.Customer;
using MyDemoProject003.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;


namespace MyDemoProject003.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();          
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
           
            services.AddTransient<IJwtHelperLibrary, JwtHelperLibrary>();
            services.AddTransient<IDateTime, DateTimeService>();

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("Zipco.CustomerDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddHttpClient(); /* Add IHTTPClientFactory and related services*/           

            return services;
        }
    }
}
