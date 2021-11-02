using Microsoft.AspNetCore.Http;
using MyDemoProject003.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDemoProject003.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            // UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserId = "ZipCoAmin";
        }

        public string UserId { get; }
    }
}
