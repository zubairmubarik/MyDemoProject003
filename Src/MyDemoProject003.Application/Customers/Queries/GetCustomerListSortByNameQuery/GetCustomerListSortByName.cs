using MediatR;
using MyDemoProject003.Common.Utilities;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MyDemoProject003.Application.Common.Models;
using MyDemoProject003.Application.Helpers.Constants;
using MyDemoProject003.Application.Customers.Queries.GetCustomersList;

namespace MyDemoProject003.Application.Customers.Queries.GetCustomerListSortByNameQuery
{
    public class GetCustomerListSortByName : IRequest<ResponseListDto<CustomerDto>>
    {
       
    }

    public class GetCustomerListSortByNametHandler : IRequestHandler<GetCustomerListSortByName, ResponseListDto<CustomerDto>>
    {
      
        private readonly IMediator _mediator;

        public GetCustomerListSortByNametHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseListDto<CustomerDto>> Handle(GetCustomerListSortByName request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCustomerListQuery());

            return new ResponseListDto<CustomerDto>()
            {
                Value = (result.Value.Count() >0 )? result.Value.OrderBy(x=>x.Name) : null,
                Description = (result.Value.Count() > 0) ? Messages.RecordsFound : Messages.NoRecordFound,
                ResponseCode = (result.Value.Count() > 0) ? ResponseCode.Ok : ResponseCode.NotFound,
                IsError = false
            };
        }
    }
}
