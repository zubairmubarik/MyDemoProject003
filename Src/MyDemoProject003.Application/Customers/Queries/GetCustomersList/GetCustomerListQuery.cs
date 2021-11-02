using MediatR;
using MyDemoProject003.Common.Utilities;
using MyDemoProject003.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MyDemoProject003.Application.Helpers.Constants;
using MyDemoProject003.Application.Common.Models;
using AutoMapper;
using System.Collections.Generic;

namespace MyDemoProject003.Application.Customers.Queries.GetCustomersList
{
    public class GetCustomerListQuery : IRequest<ResponseListDto<CustomerDto>>
    {
    }

    public class GetCustomerListHandler : IRequestHandler<GetCustomerListQuery, ResponseListDto<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public GetCustomerListHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ResponseListDto<CustomerDto>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            var result = await _customerRepository.GetAsync(cancellationToken);

            return new ResponseListDto<CustomerDto>()
            {
                Value = result.Count > 0 ? _mapper.Map<List<CustomerDto>>(result) : null,
                Description = (result.Count > 0) ? Messages.RecordsFound : Messages.NoRecordFound,
                ResponseCode = (result.Count > 0) ? ResponseCode.Ok : ResponseCode.NotFound,
                IsError = false
            };

        }
    }
}
