using MyDemoProject003.Application.Common.Interfaces;
using System;

namespace MyDemoProject003.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
