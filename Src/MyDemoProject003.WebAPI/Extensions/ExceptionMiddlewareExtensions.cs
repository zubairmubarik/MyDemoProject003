using Microsoft.AspNetCore.Builder;
using MyDemoProject003.WebAPI.Common;

namespace MyDemoProject003.WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
