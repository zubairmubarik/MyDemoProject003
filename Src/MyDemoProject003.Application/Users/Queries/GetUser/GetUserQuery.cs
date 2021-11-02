using MediatR;
using MyDemoProject003.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using MyDemoProject003.Application.Common.Models;
using Microsoft.Extensions.Logging;
using MyDemoProject003.Application.Common.Helpers;

namespace MyDemoProject003.Application.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserDto>
    {     
     
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
       
        private readonly ILogger _logger;
        private readonly IServiceEndPoints _serviceEndPoints;

        public GetUserQueryHandler(IServiceEndPoints serviceEndPoints, ILogger<GetUserQueryHandler> logger)
        {
            _serviceEndPoints = serviceEndPoints;            
            _logger = logger;
        }
        
        //TODO: Conver into Async thread 
        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing User Handler");
            
            return new UserDto()
            {
                Name = _serviceEndPoints.ClientServiceUserName,
                Token = BasicEncryptionDecryption.Base64Decode(_serviceEndPoints.ClientServiceEndpointToken)
            };
        }
    }
}
