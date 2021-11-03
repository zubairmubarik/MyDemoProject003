//using Moneytech.Middleware.Application.Common.Interfaces;
//using Moneytech.Middleware.Infrastructure.Identity;
//using Moneytech.Middleware.Infrastructure.Persistence;
//using Moneytech.Middleware.WebUI;
//using MediatR;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using MyDemoProject003.Application.Common.Interfaces;

namespace MyDemoProject003.ApplicationUnitTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
      
        public static async Task ResetState()
        {
         
           
        }
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "MyDemoProject003.WebAPI"));

            services.AddLogging();

            startup.ConfigureServices(services);

           

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

         
        }

        public static IValidationClass GetValidationClass()
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetService<IValidationClass>();

            return service;
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
