using MyDemoProject003.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemoProject003.Application.Common.Interfaces
{
    public interface ICustomerRepository :IGenericInterface<CustomerDocument>
    {  
        IList<CustomerDocument> SearchFor(Expression<Func<CustomerDocument, bool>> predicate, CancellationToken cancellationToken);
        Task<CustomerDocument> GetAsync(string username, string password, CancellationToken cancellationToken);
        Task<bool> ValidateUserAsync(string userEmail ,CancellationToken cancellationToken);
    }
}
