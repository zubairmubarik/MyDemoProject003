using MediatR;
using MyDemoProject003.Domain.Entities;
using MyDemoProject003.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using MyDemoProject003.Application.Common.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MyDemoProject003.Common.Utilities;
using System;
using MyDemoProject003.Application.Common.Helpers;

namespace MyDemoProject003.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<ResponseDto<CustomerDto>>
    {
        private readonly CustomerDocument _customer;
        public CustomerDocument Customer { get { return _customer; } }
        public CreateCustomerCommand(CustomerDocument customer)
        {
            _customer = customer;
        }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseDto<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidationClass _validationClass;

        public CreateCustomerCommandHandler(IValidationClass validationClass, ICustomerRepository customerRepository, IMapper mapper, ILogger<CreateCustomerCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
            _validationClass = validationClass;
        }
        public async Task<ResponseDto<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.Customer;
            var eligibleThreshold = 1000;
            try
            {               
                _logger.LogInformation("Executing Customer Handler");

                //TODO: Business Validators must be implemented in FluentValidator
                var checkMandatoryFields = _validationClass.ValidateCustomerMandatoryFields(customer);

                if (!checkMandatoryFields)
                {
                    return new ResponseDto<CustomerDto>()
                    {
                        Value = null,
                        Description = $"Failed to create new record. Customer mandatory fields required.",
                        ResponseCode = ResponseCode.BusinessError,
                        IsError = true
                    };
                }

                //TODO: Business Validators must be implemented in FluentValidator
                var checkEmailValidaity = _validationClass.ValidateCustomerEmailFormat(customer.Email);

                if (!checkEmailValidaity)
                {
                    return new ResponseDto<CustomerDto>()
                    {
                        Value = null,
                        Description = $"Failed to create new record. Customer Email format is not correct.",
                        ResponseCode = ResponseCode.BusinessError,
                        IsError = true
                    };
                }


                //TODO: Business Validators must be implemented in FluentValidator
                var isEligible = _validationClass.ValidateCustomerThreshold(customer);

                if (!isEligible)
                {
                    return new ResponseDto<CustomerDto>()
                    {
                        Value = null,
                        Description = $"Failed to create new record. Customer failed criteria.",
                        ResponseCode = ResponseCode.BusinessError,
                        IsError = true
                    };
                }

                var isExist = await _customerRepository.ValidateUserAsync(customer.Email, cancellationToken);

                if(isExist)
                {
                    return new ResponseDto<CustomerDto>()
                    {
                        Value =  null,
                        Description =   $"Failed to create new record. Email:{customer.Email} already exist",
                        ResponseCode =  ResponseCode.BusinessError,
                        IsError = true
                    };
                }

                request.Customer.Credit = eligibleThreshold;
                customer = await _customerRepository.CreateAsync(request.Customer, cancellationToken);               
            }
            catch (Exception eX)
            {
                _logger.LogError("Error: {0}", eX.Message);
            }

            return new ResponseDto<CustomerDto>()
            {
                Value = customer.Id != null ? _mapper.Map<CustomerDto>(customer) : null,
                Description = customer.Id != null ? "New record created" : "Failed to create new record",
                ResponseCode = customer.Id != null ? ResponseCode.Ok : ResponseCode.BusinessError,
                IsError = customer.Id != null ? false : true
            };

        }
    }
}