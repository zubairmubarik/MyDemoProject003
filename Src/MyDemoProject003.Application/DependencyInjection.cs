using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyDemoProject003.Application.Common.Behaviours;
using MyDemoProject003.Domain.Entities;
using System.Reflection;

namespace MyDemoProject003.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());            
            services.AddMediatR(Assembly.GetExecutingAssembly());           
            services.AddValidatorsFromAssembly(typeof(CustomerDocument).Assembly);
            return services;
        }
    }
}
