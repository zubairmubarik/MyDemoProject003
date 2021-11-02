using MediatR;
using MyDemoProject003.Domain.Entities;
using MyDemoProject003.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemoProject003.Application.Customers.Commands.PatchCustomer
{
    public class PatchCustomer : IRequest<CustomerDocument>
    {

        private readonly string _id;
        private readonly CustomerDocument _customer;
        public CustomerDocument Customer { get { return _customer; } }
        public string Id { get { return _id; } }
        public PatchCustomer(string id, CustomerDocument customer)
        {
            _id = id;
            _customer = customer;
        }
    }

    public class PatchCustomerHandler : IRequestHandler<PatchCustomer, CustomerDocument>
    {
        private readonly ICustomerRepository _customerRepository;

        public PatchCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerDocument> Handle(PatchCustomer request, CancellationToken cancellationToken)
        {
            return await _customerRepository.PatchAsync(request.Id, request.Customer, cancellationToken);
        }
    }
}