using MyDemoProject003.Application.Common.Interfaces;

namespace MyDemoProject003.Application.Common.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
