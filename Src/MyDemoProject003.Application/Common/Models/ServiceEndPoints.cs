using MyDemoProject003.Application.Common.Interfaces;

namespace MyDemoProject003.Application.Common.Models
{
    public class ServiceEndPoints : IServiceEndPoints
    {
        public string ClientServiceEndpoint { get; set; }
        public string ClientServiceEndpointToken { get; set; }
        public string ClientServiceUserName { get; set; }
    }
}
