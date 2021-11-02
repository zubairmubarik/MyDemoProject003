using MediatR;
using MyDemoProject003.Domain.Entities;
using MyDemoProject003.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemoProject003.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomer : IRequest<CustomerDocument>
    {      
        private readonly string _id;
        private readonly CustomerDocument _customer;
        public CustomerDocument Customer { get { return _customer; } }
        public string Id { get { return _id; } }
        public UpdateCustomer(string id ,CustomerDocument customer)
        {
            _id = id;
            _customer = customer;            
        }
    }

    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomer, CustomerDocument>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerDocument> Handle(UpdateCustomer request, CancellationToken cancellationToken)
        {
            return await _customerRepository.UpdateAsync(request.Id,request.Customer,cancellationToken);
        }
    }
}