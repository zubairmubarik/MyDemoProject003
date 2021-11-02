
using MediatR;
using Microsoft.Extensions.Logging;
using MyDemoProject003.Application.Common.Interfaces;
using MyDemoProject003.Application.Common.Models;
using MyDemoProject003.Common.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemoProject003.Application.Customers.Queries.AuthenticateCustomer
{
    public class AuthenticateCustomerQuery : IRequest<ResponseDto<LoginDto>>
    {
        private readonly string _userName;
        private readonly string _userPassword;
       
        public string UserName { get { return _userName; } }
        public  string UserPassword { get { return _userPassword; } }

        public AuthenticateCustomerQuery(string userName, string userPassword)
        {
            _userName = userName;
            _userPassword = userPassword;
        }
    }

    public class AuthenticateCustomerQueryHandler : IRequestHandler<AuthenticateCustomerQuery, ResponseDto<LoginDto>>
    {
        private readonly ICustomerRepository _customerRepository;   
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IJwtHelperLibrary _jwtHelperLibrary;

        public AuthenticateCustomerQueryHandler(IJwtHelperLibrary jwtHelperLibrary, ICustomerRepository customerRepository, IMediator mediator,  ILogger<AuthenticateCustomerQueryHandler> logger)
        {
            _customerRepository = customerRepository;  
            _logger = logger;
            _mediator = mediator;
            _jwtHelperLibrary = jwtHelperLibrary;
        }
        public async Task<ResponseDto<LoginDto>> Handle(AuthenticateCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Validate Uername and Password");
           
            var result =  await _customerRepository.GetAsync(request.UserName,request.UserPassword, cancellationToken) ;

            var jwtToken = (result != null) ? _jwtHelperLibrary.CreateJsonWebTokenWithSymetricEncryption(result) :string.Empty;

            return new ResponseDto<LoginDto>()
            {
                Value =  (result!=null) 
                ? new LoginDto(){ UserName = request.UserName, UserToken = jwtToken } : null,
                Description = result != null ? "New record created" : "Failed to authenticate customer",
                ResponseCode = result != null ? ResponseCode.Ok : ResponseCode.BusinessError,
                IsError = false
            };
         

        }
    }
}
