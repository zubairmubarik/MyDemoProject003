using MediatR;
using MyDemoProject003.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemoProject003.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomer : IRequest<long>
    {
        private readonly string _customerId;
        public string CustomerId { get { return _customerId; } }
        public DeleteCustomer(string id)
        {
            _customerId = id;
        }
    }

    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomer, long>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<long> Handle(DeleteCustomer request, CancellationToken cancellationToken)
        {
            return await _customerRepository.DeleteAsync(request.CustomerId,cancellationToken);
        }
    }
}