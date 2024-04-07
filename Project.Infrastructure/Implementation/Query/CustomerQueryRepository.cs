using Microsoft.EntityFrameworkCore;
using Project.Domain.Abstractions.QueryRepositories;
using Project.Domain.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;

namespace Project.Infrastructure.Implementation.Query
{
    public class CustomerQueryRepository : QueryRepository<Customer>, ICustomerQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CustomerQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _applicationDbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }
        // Extand your method or serce 
    }
}
