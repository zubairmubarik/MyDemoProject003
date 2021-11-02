using MediatR;
using MyDemoProject003.Common.Utilities;
using System.Threading;
using System.Threading.Tasks;
using MyDemoProject003.Application.Common.Interfaces;
using MyDemoProject003.Application.Common.Models;
using MyDemoProject003.Application.Helpers.Constants;
using AutoMapper;
using System;

namespace MyDemoProject003.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerByIdQuery : IRequest<ResponseDto<CustomerDto>>
    {
        private readonly Guid _customerId;
        public Guid CustomerId => _customerId; 
        public GetCustomerByIdQuery(Guid id)
        {
            _customerId = id;
        }
    }

    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, ResponseDto<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _customerRepository.GetAsync(request.CustomerId, cancellationToken);

            return new ResponseDto<CustomerDto>()
            {
                Value = (result != null) ? _mapper.Map<CustomerDto>(result) : null,
                Description = (result != null) ? Messages.RecordsFound : Messages.NoRecordFound,
                ResponseCode = (result != null) ? ResponseCode.Ok : ResponseCode.NotFound,
                IsError = false
            };
        }
    }
}
