using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MyDemoProject003.Domain.Entities;


namespace MyDemoProject003.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<CustomerDocument> Customers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
