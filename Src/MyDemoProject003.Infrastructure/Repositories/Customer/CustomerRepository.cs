using MyDemoProject003.Domain.Entities;
using MyDemoProject003.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Linq.Expressions;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyDemoProject003.Infrastructure.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {     
        private readonly IApplicationDbContext _dbContext;
        public CustomerRepository(IApplicationDbContext dbContext)
        {  
            //// Assign the context
            _dbContext = dbContext;
        }

        public async Task<CustomerDocument> CreateAsync(CustomerDocument item, CancellationToken cancellationToken)
        {
            _dbContext.Customers.Add(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item;
        }

        public async Task<List<CustomerDocument>> GetAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.ToListAsync(cancellationToken);           
        }

        public async Task<CustomerDocument> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x=>x.Id == id,cancellationToken);       
        }

        public async Task<CustomerDocument> GetAsync(string username, string password, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password, cancellationToken);
        }

        public async Task<bool> ValidateUserAsync(string userEmail, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.AnyAsync(x=>x.Email == userEmail, cancellationToken);            
        }

        public async Task<CustomerDocument> PatchAsync(string id, CustomerDocument item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IList<CustomerDocument> SearchFor(Expression<Func<CustomerDocument, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDocument> UpdateAsync(string id, CustomerDocument item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<long> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
